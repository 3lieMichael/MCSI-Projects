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
    public class UserDAL
    {
        public static List<User> GetUsers()
        {
            var db = new MyDBContext();
            try
            {
                return db.Users.ToList() ?? new List<User>();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public static void AddUser(User newUser)
        {
            var db = new MyDBContext();
            try
            {
                db.Users.Add(newUser);
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void UpdateUser(User user)
        {
            var db = new MyDBContext();
            try
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void DeleteUser(User user)
        {
            var db = new MyDBContext();
            try
            {
                db.Users.Remove(user);
                db.SaveChanges();
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}