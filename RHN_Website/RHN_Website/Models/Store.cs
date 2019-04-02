using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHN_Website.Models
{
    public class Store
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public int QuantityInStore { get; set; }
        public int QuantityInSold { get; set; }
        public int QuantityNeeded { get; set; }
        public bool Finished { get; set; }
    }
}