// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Identity.API.Models.AccountViewModels
{
    public class LoginViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Display(Name = "Remember me?")]
        public bool RememberMe { get; set; }
        public string ReturnUrl { get; set; }

        //public int EnableLocalLogin { get; set; }  //add
        //public IEnumerable<ExternalProvider> ExternalProviders { get; set; } //add
    }
    //public class ExternalProvider   //ad
    //{
    //    public string DisplayName { get; set; } //add
    //    public string AuthenticationScheme { get; set; } //add
    //}
}