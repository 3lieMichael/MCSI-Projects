using RHN_Website.DAL_SQL;
using RHN_Website.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace RHN_Website.Controllers
{
    public class UsersController : Controller
    {
        // GET: Users
        public ActionResult Index()
        {
            return View(UserDAL.GetUsers());
        }

        // GET: Users/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        [HttpPost]
        public ActionResult Create(User user)
        {
            try
            {
                // TODO: Add insert logic here
                user.Id = Guid.NewGuid();
                UserDAL.AddUser(user);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            var userToEdit = UserDAL.GetUsers().FirstOrDefault(obj => obj.Id.ToString() == id);
            return View(userToEdit);
        }

        // POST: Users/Edit/5
        [HttpPost]
        public ActionResult Edit(User userToEdit)
        {
            try
            {
                // TODO: Add update logic here
                UserDAL.UpdateUser(userToEdit);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            var userToDeleted = UserDAL.GetUsers().FirstOrDefault(obj => obj.Id.ToString() == id);
            return View(userToDeleted);
        }

        // POST: Users/Delete/5
        [HttpPost]
        public ActionResult Delete(User userToDeleted)
        {
            try
            {
                // TODO: Add delete logic here
                UserDAL.DeleteUser(userToDeleted);

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
