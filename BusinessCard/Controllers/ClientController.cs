using System;
using System.Collections.Generic;
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
                file.SaveAs(Server.MapPath("~/images/"+file.FileName));
                newCard.logoPath = file.FileName;
            }
            db.Cards.Add(newCard);
            db.SaveChanges();
            return RedirectToAction("ShowCard", newCard);
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
        public ActionResult Logout()
        {
            client = null;
            return RedirectToAction("Login", "Auth");
        }

    }
}