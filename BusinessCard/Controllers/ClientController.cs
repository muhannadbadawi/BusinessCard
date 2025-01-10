using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessCard.Models;

namespace BusinessCard.Controllers
{
    public class ClientController : Controller
    {
        BCDBEntities db = new BCDBEntities();
        static Client client;
        // GET: Client
        public ActionResult Index(Client loginClient)
        {
            client = loginClient;
            var cards = db.Cards.Where(c => c.clientId == loginClient.Id).ToList();
            AuthenticatedClient authenticatedClient = new AuthenticatedClient();
            authenticatedClient.client = loginClient;
            authenticatedClient.cards = cards;
            return View(authenticatedClient);
        }
        public ActionResult NewCard(Client client)
        {
            var card = new Card
            {
                clientId = client.Id
            };
            return View(card);
        }

        public ActionResult CreateCard(Card newCard, HttpPostedFileBase file)
        {
            if (file != null)
            {
                file.SaveAs(Server.MapPath("~/images/" + file.FileName));
                newCard.logoPath = file.FileName;
            }
            db.Cards.Add(newCard);
            db.SaveChanges();
            return RedirectToAction("ShowCard", newCard);
        }
        public ActionResult Profile()
        {
            return View(client);
        }
        public ActionResult EditProfile(Client client)
        {
            var clientToUpdate = db.Clients.Find(client.Id);
            if (clientToUpdate == null)
            {
                return HttpNotFound(); // Handle not found case
            }
            clientToUpdate.username = client.username;
            clientToUpdate.email = client.email;
            clientToUpdate.phone = client.phone;
            clientToUpdate.name = client.name;
            clientToUpdate.hashPassword = HashPassword(client.hashPassword);

            db.Entry(clientToUpdate).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", clientToUpdate);
        }
        public ActionResult DeleteCard(Card card)
        {
            var cardToDelete = db.Cards.Find(card.Id);

            db.Cards.Remove(cardToDelete);
            db.SaveChanges();
            return RedirectToAction("Index", client);
        }
        public ActionResult ShowCard(Card card)
        {
            return View(card);
        }
        public ActionResult EditCard(Card card)
        {
            return View(card);
        }
        [HttpPost]
        public ActionResult Edit(Card card, HttpPostedFileBase file)
        {
            var cardToUpdate = db.Cards.Find(card.Id);
            if (cardToUpdate == null)
            {
                return HttpNotFound(); // Handle not found case
            }

            if (file != null)
            {
                string path = Server.MapPath("~/images/" + file.FileName);
                file.SaveAs(path);
                cardToUpdate.logoPath = file.FileName;
            }

            // Update only the properties that need to be changed
            cardToUpdate.phone = card.phone;
            cardToUpdate.email = card.email;
            cardToUpdate.clientId = card.clientId;
            cardToUpdate.companyName = card.companyName;
            cardToUpdate.jobTitle = card.jobTitle;
            cardToUpdate.Id = card.Id;
            cardToUpdate.location = card.location;
            // Add other properties as needed

            db.Entry(cardToUpdate).State = EntityState.Modified;
            db.SaveChanges();

            return RedirectToAction("Index", client);
        }
        public ActionResult Logout()
        {
            client = null;
            return RedirectToAction("Login", "Auth");
        }
        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }
    }
}