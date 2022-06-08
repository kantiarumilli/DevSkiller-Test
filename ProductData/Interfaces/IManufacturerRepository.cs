using ProductData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductData.Interfaces
{
    public interface IManufacturerRepository
    {
        IEnumerable<Manufacturer> GetAllManufacturers();
        Manufacturer GetManufacturerByID(int id);
        Manufacturer GetManufacturerByName(string name);
        void AddManufacturer(Manufacturer manufacturer);
        void UpdateManufacturer(Manufacturer manufacturer);
    }
}
