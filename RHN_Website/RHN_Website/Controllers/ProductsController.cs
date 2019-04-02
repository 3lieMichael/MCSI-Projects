using RHN_Website.DAL_SQL;
using RHN_Website.Models;
using RHN_Website.ViewModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Drawing.Imaging;
using System.Text;

namespace RHN_Website.Controllers
{
    public class ProductsController : Controller
    {
        // GET: Products
        public ActionResult Index()
        {
            return View(ProductDAL.GetProducts().OrderBy(p => p.Name));
        }

        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            return View(ProductDAL.GetProduct(Guid.Parse(id)));
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Products/Create
        [HttpPost]
        public ActionResult Create(Product_vm productToAdd)
        {
            var reader = new StreamReader(productToAdd.Image.InputStream);
            try
            {
                byte[] buffer = new byte[productToAdd.Image.InputStream.Length];
                productToAdd.Image.InputStream.Read(buffer, 0, buffer.Length);
                //var ImageData = Encoding.Default.GetBytes(reader.ReadToEnd());
                var imgString = Convert.ToBase64String(buffer);
                // TODO: Add insert logic here

                ProductDAL.AddProduct(new Product()
                {
                    Id = Guid.NewGuid(),
                    Name = productToAdd.Name,
                    Description = productToAdd.Description,
                    StockPrice = productToAdd.StockPrice,
                    SellingPrice = productToAdd.SellingPrice,
                    Image = imgString,
                    DateAdded = DateTime.Now
                });

                return RedirectToAction("Create", "Store");
            }
            catch
            {
                return View();
            }
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            var productToEdit = ProductDAL.GetProducts().FirstOrDefault(obj => obj.Id.ToString() == id);
            return View(productToEdit);
        }

        // POST: Products/Edit/5
        [HttpPost]
        public ActionResult Edit(string id, Product_vm EditedProd)
        {
            var productToEdit = ProductDAL.GetProducts().FirstOrDefault(obj => obj.Id.ToString() == id);
            byte[] buffer;
            var imgString = "";
            if (EditedProd.Image != null)
            {
                buffer = new byte[EditedProd.Image.InputStream.Length];
                EditedProd.Image.InputStream.Read(buffer, 0, buffer.Length);
                imgString = Convert.ToBase64String(buffer);
            }
            try
            {
                // TODO: Add update logic here
                ProductDAL.UpdateProduct(new Product()
                {
                    Id = EditedProd.Id,
                    Name = EditedProd.Name,
                    Description = EditedProd.Description,
                    StockPrice = EditedProd.StockPrice,
                    SellingPrice = EditedProd.SellingPrice,
                    Image = imgString != productToEdit.Image && imgString != "" ? imgString : productToEdit.Image
                });

                return RedirectToAction("Index");
            }
            catch
            {
                var product = ProductDAL.GetProducts().FirstOrDefault(obj => obj.Id.ToString() == productToEdit.Id.ToString());
                return View(product);
            }
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            var productToDelete = ProductDAL.GetProducts().FirstOrDefault(obj => obj.Id.ToString() == id);
            var storeItemToDelete = StoreDAL.GetStoredProduct(productToDelete.Id);
            ProductDAL.DeleteProduct(productToDelete);
            StoreDAL.DeleteStore(storeItemToDelete);
            return RedirectToAction("Index");
        }

        // POST: Products/Delete/5
        [HttpPost]
        public ActionResult Delete(string id, Product productToDelete)
        {
            try
            {
                // TODO: Add delete logic here
                ProductDAL.DeleteProduct(productToDelete);
                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
