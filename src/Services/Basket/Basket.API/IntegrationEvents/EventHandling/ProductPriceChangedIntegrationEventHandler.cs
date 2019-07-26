﻿using Microsoft.eShopOnContainers.Services.Basket.API.Model;
using System;
using System.Linq;
using System.Threading.Tasks;
using eShopOnContainers.Services.IntegrationEvents.Events;
using NServiceBus;

namespace Microsoft.eShopOnContainers.Services.Basket.API.IntegrationEvents.EventHandling
{
    public class ProductPriceChangedIntegrationEventHandler : IHandleMessages<ProductPriceChangedIntegrationEvent>
    {
        private readonly IBasketRepository _repository;

        public ProductPriceChangedIntegrationEventHandler(IBasketRepository repository)
        {
            _repository = repository ?? throw new ArgumentNullException(nameof(repository));
        }

        //public async Task Handle(ProductPriceChangedIntegrationEvent @event)
        //{
        //        //_logger.LogInformation("----- Handling integration event: {IntegrationEventId} at {AppName} - ({@IntegrationEvent})", @event.Id, Program.AppName, @event);

        //        var userIds = _repository.GetUsersAsync();

        //        foreach (var id in userIds)
        //        {
        //            var basket = await _repository.GetBasketAsync(id);

        //            await UpdatePriceInBasketItems(@event.ProductId, @event.NewPrice, @event.OldPrice, basket);
        //        }
        //    }
        //}

        public async Task Handle(ProductPriceChangedIntegrationEvent message, IMessageHandlerContext context)
        {
            var userIds = await _repository.GetUsersAsync();
            
            foreach (var id in userIds)
            {
                var basket = await _repository.GetBasketAsync(id);

                await UpdatePriceInBasketItems(message.ProductId, message.NewPrice, message.OldPrice, basket);                      
            }
        }

        private async Task UpdatePriceInBasketItems(int productId, decimal newPrice, decimal oldPrice, CustomerBasket basket)
        {
            var itemsToUpdate = basket?.Items?.Where(x => int.Parse(x.ProductId) == productId).ToList();

            if (itemsToUpdate != null)
            {
                foreach (var item in itemsToUpdate)
                {
                    if(item.UnitPrice == oldPrice)
                    { 
                        var originalPrice = item.UnitPrice;
                        item.UnitPrice = newPrice;
                        item.OldUnitPrice = originalPrice;
                    }
                }
                await _repository.UpdateBasketAsync(basket);
            }         
        }

        
    }
}

