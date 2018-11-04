// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentityController.cs" company="GSD Logic">
//   Copyright © 2018 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ApiQuickStart.Controllers
{
    using System.Linq;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(this.User.Claims.Select(c => new { c.Type, c.Value }));
        }
    }
}