using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BusinessCard.Models;

namespace BusinessCard.Controllers
{
    public class AuthController : Controller
    {
        BCDBEntities db = new BCDBEntities();
        
        // GET: Auth
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(Users user)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    var myUser = db.Users.Where(u => u.username == user.username).FirstOrDefault();
                    var type = myUser.type;

                    if (type == "Admin")
                    {
                        var admin = db.Admins.Where(u => u.Id == myUser.Id).FirstOrDefault();
                        if (admin == null)
                        {
                            return View();
                        }
                        else
                        {
                            var hashPassword = HashPassword(user.password);

                            if (admin.hashPassword != hashPassword)
                            {
                                return View();
                            }
                            else
                            {
                                admin.lastLogin = DateTime.Now;
                                var LoginAdmin = EditAdmin(admin);
                                return RedirectToAction("Index", "Admin", LoginAdmin);
                            }
                        }

                    }
                    else
                    {
                        var client = db.Clients.Where(c => c.Id == myUser.Id).FirstOrDefault();
                        if (client == null)
                        {
                            return View();
                        }
                        else
                        {
                            var hashPassword = HashPassword(user.password);

                            if (client.hashPassword != hashPassword)
                            {
                                return View();
                            }
                            else
                            {
                                client.lastLogin = DateTime.Now;
                                var LoginClient = EditClient(client);
                                return RedirectToAction("Index", "Client", LoginClient);
                            }
                        }
                    }
                }
            }
            catch (Exception ex) 
            {
                // Log the exception
                Console.WriteLine(ex.Message);
                // Optionally, add user-friendly feedback
                ViewBag.ErrorMessage = "An error occurred during login. Please try again.";
                return View();
            }
            return View();
        }
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Register(MyClient client)
        {
            if (ModelState.IsValid)
            {
                User user = new User();
                var currentUser =db.Users.Where(u => u.username == client.username).FirstOrDefault();
                if (currentUser == null)
                {
                    user.username = client.username;
                    user.type = "Client";
                    // Save the client to the database
                    db.Users.Add(user);
                    db.SaveChanges();

                    var model = MapClient(client);
                    var myUser = db.Users.Where(u => u.username == client.username).FirstOrDefault();
                    model.Id = myUser.Id;
                    db.Clients.Add(model);
                    db.SaveChanges();

                    // Redirect to a success page or login
                    return RedirectToAction("Login");
                }
                else 
                {
                    ModelState.AddModelError("username", "This username already exists.");
                    return View(client);
                }

            }

            // Return the same view if validation fails
            return View(client);
        }

        private string HashPassword(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                return Convert.ToBase64String(sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password)));
            }
        }
        private Client MapClient(MyClient myClient) 
        {
            Client client = new Client();
            client.email = myClient.email;
            client.phone = myClient.phone;
            client.hashPassword = HashPassword(myClient.password);
            client.username = myClient.username;
            client.name = myClient.name;

            return client;
        }
        public Admin EditAdmin(Admin admin)
        {
            db.Entry(admin).State = EntityState.Modified;
            db.SaveChanges();
            return admin;
        }
        public Client EditClient(Client client)
        {
            db.Entry(client).State = EntityState.Modified;
            db.SaveChanges();
            return client;
        }
    }
}