// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Claims.cshtml.cs" company="GSD Logic">
//   Copyright © 2019 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Client.MVC.Pages
{
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Authentication;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Newtonsoft.Json.Linq;

    [Authorize]
    public class ClaimsModel : PageModel
    {
        public async Task<string> CallApi()
        {
            var accessToken = await this.HttpContext.GetTokenAsync("access_token");

            var client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", accessToken);
            var content = await client.GetStringAsync("http://localhost:5001/identity");

            return JObject.Parse(content).ToString();
        }
    }
}