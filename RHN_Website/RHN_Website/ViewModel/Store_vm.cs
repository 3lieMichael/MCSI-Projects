using RHN_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHN_Website.ViewModel
{
    public class Store_vm
    {
        public Guid Id { get; set; }
        public Product Product { get; set; }
        public int QuantityInStore { get; set; }
        public int QuantityInSold { get; set; }
        public int QuantityNeeded { get; set; }
        public bool Finished { get; set; }
    }
}