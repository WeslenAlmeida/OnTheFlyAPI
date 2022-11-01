using DomainAPI.Models.Sale;
using MongoDB.Driver;
using System.Collections.Generic;
using System.Linq;
using DomainAPI.Database.Sale.Interface;
using System;
using DomainAPI.Utils.Passenger;
using DomainAPI.Models.Passenger;

namespace DomainAPI.Services.Sale
{
    public class SalesService
    {
        private readonly IMongoCollection<Sales> _sales;

        public SalesService(IDatabaseSettings settings)
        {
            var sales = new MongoClient(settings.ConnectionString);
            var database = sales.GetDatabase(settings.DataBaseName);
            _sales = database.GetCollection<Sales>(settings.SalesCollectionName);
        }

        public Sales Create(Sales sales) {
            _sales.InsertOne(sales);
            return sales;
        }

        public List<Sales> Get() => _sales.Find(sales => true).ToList();

        public Sales GetSpecificSale(Passengers passenger, DateTime date, string rab) =>
            _sales.Find<Sales>(sales => sales.Flight.Plane.RAB == rab && sales.Flight.Departure == date && sales.Passengers.Contains(passenger)).FirstOrDefault();

    }
}
