using System.ComponentModel.DataAnnotations;

namespace ProductWebsiteMVC.ViewModels
{
    public class ManufacturerViewModel
    {
        [Required]
        [StringLength(30, ErrorMessage = "The name cannot be more than 30 characters long")]
        public string Name { get; set; }

    }
}
