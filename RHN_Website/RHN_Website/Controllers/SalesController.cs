using RHN_Website.DAL_SQL;
using RHN_Website.Models;
using RHN_Website.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RHN_Website.Controllers
{
    public class SalesController : Controller
    {
        // GET: Sales
        public ActionResult Index()
        {
            var sales = SalesDAL.GetSales();
            var products = ProductDAL.GetProducts();
            var viewModelSales = new List<Sales_vm>();
            foreach (var sale in sales)
            {
                viewModelSales.Add(new Sales_vm()
                {
                    Id = sale.Id,
                    Product = products.FirstOrDefault(p => p.Id == sale.ProductId),
                    Date = sale.Date,
                    Quantity = sale.Quantity,
                    TotalSellingPrice = sale.TotalSellingPrice
                });
            }
            return View(viewModelSales.OrderByDescending(x => x.Date));
        }

        // GET: Sales/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Sales/Create
        public ActionResult Create()
        {
            return View(ProductDAL.GetProducts());
        }

        // POST: Sales/Create
        [HttpPost]
        public ActionResult Create(Sale Sale)
        {
            try
            {
                // TODO: Add insert logic here

                var productInStore = StoreDAL.GetStoredProduct(Sale.ProductId);
                productInStore.QuantityInSold += Sale.Quantity;
                productInStore.QuantityInStore -= Sale.Quantity;
                productInStore.QuantityNeeded += Sale.Quantity;
                productInStore.Finished = productInStore.QuantityInStore <= 0;
                StoreDAL.UpdateStore(productInStore);
                Sale.Id = Guid.NewGuid();
                Sale.TotalSellingPrice = ProductDAL.GetProduct(Sale.ProductId).SellingPrice * Sale.Quantity;
                SalesDAL.AddSale(Sale);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sales/Edit/5
        public ActionResult Edit(string id)
        {
            var SaleToEdit = SalesDAL.GetSales().FirstOrDefault(obj => obj.Id.ToString() == id);
            return View(SaleToEdit);
        }

        // POST: Sales/Edit/5
        [HttpPost]
        public ActionResult Edit(Sale SaleToEdit)
        {
            try
            {
                // TODO: Add update logic here
                SalesDAL.UpdateSale(SaleToEdit);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Sales/Delete/5
        public ActionResult Delete(string id)
        {
            var SaleToDeleted = SalesDAL.GetSales().FirstOrDefault(obj => obj.Id.ToString() == id);
            return View(SaleToDeleted);
        }

        // POST: Sales/Delete/5
        [HttpPost]
        public ActionResult Delete(Sale SaleToDeleted)
        {
            try
            {
                // TODO: Add delete logic here
                SalesDAL.DeleteSale(SaleToDeleted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
