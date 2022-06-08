using JsonFlatFileDataStore;
using ProductData.Interfaces;
using ProductData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Repositories
{
    public class ManufacturerRepository : IManufacturerRepository
    {
        private DataStore _store;
        public ManufacturerRepository()
        {
            _store = new DataStore("data.json");
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            var collection = _store.GetCollection<Manufacturer>();
            manufacturer.Id = collection.GetNextIdValue();
            collection.InsertOne(manufacturer);
        }

        public IEnumerable<Manufacturer> GetAllManufacturers()
        {
            var collection = _store.GetCollection<Manufacturer>();
            return collection.AsQueryable().OrderBy(c => c.Name);
        }
        public Manufacturer GetManufacturerByID(int id)
        {
            var collection = _store.GetCollection<Manufacturer>();
            return collection.AsQueryable().FirstOrDefault(m => m.Id == id);
        }

        public Manufacturer GetManufacturerByName(string name)
        {
            var collection = _store.GetCollection<Manufacturer>();
            return collection.AsQueryable().FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            var collection = _store.GetCollection<Manufacturer>();
            collection.UpdateOne(p => p.Id == manufacturer.Id, manufacturer);
        }
    }
}
