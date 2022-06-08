using Microsoft.VisualStudio.TestTools.UnitTesting;
using ProductData.Interfaces;
using ProductBusiness.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Moq;
using ProductData.Models;

namespace ProductTests
{
    [TestClass]
    public class ProductServiceTests
    {
        private Mock<IProductRepository> _mockRepo;
        private Mock<IManufacturerRepository> _mockManufacturerRepository;

        [TestInitialize]
        public void Setup()
        {
            var products = new List<Product>();
            products.Add(new Product { Id = 1, Name = "One", Description = "First", ManufacturerId = 1 });
            products.Add(new Product { Id = 2, Name = "Two", Description = "Second", ManufacturerId = 1 });
            products.Add(new Product { Id = 3, Name = "Three", Description = "Third", ManufacturerId = 2 });
            products.Add(new Product { Id = 4, Name = "Four", Description = "Fourth", ManufacturerId = 2 });
            var productsArr = products.ToArray();
            _mockRepo = new Mock<IProductRepository>();
            _mockRepo.Setup(r => r.GetProductByName("One")).Returns(productsArr[0]);
            _mockRepo.Setup(r => r.GetProductByName("Two")).Returns(productsArr[1]);
            _mockRepo.Setup(r => r.GetProductByName("Three")).Returns(productsArr[2]);
            _mockRepo.Setup(r => r.GetProductByName("Four")).Returns(productsArr[3]);

            var manufacturers = new List<Manufacturer>();
            manufacturers.Add(new Manufacturer { Id = 1, Name = "One" });
            manufacturers.Add(new Manufacturer { Id = 2, Name = "Two" });
            var manufacturersArr = manufacturers.ToArray();
            _mockManufacturerRepository = new Mock<IManufacturerRepository>();
            _mockManufacturerRepository.Setup(m => m.GetManufacturerByID(1)).Returns(manufacturersArr[0]);
            _mockManufacturerRepository.Setup(m => m.GetManufacturerByID(2)).Returns(manufacturersArr[1]);


            _mockRepo.Setup(r => r.GetAllProducts()).Returns(products);

        }
        [TestMethod]
        public void ValidNewProductIsAdded()
        {
            _mockRepo.Setup(r => r.AddProduct(It.IsAny<Product>())).Verifiable();

            var productService = new ProductService(_mockRepo.Object, _mockManufacturerRepository.Object);

            productService.AddProduct(new Product { Id = 5, Name = "Five", Description = "Fifth", ManufacturerId = 2 });

            _mockRepo.Verify();
        }
        [TestMethod]
        public void DuplicateProductIsNotAdded()
        {
            _mockRepo.Setup(r => r.AddProduct(It.IsAny<Product>())).Verifiable();

            var productService = new ProductService(_mockRepo.Object, _mockManufacturerRepository.Object);

            Assert.ThrowsException<Exception>(delegate { productService.AddProduct(new Product { Id = 5, Name = "Four", Description = "Fifth", ManufacturerId = 2 }); });
            
            _mockRepo.Verify(r=>r.AddProduct(It.IsAny<Product>()), Times.Never);
        }


        [TestMethod]
        public void ProductNameContainingManufacturerIsNotAdded()
        {
            _mockRepo.Setup(r => r.AddProduct(It.IsAny<Product>())).Verifiable();

            var productService = new ProductService(_mockRepo.Object, _mockManufacturerRepository.Object);

            var retVal = productService.AddProduct(new Product { Id = 6, Name = "One Two", Description = "Sixth", ManufacturerId = 2 });
            Assert.IsNotNull(retVal);
            Assert.AreEqual(1, retVal.Count);

            _mockRepo.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }


        [TestMethod]
        public void DuplicateProductIsNotUpdated()
        {
            _mockRepo.Setup(r => r.AddProduct(It.IsAny<Product>())).Verifiable();

            var productService = new ProductService(_mockRepo.Object, _mockManufacturerRepository.Object);

            Assert.ThrowsException<Exception>(delegate { productService.UpdateProduct(new Product { Id = 4, Name = "Three", Description = "Two", ManufacturerId = 2 }); });

            _mockRepo.Verify(r => r.AddProduct(It.IsAny<Product>()), Times.Never);
        }
    }
}
