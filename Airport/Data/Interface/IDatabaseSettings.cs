namespace Airport.Data.Interface
{
    public interface IDatabaseSettings
    {
        string AirportsCollectionName { get; set; }
        string AirportsTrashCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
