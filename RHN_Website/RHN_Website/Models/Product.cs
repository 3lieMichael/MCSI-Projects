using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHN_Website.Models
{
    public class Product
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public double StockPrice { get; set; }
        public double SellingPrice { get; set; }
        public double Profit { get { return GetProfit(); } }
        public string Image { get; set; }
        public DateTime DateAdded { get; set; }

        private double GetProfit()
        {
            return SellingPrice - StockPrice;
        }
    }
}