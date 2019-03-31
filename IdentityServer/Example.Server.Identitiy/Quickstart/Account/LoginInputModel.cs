// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoginInputModel.cs" company="GSD Logic">
//   Copyright © 2019 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Server.Identity.Quickstart.Account
{
    using System.ComponentModel.DataAnnotations;

    public class LoginInputModel
    {
        [Required] public string Password { get; set; }

        public bool RememberLogin { get; set; }
        public string ReturnUrl { get; set; }

        [Required] public string Username { get; set; }
    }
}