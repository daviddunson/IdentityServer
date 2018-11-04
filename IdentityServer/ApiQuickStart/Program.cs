// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Program.cs" company="GSD Logic">
//   Copyright © 2018 GSD Logic. All Rights Reserved.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------

namespace ApiQuickStart
{
    using Microsoft.AspNetCore;
    using Microsoft.AspNetCore.Hosting;

    public class Program
    {
        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();

        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }
    }
}