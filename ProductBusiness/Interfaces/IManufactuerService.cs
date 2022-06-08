using ProductData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBusiness.Interfaces
{
    public interface IManufactuerService
    {
        IEnumerable<Manufacturer> GetAllManufacturers();
        void AddManufacturer(Manufacturer manufacturer);
        Manufacturer GetManufacturerByID(int id);
        void UpdateManufacturer(Manufacturer manufacturer);
    }
}
