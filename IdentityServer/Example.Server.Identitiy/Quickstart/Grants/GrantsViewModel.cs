// --------------------------------------------------------------------------------------------------------------------
// <copyright file="GrantsViewModel.cs" company="GSD Logic">
//   Copyright © 2019 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Server.Identity.Quickstart.Grants
{
    using System;
    using System.Collections.Generic;

    public class GrantsViewModel
    {
        public IEnumerable<GrantViewModel> Grants { get; set; }
    }

    public class GrantViewModel
    {
        public IEnumerable<string> ApiGrantNames { get; set; }
        public string ClientId { get; set; }
        public string ClientLogoUrl { get; set; }
        public string ClientName { get; set; }
        public string ClientUrl { get; set; }
        public DateTime Created { get; set; }
        public DateTime? Expires { get; set; }
        public IEnumerable<string> IdentityGrantNames { get; set; }
    }
}