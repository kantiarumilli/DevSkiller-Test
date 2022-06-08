using ProductData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductBusiness.Interfaces
{
    public interface IProductService
    {
        IEnumerable<Product> GetAllProducts();
        Dictionary<string, string> AddProduct(Product product);
        Product GetProductByID(int id);
        Dictionary<string, string> UpdateProduct(Product product);
        Dictionary<string, string> ValidateProduct(Product product);
    }
}
