// --------------------------------------------------------------------------------------------------------------------
// <copyright file="MainWindow.xaml.cs" company="GSD Logic">
//   Copyright © 2018 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace WpfQuickstart
{
    using System;
    using System.Net;
    using System.Text;
    using System.Threading;
    using System.Windows;

    public partial class MainWindow
    {
        public MainWindow()
        {
            this.InitializeComponent();
            this.State = new OAuthState();
            this.DataContext = this;
        }

        public OAuthState State { get; }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            var thread = new Thread(this.HandleRedirect);
            thread.Start();
        }

        private async void HandleRedirect()
        {
            this.State.Token = null;

            // for example, let's pretend I want also want to have access to WebAlbums
            var scopes = new[] { "https://picasaweb.google.com/data/" };

            var request = OAuthRequest.BuildLoopbackRequest(scopes);
            var listener = new HttpListener();
            listener.Prefixes.Add(request.RedirectUri);
            listener.Start();

            // note: add a reference to System.Windows.Presentation and a 'using System.Windows.Threading' for this to compile
            await this.Dispatcher.BeginInvoke(new Action(() => { this.Wb.Navigate(request.AuthorizationRequestUri); }));

            // here, we'll wait for redirection from our hosted webbrowser
            var context = await listener.GetContextAsync();

            // browser has navigated to our small http servern answer anything here
            string html = "<html><body></body></html>";
            var buffer = Encoding.UTF8.GetBytes(html);
            context.Response.ContentLength64 = buffer.Length;
            var stream = context.Response.OutputStream;
            var responseTask = stream.WriteAsync(buffer, 0, buffer.Length).ContinueWith(task =>
            {
                stream.Close();
                listener.Stop();
            });

            string error = context.Request.QueryString["error"];
            if (error != null)
                return;

            string state = context.Request.QueryString["state"];
            if (state != request.State)
                return;

            string code = context.Request.QueryString["code"];
            this.State.Token = request.ExchangeCodeForAccessToken(code);
        }
    }
}