using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RHN_Website.Helpers;
using RHN_Website.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;

namespace RHN_Website.DAL_SQL
{
    public static class ProductDAL
    {
        public static List<Product> GetProducts()
        {
            var db = new MyDBContext();
            try
            {
                return db.Products.ToList() ?? new List<Product>();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void AddProduct(Product newProduct)
        {
            var db = new MyDBContext();
            try
            {
                db.Products.Add(newProduct);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void UpdateProduct(Product Product)
        {
            var db = new MyDBContext();

            try
            {
                db.Entry(Product).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void DeleteProduct(Product Product)
        {
            var db = new MyDBContext();
            try
            {
                db.Entry<Product>(Product).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static Product GetProduct(Guid id)
        {
            var db = new MyDBContext();

            return db.Products.FirstOrDefault(p => p.Id == id); 
        }
    }
}