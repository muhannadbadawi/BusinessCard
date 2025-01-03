using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessCard.Models;

namespace BusinessCard.Controllers
{
    public class AdminController : Controller
    {
        BCDBEntities db = new BCDBEntities();

        // GET: Admin
        public ActionResult Index(Admin admin)
        {
            var clientCount = db.Clients.Count();
            var cardCount = db.Cards.Count();
            AuthenticatedAdmin authenticatedAdmin = new AuthenticatedAdmin();
            authenticatedAdmin.admin = admin;
            authenticatedAdmin.clientCount = clientCount.ToString();
            authenticatedAdmin.cardCount = cardCount.ToString();

            return View(authenticatedAdmin);
        }
        public ActionResult Logout()
        {
            return RedirectToAction("Login", "Auth");
        }
        public ActionResult Clients()
        {
            var clients = db.Clients.ToArray();
            return View(clients);
        }
        public ActionResult DeleteClient(Client client)
        {
            var myClient = db.Clients.Find(client.Id);
            var myUser = db.Users.Find(client.Id);

            db.Clients.Remove(myClient);
            db.Users.Remove(myUser);
            db.SaveChanges();

            var clients = db.Clients.ToArray();
            return View("Clients", clients);
        }
        public ActionResult EditClient(Client client)
        {
            var myClient = db.Clients.Find(client.Id);
            if (myClient == null)
            {
                return View("Error");
            }
            return View(myClient);
        }
        [HttpPost]
        public ActionResult Edit(Client client)
        {
             db.Entry(client).State = EntityState.Modified;
             db.SaveChanges();

            var clients = db.Clients.ToArray();

            return View("Clients", clients);
        }
    }
}