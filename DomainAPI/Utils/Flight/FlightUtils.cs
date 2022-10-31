using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using System;
using System.IO;

namespace DomainAPI.Utils.FlightUtils
{
    public abstract class FlightUtils
    {
        public static IConfigurationRoot Configuration { get; set; }

        public static bool DepartureValidator(DateTime departuredate)
        {
            if (DateTime.Compare(departuredate, System.DateTime.Now) > 0)
                return true;
            else return false;
        }

        public static bool DateOpenCompanyValidator(DateTime opendate)
        {
            if (DateTime.Compare(opendate, System.DateTime.Now.AddMonths(-6)) < 0)
                return true;
            else return false;
        }

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
