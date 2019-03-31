// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IdentityController.cs" company="GSD Logic">
//   Copyright © 2019 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Server.API.Controllers
{
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [Authorize]
    [Route("identity")]
    public class IdentityController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var result = new
            {
                IDToken = await this.HttpContext.GetTokenAsync("id_token"),
                AccessToken = await this.HttpContext.GetTokenAsync("access_token"),
                Claims = this.User.Claims.Select(c => new { c.Type, c.Value })
            };

            return new JsonResult(result);
        }
    }
}