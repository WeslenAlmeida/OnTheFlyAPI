namespace Flight.Data.Interface
{
    public interface IDatabaseSettings
    {
        string FlightsCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
