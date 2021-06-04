using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using NS_Game_Engine;
using Blazora.Scripts;

namespace Blazora
{
    public class Program
    {
        public static int cell_size = 36;
        public static string base_adress_str;

        public static async Task Main (string[] args)
        {
            Game_Engine.setup ();
            Game_Engine.initialized = true;

            var builder = WebAssemblyHostBuilder.CreateDefault (args);
            builder.RootComponents.Add<App> ("app");

            Uri base_adress = new Uri (builder.HostEnvironment.BaseAddress);
            Program.base_adress_str = base_adress.AbsoluteUri;
            builder.Services.AddScoped (sp => new HttpClient { BaseAddress = base_adress });

            await builder.Build ().RunAsync ();
        }
    }
}
