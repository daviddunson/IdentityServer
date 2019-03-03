﻿// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OAuthRequest.cs" company="GSD Logic">
//   Copyright © 2018 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WpfQuickstart
{
    using System;
    using System.IO;
    using System.Net;
    using System.Net.Sockets;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Security.Cryptography;
    using System.Text;

    public sealed class OAuthRequest
    {
        private const string AuthorizationEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";

        // TODO: this is a sample, please change these two, use your own!
        private const string ClientId = "581786658708-elflankerquo1a6vsckabbhn25hclla0.apps.googleusercontent.com";
        private const string ClientSecret = "3f6NggMbPtrmIBpgx-MK2xXK";
        private const string TokenEndpoint = "https://www.googleapis.com/oauth2/v4/token";
        private const string UserInfoEndpoint = "https://www.googleapis.com/oauth2/v3/userinfo";

        private OAuthRequest()
        {
        }

        public string AuthorizationRequestUri { get; private set; }
        public string CodeVerifier { get; private set; }
        public string RedirectUri { get; private set; }
        public string[] Scopes { get; private set; }
        public string State { get; private set; }

        // https://developers.google.com/identity/protocols/OAuth2InstalledApp
        public static OAuthRequest BuildLoopbackRequest(params string[] scopes)
        {
            var request = new OAuthRequest
            {
                CodeVerifier = RandomDataBase64Url(32),
                Scopes = scopes
            };

            string codeChallenge = Base64UrlEncodeNoPadding(Sha256(request.CodeVerifier));
            const string codeChallengeMethod = "S256";

            string scope = BuildScopes(scopes);

            request.RedirectUri = string.Format("http://{0}:{1}/", IPAddress.Loopback, GetRandomUnusedPort());
            request.State = RandomDataBase64Url(32);
            request.AuthorizationRequestUri = string.Format("{0}?response_type=code&scope=openid%20profile{6}&redirect_uri={1}&client_id={2}&state={3}&code_challenge={4}&code_challenge_method={5}",
                AuthorizationEndpoint,
                Uri.EscapeDataString(request.RedirectUri),
                ClientId,
                request.State,
                codeChallenge,
                codeChallengeMethod,
                scope);

            return request;
        }

        // https://developers.google.com/identity/protocols/OAuth2InstalledApp Step 5: Exchange authorization code for refresh and access tokens
        public OAuthToken ExchangeCodeForAccessToken(string code)
        {
            if (code == null)
                throw new ArgumentNullException(nameof(code));

            string tokenRequestBody = string.Format("code={0}&redirect_uri={1}&client_id={2}&code_verifier={3}&client_secret={4}&scope=&grant_type=authorization_code",
                code,
                Uri.EscapeDataString(this.RedirectUri),
                ClientId, this.CodeVerifier,
                ClientSecret
            );

            return TokenRequest(tokenRequestBody, this.Scopes);
        }

        // this is not used in this sample, but can be used to refresh a token from an old one
        // https://developers.google.com/identity/protocols/OAuth2InstalledApp Refreshing an access token
        public OAuthToken Refresh(OAuthToken oldToken)
        {
            if (oldToken == null)
                throw new ArgumentNullException(nameof(oldToken));

            string tokenRequestBody = string.Format("refresh_token={0}&client_id={1}&client_secret={2}&grant_type=refresh_token",
                oldToken.RefreshToken,
                ClientId,
                ClientSecret
            );

            return TokenRequest(tokenRequestBody, oldToken.Scopes);
        }

        private static string Base64UrlEncodeNoPadding(byte[] buffer)
        {
            string b64 = Convert.ToBase64String(buffer);
            // converts base64 to base64url.
            b64 = b64.Replace('+', '-');
            b64 = b64.Replace('/', '_');
            // strips padding.
            b64 = b64.Replace("=", "");
            return b64;
        }

        private static string BuildScopes(string[] scopes)
        {
            string scope = null;
            if (scopes != null)
            {
                foreach (var sc in scopes)
                {
                    scope += "%20" + Uri.EscapeDataString(sc);
                }
            }

            return scope;
        }

        private static DataContractJsonSerializer CreateSerializer(Type type)
        {
            if (type == null)
                throw new ArgumentNullException(nameof(type));

            var settings = new DataContractJsonSerializerSettings
            {
                DateTimeFormat = new DateTimeFormat("yyyy-MM-dd'T'HH:mm:ss.fffK")
            };
            return new DataContractJsonSerializer(type, settings);
        }

        private static T Deserialize<T>(string json)
        {
            if (string.IsNullOrWhiteSpace(json))
                return default(T);

            return Deserialize<T>(Encoding.UTF8.GetBytes(json));
        }

        private static T Deserialize<T>(byte[] json)
        {
            if (json == null || json.Length == 0)
                return default(T);

            using (var ms = new MemoryStream(json))
            {
                return Deserialize<T>(ms);
            }
        }

        private static T Deserialize<T>(Stream json)
        {
            if (json == null)
                return default(T);

            var ser = CreateSerializer(typeof(T));
            return (T)ser.ReadObject(json);
        }

        // https://stackoverflow.com/questions/223063/how-can-i-create-an-httplistener-class-on-a-random-port-in-c/
        private static int GetRandomUnusedPort()
        {
            var listener = new TcpListener(IPAddress.Loopback, 0);
            listener.Start();
            var port = ((IPEndPoint)listener.LocalEndpoint).Port;
            listener.Stop();
            return port;
        }

        private static UserInfo GetUserInfo(string accessToken)
        {
            var request = (HttpWebRequest)WebRequest.Create(UserInfoEndpoint);
            request.Method = "GET";
            request.Headers.Add(string.Format("Authorization: Bearer {0}", accessToken));
            var response = request.GetResponse();
            using (var stream = response.GetResponseStream())
            {
                return Deserialize<UserInfo>(stream);
            }
        }

        private static string RandomDataBase64Url(int length)
        {
            using (var rng = new RNGCryptoServiceProvider())
            {
                var bytes = new byte[length];
                rng.GetBytes(bytes);
                return Base64UrlEncodeNoPadding(bytes);
            }
        }

        private static byte[] Sha256(string text)
        {
            using (var sha256 = new SHA256Managed())
            {
                return sha256.ComputeHash(Encoding.ASCII.GetBytes(text));
            }
        }

        private static OAuthToken TokenRequest(string tokenRequestBody, string[] scopes)
        {
            var request = (HttpWebRequest)WebRequest.Create(TokenEndpoint);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded";
            byte[] bytes = Encoding.ASCII.GetBytes(tokenRequestBody);
            using (var requestStream = request.GetRequestStream())
            {
                requestStream.Write(bytes, 0, bytes.Length);
            }

            var response = request.GetResponse();
            using (var responseStream = response.GetResponseStream())
            {
                var token = Deserialize<OAuthToken>(responseStream);
                token.ExpirationDate = DateTime.Now + new TimeSpan(0, 0, token.ExpiresIn);
                var user = GetUserInfo(token.AccessToken);
                token.Name = user.Name;
                token.Picture = user.Picture;
                token.Email = user.Email;
                token.Locale = user.Locale;
                token.FamilyName = user.FamilyName;
                token.GivenName = user.GivenName;
                token.Id = user.Id;
                token.Profile = user.Profile;
                token.Scopes = scopes;
                return token;
            }
        }

        // https://developers.google.com/+/web/api/rest/openidconnect/getOpenIdConnect
        [DataContract]
        private class UserInfo
        {
            [DataMember(Name = "email")]
            public string Email { get; set; }

            [DataMember(Name = "family_name")]
            public string FamilyName { get; set; }

            [DataMember(Name = "gender")]
            public string Gender { get; set; }

            [DataMember(Name = "given_name")]
            public string GivenName { get; set; }

            [DataMember(Name = "sub")]
            public string Id { get; set; }

            [DataMember(Name = "kind")]
            public string Kind { get; set; }

            [DataMember(Name = "locale")]
            public string Locale { get; set; }

            [DataMember(Name = "name")]
            public string Name { get; set; }

            [DataMember(Name = "picture")]
            public string Picture { get; set; }

            [DataMember(Name = "profile")]
            public string Profile { get; set; }
        }
    }
}