using Flight.Data.Interface;

namespace Flight.Data
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string FlightsCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}