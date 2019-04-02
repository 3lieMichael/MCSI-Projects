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
    public static class SalesDAL
    {
        public static List<Sale> GetSales()
        {
            var db = new MyDBContext();
            try
            {
                return db.Sales.ToList() ?? new List<Sale>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void AddSale(Sale newSale)
        {
            var db = new MyDBContext();
            try
            {
                db.Sales.Add(newSale);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void UpdateSale(Sale Sale)
        {
            var db = new MyDBContext();

            try
            {
                db.Entry(Sale).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void DeleteSale(Sale Sale)
        {
            var db = new MyDBContext();
            try
            {
                db.Sales.Remove(Sale);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}