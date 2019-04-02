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
    public class StoreController : Controller
    {
        // GET: Store
        public ActionResult Index()
        {
            var products = StoreDAL.GetStores();
            var store = new List<Store_vm>();
            foreach(var prod in products)
            {
                store.Add(new Store_vm()
                {
                    Id = prod.Id,
                    Product = ProductDAL.GetProduct(prod.ProductId),
                    QuantityInSold = prod.QuantityInSold,
                    QuantityInStore = prod.QuantityInStore,
                    QuantityNeeded = prod.QuantityNeeded,
                    Finished = prod.QuantityInStore == 0
                });
            }
            return View(store.OrderBy(p => p.Product.Name));
        }

        // GET: Store/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Store/Create
        public ActionResult Create()
        {
            return View(ProductDAL.GetProducts());
        }

        // POST: Store/Create
        [HttpPost]
        public ActionResult Create(Store item)
        {
            try
            {
                // TODO: Add insert logic here
                item.Id = Guid.NewGuid();
                item.QuantityInSold = 0;
                item.QuantityNeeded = 0;
                item.Finished = item.QuantityInStore <= 0;
                StoreDAL.AddStore(item);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Store/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Store/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Store/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Store/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
