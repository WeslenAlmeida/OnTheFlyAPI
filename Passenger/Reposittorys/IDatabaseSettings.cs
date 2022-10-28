namespace Passenger.Reposittorys {
    public interface IDatabaseSettings {

        string PassengerCollectionName { get; set; }
        string RemovedPassengerCollectionName { get; set; }
        string RestrictPassengerCollectionName { get; set; }
        string ConnectionString { get; set; }
        string DatabaseName { get; set; }
    }
}
