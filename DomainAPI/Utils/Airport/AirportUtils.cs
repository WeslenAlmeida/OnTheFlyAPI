using System.IO;
using Microsoft.Extensions.Configuration;

namespace DomainAPI.Utils.Airport
{
    public class AirportUtils
    {
            public static IConfigurationRoot Configuration { get; set; }

            public static string GetAPIUri(string uriJsonName)
            {
                var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

                Configuration = builder.Build();
                return Configuration["DatabaseSettings:" + uriJsonName];
            }
    }
}
    