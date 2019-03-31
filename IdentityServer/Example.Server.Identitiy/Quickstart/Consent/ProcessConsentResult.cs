// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ProcessConsentResult.cs" company="GSD Logic">
//   Copyright © 2019 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Server.Identity.Quickstart.Consent
{
    public class ProcessConsentResult
    {
        public string ClientId { get; set; }

        public bool HasValidationError => this.ValidationError != null;
        public bool IsRedirect => this.RedirectUri != null;
        public string RedirectUri { get; set; }

        public bool ShowView => this.ViewModel != null;
        public string ValidationError { get; set; }
        public ConsentViewModel ViewModel { get; set; }
    }
}