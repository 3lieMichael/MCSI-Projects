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
    public class StoreDAL
    {
        public static List<Store> GetStores()
        {
            var db = new MyDBContext();
            try
            {
                return db.Store.ToList() ?? new List<Store>();
            }
            catch (Exception)
            {
                throw;
            }
        }
        public static void AddStore(Store newStore)
        {
            var db = new MyDBContext();
            try
            {
                db.Store.Add(newStore);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void UpdateStore(Store Store)
        {
            var db = new MyDBContext();

            try
            {
                db.Entry(Store).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void DeleteStore(Store Store)
        {
            var db = new MyDBContext();
            try
            {
                db.Entry(Store).State = EntityState.Deleted;
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static Store GetStoredProduct(Guid ProdID)
        {
            var db = new MyDBContext();
            return db.Store.FirstOrDefault(p => p.ProductId == ProdID);
        }
    }
}