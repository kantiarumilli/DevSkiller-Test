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
    public class ProductService : IProductService
    {
        private IProductRepository _productRepository;
        private IManufacturerRepository _manufacturerRepository;
        public ProductService(IProductRepository productRepository, IManufacturerRepository manufacturerRepository)
        {
            _productRepository = productRepository;
            _manufacturerRepository = manufacturerRepository;
        }

        public Dictionary<string, string> AddProduct(Product product)
        {
            var ret = ValidateProduct(product);
            if (ret?.Count > 0) return ret;

            if(_productRepository.GetProductByName(product.Name)!=null)
            {
                throw new Exception("Product Already Exists");
            }
            _productRepository.AddProduct(product);

            return ret;
        }

        public IEnumerable<Product> GetAllProducts()
        {
            var products = _productRepository.GetAllProducts();
            foreach(var product in products)
            {
                //TODO - Slow performance, needs to be optimized.
                product.ManufacturerName = _manufacturerRepository.GetManufacturerByID(product.ManufacturerId)?.Name;
            }
            return products;
        }

        public Product GetProductByID(int id)
        {
            var product = _productRepository.GetProductByID(id);
            product.ManufacturerName = _manufacturerRepository.GetManufacturerByID(product.ManufacturerId)?.Name;
            return product;
        }

        public Dictionary<string, string> UpdateProduct(Product product)
        {
            var ret = ValidateProduct(product);
            if (ret?.Count > 0) return ret;

            var existing = _productRepository.GetProductByName(product.Name);
            if (existing?.Id != product.Id)
            {
                throw new Exception("Another product with same name exists.");
            }

            _productRepository.UpdateProduct(product);

            return ret;
        }

        public Dictionary<string, string> ValidateProduct(Product product)
        {
            var ret = new Dictionary<string, string>();
            if (!ValidateProductName(product)) ret.Add(nameof(product.Name), "Error: Product Name cannot have manufacturer's name in the name field");

            return ret;
        }

        private bool ValidateProductName(Product product)
        {
            var manufacturer = _manufacturerRepository.GetManufacturerByID(product.ManufacturerId);
            if (product.Name.ToLower().Contains(manufacturer.Name.ToLower())) return false;

            return true;
        }
    }
}
