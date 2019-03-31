// --------------------------------------------------------------------------------------------------------------------
// <copyright file="LoggedOutViewModel.cs" company="GSD Logic">
//   Copyright © 2019 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Server.Identity.Quickstart.Account
{
    public class LoggedOutViewModel
    {
        public bool AutomaticRedirectAfterSignOut { get; set; } = false;
        public string ClientName { get; set; }
        public string ExternalAuthenticationScheme { get; set; }

        public string LogoutId { get; set; }
        public string PostLogoutRedirectUri { get; set; }
        public string SignOutIframeUrl { get; set; }
        public bool TriggerExternalSignout => this.ExternalAuthenticationScheme != null;
    }
}