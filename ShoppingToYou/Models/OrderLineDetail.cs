using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;

namespace ShoppingToYou.Models
{
    public class OrderLineDetail
    {
        public int ID { get; set; }
        public int OrderQuantity { get; set; }
        public string ProductDescription { get; set; }
        public double ProductPrice { get; set; }
        public int CustomerId { get; set; }

    }
}
