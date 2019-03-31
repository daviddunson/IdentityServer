// --------------------------------------------------------------------------------------------------------------------
// <copyright file="ConsentOptions.cs" company="GSD Logic">
//   Copyright © 2019 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Server.Identity.Quickstart.Consent
{
    public class ConsentOptions
    {
        public static readonly string InvalidSelectionErrorMessage = "Invalid selection";

        public static readonly string MustChooseOneErrorMessage = "You must pick at least one permission";
        public static bool EnableOfflineAccess = true;
        public static string OfflineAccessDescription = "Access to your applications and resources, even when you are offline";
        public static string OfflineAccessDisplayName = "Offline Access";
    }
}