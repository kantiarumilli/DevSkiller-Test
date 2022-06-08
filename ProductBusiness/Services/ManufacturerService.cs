using ProductBusiness.Interfaces;
using ProductData.Interfaces;
using ProductData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBusiness.Services
{
    public class ManufacturerService : IManufactuerService
    {
        private IManufacturerRepository _manufacturerRepository;
        public ManufacturerService(IManufacturerRepository manufacturerRepository)
        {
            _manufacturerRepository = manufacturerRepository;
        }

        public void AddManufacturer(Manufacturer manufacturer)
        {
            if (_manufacturerRepository.GetManufacturerByName(manufacturer.Name) != null)
            {
                throw new Exception("Manufacturer Already Exists");
            }
            _manufacturerRepository.AddManufacturer(manufacturer);
        }

        public IEnumerable<Manufacturer> GetAllManufacturers()
        {
            var manufacturers = _manufacturerRepository.GetAllManufacturers();
            return manufacturers;
        }

        public Manufacturer GetManufacturerByID(int id)
        {
            var manufacturer = _manufacturerRepository.GetManufacturerByID(id);
            return manufacturer;
        }

        public void UpdateManufacturer(Manufacturer manufacturer)
        {
            var existing = _manufacturerRepository.GetManufacturerByName(manufacturer.Name);
            if(existing.Id != manufacturer.Id)
            {
                throw new Exception("Another manufacturer with same name exists.");
            }

            _manufacturerRepository.UpdateManufacturer(manufacturer);
        }
    }
}
