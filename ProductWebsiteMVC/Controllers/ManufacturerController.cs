using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProductBusiness.Interfaces;
using ProductData.Models;
using ProductWebsiteMVC.ViewModels;
using System;

namespace ProductWebsiteMVC.Controllers
{
    public class ManufacturerController : Controller
    {
        private readonly IManufactuerService _manufacturerService;
        private readonly IMapper mapper;

        public ManufacturerController(IManufactuerService manufacturerService, IMapper mapper)
        {
            this._manufacturerService = manufacturerService;
            this.mapper = mapper;
        }

        public IActionResult Index()
        {
            // TODO Adding Search/Filtering feature, Sorting, Paging might help if the manufacturers list gets longer.
            // TODO For now, for a small list - web browser based CTRL + F might be easier
            // TODO Implementation details are TBD with product team, analyst
            var model = _manufacturerService.GetAllManufacturers();
            return View("Index", model);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ManufacturerViewModel manufacturerViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _manufacturerService.AddManufacturer(new Manufacturer { Name = manufacturerViewModel.Name });
                    return Index();
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(manufacturerViewModel);
            }
        }

        public IActionResult Edit(int Id)
        {
            var manufacturer = _manufacturerService.GetManufacturerByID(Id);
            var model = new ManufacturerViewModel { Name = manufacturer.Name };
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, ManufacturerViewModel manufacturerViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var manf = mapper.Map<Manufacturer>(manufacturerViewModel);
                    //TODO - Concurrency checking might be helpful
                    _manufacturerService.UpdateManufacturer(manf);

                    return Index();
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(manufacturerViewModel);
            }
        }
    }
}