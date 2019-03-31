// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsentInputModel.cs" company="GSD Logic">
//   Copyright © 2019 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Server.Identity.Quickstart.Consent
{
    using System.Collections.Generic;

    public class ConsentInputModel
    {
        public string Button { get; set; }
        public bool RememberConsent { get; set; }
        public string ReturnUrl { get; set; }
        public IEnumerable<string> ScopesConsented { get; set; }
    }
}