// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Claims.cshtml.cs" company="GSD Logic">
//   Copyright © 2018 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace MvcClientQuickStart.Pages
{
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    [Authorize]
    public class ClaimsModel : PageModel
    {
    }
}