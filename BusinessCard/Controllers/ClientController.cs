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
        // GET: Client
        public ActionResult Index(Client client)
        {
            return View(client);
        }
    }
}