using Airport.Data.Interface;

namespace Airport.Data
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string AirportsCollectionName { get; set; }
        public string AirportsTrashCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
