using Microsoft.AspNetCore.Http;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ShoppingToYou.Models
{
    public class OrderLines
    {
        public int ID { get; set; }
        [Required]
        public string OrderNumber { get; set; }
        public int OrderLine { get; set; }
        public int ProductID { get; set; }
        public double ProductPrice { get; set; }
        public string ProductDescription { get; set; }
        public int OrderQuantity { get; set; }
        [DisplayName("Image Name")]
        public string ImageName { get; set; }
        [DisplayName("Upload File")]
        public IFormFile ImageFile { get; set; }
    }
}
