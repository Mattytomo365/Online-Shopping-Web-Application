using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingToYou.Models
{
    public class ProductDetails
    {
        public int ID { get; set; }
        [Required]
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public int Quantity { get; set; }
        public int ReOrderQuantity { get; set; }
        public int DefaultOrderQuantity { get; set; }
        [Required]
        public string ProductType { get; set; }
        [Required]
        public string ProductCode { get; set; }

        [DisplayName("Image Name")]
        public string ImageName { get; set; }
        [Required]
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
