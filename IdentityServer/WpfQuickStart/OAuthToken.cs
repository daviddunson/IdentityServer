// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OAuthToken.cs" company="GSD Logic">
//   Copyright © 2018 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WpfQuickstart
{
    using System;
    using System.Runtime.Serialization;

    [DataContract]
    public class OAuthToken
    {
        [DataMember(Name = "access_token")]
        public string AccessToken { get; set; }

        [DataMember]
        public string Email { get; set; }

        // not from google's response, but we store this
        public DateTime ExpirationDate { get; set; }

        [DataMember(Name = "expires_in")]
        public int ExpiresIn { get; set; }

        [DataMember]
        public string FamilyName { get; set; }

        [DataMember]
        public string GivenName { get; set; }

        [DataMember]
        public string Id { get; set; }

        [DataMember]
        public string Locale { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string Picture { get; set; }

        [DataMember]
        public string Profile { get; set; }

        [DataMember(Name = "refresh_token")]
        public string RefreshToken { get; set; }

        [DataMember]
        public string[] Scopes { get; set; }

        [DataMember(Name = "token_type")]
        public string TokenType { get; set; }
    }
}