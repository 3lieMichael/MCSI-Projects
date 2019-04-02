using RHN_Website.DAL_SQL;
using RHN_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace RHN_Website.ViewModel
{
    public class Home_vm
    {
        private List<Product> _Products;
        private List<Sale> _Sales;
        private List<Store> _Store;

        public Home_vm()
        {
            _Sales = SalesDAL.GetSales();
            _Store = StoreDAL.GetStores();
            _Products = ProductDAL.GetProducts();

        }
        public List<Product> Products { get { return _Products; } }
        public List<SalesChart> Sales {
            get {
                var sales = new List<SalesChart>();
                foreach (var sale in _Sales)
                {
                    if (DateTime.Now.Month == sale.Date.Month && DateTime.Now.Year == sale.Date.Year)
                    {
                        var duplicate = sales.FirstOrDefault(s => s.SaleDate == sale.Date.ToShortDateString());
                        if (duplicate == null)
                        {
                            sales.Add(
                                new SalesChart
                                {
                                    ProductName = _Products.FirstOrDefault(p => p.Id == sale.ProductId).Name,
                                    SaleDate = sale.Date.ToShortDateString(),
                                    SalesAmount = sale.TotalSellingPrice
                                });
                        }
                        else
                        {
                            var index = sales.FindIndex(x => x == duplicate);
                            sales[index].SalesAmount += sale.TotalSellingPrice;
                        }
                    }
                }
                return sales.OrderBy(x => x.SaleDate).ToList();
            }
        }
        public List<Store> Store { get { return _Store; } }
        public double TotalSalesExpected { get { return GetTotalSalesExpected(); } }
        public double TotalSalesCurrent { get { return GetTotalSalesCurrent(); } }
        public double StockCapital { get { return GetStockCapital(); } }
        public double TotalProfit { get { return GetTotalProfit(); } }
        public double TotalCurrentProfit { get { return GetCurrentProfit(); } }

        private double GetTotalSalesExpected() {
            double result = 0;
            foreach(var product in _Products)
            {
                var inStore = _Store.FirstOrDefault(p => p.ProductId == product.Id);
                result += (product.SellingPrice * (inStore.QuantityInStore + inStore.QuantityNeeded));
            }
            return result;
        }
        private double GetTotalSalesCurrent() {
            double result = 0;
            foreach (var product in _Products)
            {
                var inStore = _Store.FirstOrDefault(p => p.ProductId == product.Id);
                result += (product.SellingPrice * inStore.QuantityInSold);
            }
            return result;
        }
        private double GetStockCapital() {
            double result = 0;
            foreach (var product in _Products)
            {
                var inStore = _Store.FirstOrDefault(p => p.ProductId == product.Id);
                result += (product.StockPrice * (inStore.QuantityInStore + inStore.QuantityNeeded));
            }
            return result;
        }
        private double GetTotalProfit() {
            return GetTotalSalesExpected() - GetStockCapital();
        }
        private double GetCurrentProfit()
        {
            double result = 0;
            foreach (var product in _Products)
            {
                var inStore = _Store.FirstOrDefault(p => p.ProductId == product.Id);
                result += product.Profit * inStore.QuantityNeeded;
            }
            return result;
        }
    }
}