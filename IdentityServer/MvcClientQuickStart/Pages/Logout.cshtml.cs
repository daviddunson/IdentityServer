// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Logout.cshtml.cs" company="GSD Logic">
//   Copyright © 2018 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MvcClientQuickStart.Pages
{
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize]
    public class LogoutModel : PageModel
    {
        public async Task Logout()
        {
            await this.HttpContext.SignOutAsync("Cookies");
            await this.HttpContext.SignOutAsync("oidc");
        }

        public void OnGet()
        {
            this.Logout().Wait();
        }
    }
}