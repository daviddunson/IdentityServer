// --------------------------------------------------------------------------------------------------------------------
// <copyright file="OAuthState.cs" company="GSD Logic">
//   Copyright © 2019 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace Example.Client.WPF
{
    using System;
    using System.ComponentModel;

    // state model

    // This is a sample. Fille information (email, etc.) can depend on scopes

    // largely inspired from
    // https://github.com/googlesamples/oauth-apps-for-windows

    public class OAuthState : INotifyPropertyChanged
    {
        private OAuthToken _token;
        public event PropertyChangedEventHandler PropertyChanged;
        public bool IsNotSigned => !this.IsSigned;

        public bool IsSigned => this.Token != null && this.Token.ExpirationDate > DateTime.Now;

        public OAuthToken Token
        {
            get => this._token;
            set
            {
                if (this._token == value)
                    return;

                this._token = value;
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.Token)));
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.IsSigned)));
                this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(this.IsNotSigned)));
            }
        }
    }
}