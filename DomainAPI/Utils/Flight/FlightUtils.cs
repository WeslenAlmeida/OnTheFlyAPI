using DomainAPI.Models.Aircraft;
using Newtonsoft.Json;
using System;

namespace DomainAPI.Utils.FlightUtils
{
    public abstract class FlightUtils
    {
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

        public static string GetAirportWEBAPIUri()
        {
            dynamic JsonConfig = JsonConvert.DeserializeObject("appsettings.json");
            return JsonConfig.DatabaseSettings.ApiWebAirportUri;
        }

        public static string GetAirportAPIUri()
        {
            dynamic JsonConfig = JsonConvert.DeserializeObject("appsettings.json");
            return JsonConfig.DatabaseSettings.ApiAirportUri;
        }
    }
}
