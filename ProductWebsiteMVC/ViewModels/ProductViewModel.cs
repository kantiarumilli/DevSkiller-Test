using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using ProductBusiness.Interfaces;

namespace ProductWebsiteMVC.ViewModels
{
    public class ProductViewModel
    {
        public int Id { get; set; }
        [Required][StringLength(50, ErrorMessage = "The name cannot be more than 50 characters long")]
        public string Name { get; set; }
        [StringLength(500, ErrorMessage = "The description cannot be more than 500 characters long")]
        public string Description { get; set; }
        public int ManufacturerId { get; set; }
        //public List<SelectListItem> Manufacturers { get
        //    {
        //        var items = new List<SelectListItem>();
        //        var manufacturers = manufactuerService.GetAllManufacturers();
        //        foreach(var item in manufacturers)
        //            items.Add(new SelectListItem(item.Name, item.Id.ToString()));
                
        //        return items;
        //    } }

    }
}
