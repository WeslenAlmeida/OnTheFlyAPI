using DomainAPI.Database.Passenger.Interface;
using DomainAPI.Dto.Passenger;
using DomainAPI.Models.Passenger;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DomainAPI.Services.Passenger
{
    public class PassengerServices
    {

        private readonly IMongoCollection<Passengers> _passenger;
        private readonly IMongoCollection<Passengers> _removedPassenger;
        private readonly IMongoCollection<Passengers> _restrictPassenger;


        public PassengerServices(IDatabaseSettings settings)
        {

            var passenger = new MongoClient(settings.ConnectionString);
            var dbPassanger = passenger.GetDatabase(settings.DatabaseName);
            _passenger = dbPassanger.GetCollection<Passengers>(settings.PassengerCollectionName);

            var remove = new MongoClient(settings.ConnectionString);
            var dbRemoved = remove.GetDatabase(settings.DatabaseName);
            _removedPassenger = dbRemoved.GetCollection<Passengers>(settings.RemovedPassengerCollectionName);

            var restrict = new MongoClient(settings.ConnectionString);
            var dbRestrict = remove.GetDatabase(settings.DatabaseName);
            _restrictPassenger = dbRestrict.GetCollection<Passengers>(settings.RestrictPassengerCollectionName);

        }

        public Passengers CreateDTO(PassengerDTO passengerDTO)
        {
            Passengers passenger = new();
            passenger.Cpf = passengerDTO.Cpf;
            passenger.Name = passengerDTO.Name;
            passenger.Gender = passengerDTO.Gender;
            passenger.Phone = passengerDTO.Phone;
            passenger.DtBirth = passengerDTO.DtBirth;
            passenger.DtRegister = passengerDTO.DtRegister;
            passenger.Status = passengerDTO.Status;
            passenger.Address = passengerDTO.Address;
            _passenger.InsertOne(passenger);
            return passenger;
        }

        public Passengers Create(Passengers passenger)
        {
            _removedPassenger.InsertOne(passenger);
            return passenger;
        }

        public Passengers CreateRestrict(Passengers passenger)
        {
            _restrictPassenger.InsertOne(passenger);
            return passenger;
        }

        public List<Passengers> Get() => _passenger.Find<Passengers>(passenger => true).ToList();
        public List<Passengers> GetValids() => _passenger.Find<Passengers>(passenger => passenger.Status == true).ToList();

        public Passengers GetPassenger(string cpf) => _passenger.Find<Passengers>(passenger => passenger.Cpf == cpf).FirstOrDefault();
        public void Update(string cpf, Passengers passengerIn)
        {
            _passenger.ReplaceOne(passenger => passenger.Cpf == cpf, passengerIn);
        }
        public void UpdateRestrict(string cpf, Passengers passengerIn)
        {
            _passenger.ReplaceOne(passenger => passenger.Cpf == cpf, passengerIn);
        }

        public void Remove(Passengers passengerIn) => _passenger.DeleteOne(passenger => passenger.Cpf == passengerIn.Cpf);
        public void RemoveRestrict(Passengers passengerIn) => _restrictPassenger.DeleteOne(passenger => passenger.Cpf == passengerIn.Cpf);
    }
}
