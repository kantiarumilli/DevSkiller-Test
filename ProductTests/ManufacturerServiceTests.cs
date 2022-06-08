using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using ProductBusiness.Services;
using ProductData.Interfaces;
using ProductData.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProductTests
{
    [TestClass]
    public class ManufacturerServiceTests
    {
        private Mock<IManufacturerRepository> _mockManufacturerRepository;

        [TestInitialize]
        public void Setup()
        {
            var manufacturers = new List<Manufacturer>();
            manufacturers.Add(new Manufacturer { Id = 1, Name = "One" });
            manufacturers.Add(new Manufacturer { Id = 2, Name = "Two" });
            var manufacturersArr = manufacturers.ToArray();
            _mockManufacturerRepository = new Mock<IManufacturerRepository>();
            _mockManufacturerRepository.Setup(m => m.GetManufacturerByID(1)).Returns(manufacturersArr[0]);
            _mockManufacturerRepository.Setup(m => m.GetManufacturerByID(2)).Returns(manufacturersArr[1]);
            _mockManufacturerRepository.Setup(m => m.GetManufacturerByName("One")).Returns(manufacturers[0]);
        }

        [TestMethod]
        public void ValidNewManufacturerIsAdded()
        {
            _mockManufacturerRepository.Setup(m => m.AddManufacturer(It.IsAny<Manufacturer>())).Verifiable();

            var manufacturerService = new ManufacturerService(_mockManufacturerRepository.Object);

            manufacturerService.AddManufacturer(new Manufacturer { Id = 3, Name = "Three" });

            _mockManufacturerRepository.Verify();
        }
        [TestMethod]
        public void DuplicateManufacturerIsNotAdded()
        {
            _mockManufacturerRepository.Setup(m => m.AddManufacturer(It.IsAny<Manufacturer>())).Verifiable();

            var manufacturerService = new ManufacturerService(_mockManufacturerRepository.Object);

            Assert.ThrowsException<Exception>(delegate { manufacturerService.AddManufacturer(new Manufacturer { Id = 3, Name = "One" }); });

            _mockManufacturerRepository.Verify(r => r.AddManufacturer(It.IsAny<Manufacturer>()), Times.Never);
        }

        [TestMethod]
        public void DuplicateManufacturerNameIsNotUpdated()
        {
            _mockManufacturerRepository.Setup(m => m.UpdateManufacturer(It.IsAny<Manufacturer>())).Verifiable();

            var manufacturerService = new ManufacturerService(_mockManufacturerRepository.Object);

            Assert.ThrowsException<Exception>(delegate { manufacturerService.UpdateManufacturer(new Manufacturer { Id = 3, Name = "One" }); });

            _mockManufacturerRepository.Verify(r => r.AddManufacturer(It.IsAny<Manufacturer>()), Times.Never);
        }
    }
}
