using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHN_Website.Models
{
    public class Sale
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public double TotalSellingPrice { get; set; }
    }
}