using RHN_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHN_Website.ViewModel
{
    public class Sales_vm
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int Quantity { get; set; }
        public DateTime Date { get; set; }
        public double TotalSellingPrice { get; set; }
    }
}