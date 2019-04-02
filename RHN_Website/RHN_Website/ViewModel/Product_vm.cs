using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHN_Website.ViewModel
{
    public class Product_vm
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double StockPrice { get; set; }
        public double SellingPrice { get; set; }
        public HttpPostedFileBase Image { get; set; }
    }
}