// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Claims.cshtml.cs" company="GSD Logic">
//   Copyright © 2019 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Client.MVC.Pages
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize]
    public class ClaimsModel : PageModel
    {
    }
}