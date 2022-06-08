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
    public class ProductRepository : IProductRepository
    {
        private DataStore _store;
        public ProductRepository()
        {
            _store = new DataStore("data.json");
        }
        public void AddProduct(Product product)
        {
            var collection = _store.GetCollection<Product>();
            product.Id = collection.GetNextIdValue();
            collection.InsertOne(product);
        }
        private int GetNextID()
        {
            var collection = _store.GetCollection<Product>();
            return collection.AsQueryable().Max(c => c.Id) + 1;
        }
        public IEnumerable<Product> GetAllProducts()
        {
            var collection = _store.GetCollection<Product>();
            return collection.AsQueryable().OrderBy(c => c.Name);
        }

        public void UpdateProduct(Product product)
        {
            var collection = _store.GetCollection<Product>();
            collection.UpdateOne(p => p.Id == product.Id, product);
        }

        public Product GetProductByID(int id)
        {
            var collection = _store.GetCollection<Product>();
            return collection.AsQueryable().FirstOrDefault(p => p.Id == id);
        }
        public Product GetProductByName(string name)
        {
            var collection = _store.GetCollection<Product>();
            return collection.AsQueryable().FirstOrDefault(p => string.Equals(p.Name, name, StringComparison.InvariantCultureIgnoreCase));
        }
    }
}
