namespace Company.Utils.Interface
{
    public interface IDatabaseSettings
    {
        public string CompanyCollectionName { get; set; }
        public string RestrictedCollectionName { get; set; }
        public string DeadfileCollectionName { get; set; }
        public string ConnectionString { get; set; }
        public string DatabaseName { get; set; }
    }
}
