using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductBusiness.Interfaces;
using ProductData.Models;
using ProductWebsiteMVC.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProductWebsiteMVC
{
    public class ProductController : Controller
    {
        private IProductService _productService;
        private readonly IManufactuerService _manufactuerService;
        private readonly IMapper mapper;

        public ProductController(IProductService productService, IManufactuerService manufactuerService, IMapper mapper)
        {
            _productService = productService;
            this._manufactuerService = manufactuerService;
            this.mapper = mapper;
        }
        public IActionResult Index()
        {
            // TODO Adding Search/Filtering feature, Sorting, Paging might help if the products list gets longer.
            // TODO For now, for a small list - web browser based CTRL + F might be easier
            // TODO Implementation details are TBD with product team, analyst
            var model = _productService.GetAllProducts();
            return View("Index", model);
        }
        public IActionResult Create()
        {
            var items = new List<SelectListItem>();
            var manufacturers = _manufactuerService.GetAllManufacturers();
            //TODO - Filtering of manufacturers should be allowed, if and when the manufacturers list becomes big.
            ViewData["Manufacturers"] = manufacturers;

            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(ProductViewModel productViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var prod = mapper.Map<Product>(productViewModel);
                    var ret = _productService.AddProduct(prod);
                    if (ret?.Count > 0)
                    {
                        foreach (var item in ret)
                        {
                            ModelState.AddModelError(item.Key, item.Value);
                        }
                        return View();
                    }

                    return Index();
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(productViewModel);
            }
        }
        public IActionResult Edit(int Id)
        {
            var product = _productService.GetProductByID(Id);
            var model = new ProductViewModel{ Name = product.Name, Description = product.Description, ManufacturerId = product.ManufacturerId };

            var manufacturers = _manufactuerService.GetAllManufacturers();
            //TODO - Filtering of manufacturers should be allowed, if and when the manufacturers list becomes big.
            ViewData["Manufacturers"] = manufacturers.AsEnumerable();

            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int Id, ProductViewModel productViewModel)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var prod = mapper.Map<Product>(productViewModel);
                    //TODO - Concurrency checking might be helpful
                    var ret = _productService.UpdateProduct(prod);

                    if (ret?.Count > 0) { 
                        foreach(var item in ret)
                        {
                            ModelState.AddModelError(item.Key, item.Value);
                        }
                        return View(); 
                    }
                    return Index();
                }
                return View();
            }
            catch (Exception ex)
            {
                ModelState.AddModelError(string.Empty, ex.Message);
                return View(productViewModel);
            }
        }
    }
}
