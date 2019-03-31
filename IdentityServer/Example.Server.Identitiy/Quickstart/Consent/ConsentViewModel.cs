// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsentViewModel.cs" company="GSD Logic">
//   Copyright © 2019 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Server.Identity.Quickstart.Consent
{
    using System.Collections.Generic;

    public class ConsentViewModel : ConsentInputModel
    {
        public bool AllowRememberConsent { get; set; }
        public string ClientLogoUrl { get; set; }
        public string ClientName { get; set; }
        public string ClientUrl { get; set; }

        public IEnumerable<ScopeViewModel> IdentityScopes { get; set; }
        public IEnumerable<ScopeViewModel> ResourceScopes { get; set; }
    }
}