using Company.Models;
using Company.Utils.Interface;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Company.Services
{
    public class RestrictedCompanyServices
    {
        private readonly IMongoCollection<RestrictedCompany> _restrictedCompany;

        public RestrictedCompanyServices(IDatabaseSettings settings)
        {
            var restrict = new MongoClient(settings.ConnectionString);
            var database = restrict.GetDatabase(settings.DatabaseName);
            _restrictedCompany = database.GetCollection<RestrictedCompany>(settings.RestrictedCollectionName);
        }

        public async Task<List<RestrictedCompany>> Get() => await _restrictedCompany.Find(restricted => true).ToListAsync();

        public async Task<RestrictedCompany> Get(string cnpj) => await _restrictedCompany.Find(restricted => restricted.CNPJ == cnpj).FirstOrDefaultAsync();

        public async Task Create(RestrictedCompany restrictedIn) => await _restrictedCompany.InsertOneAsync(restrictedIn);

        public async Task Put(string cnpj, RestrictedCompany restrictedIn) => await _restrictedCompany.ReplaceOneAsync(restricted => restricted.CNPJ == cnpj, restrictedIn);

        public async Task Remove(string cnpj) => await _restrictedCompany.DeleteOneAsync(restrict => restrict.CNPJ == cnpj);
    }
}
