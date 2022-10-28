namespace Saler.Utils
{
    public interface IDataBaseSettings
    {
        string SalesCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DataBaseName { get; set; }
    }
}
