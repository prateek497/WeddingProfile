using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biodata.Database;
using biodata.Database.Tables;
using biodata.Models;
using Microsoft.Ajax.Utilities;

namespace biodata.Controllers
{
    public class InformationController : Controller
    {
        [HttpGet]
        public ActionResult Contact()
        {
            return View(new Contact
            {
                RelationshipList = Support.RelationshipList()
            });
        }

        [HttpPost]
        public ActionResult Contact(Contact model)
        {
            if (model == null) return null;

            //if (ModelState.IsValid)
            //{
                var entities = new BiodataDb();
                entities.Contactinfoes.Add(new ContactInfo
                {
                    Email = model.Email,
                    Name = model.Name,
                    Relationship = model.RelationshipText,
                    City = model.City,
                    ContactNumber = model.Phone,
                    State = model.State,
                    UserId = GetUserId(model.Email, entities)
                });

                entities.SaveChanges();
                return RedirectToAction("Contact");
            //}

            return View(model);
        }

        public ActionResult Personal()
        {
            return View();
        }

        public ActionResult Religious()
        {
            return View();
        }

        public ActionResult Education()
        {
            return View();
        }

        public ActionResult Career()
        {
            return View();
        }

        public ActionResult Family()
        {
            return View();
        }

        public ActionResult Pictures()
        {
            return View();
        }

        public ActionResult Download()
        {
            return View();
        }

        public int GetUserId(string email, BiodataDb entities)
        {
            var user = entities.Users.FirstOrDefault(x => x.Email.Equals(email));
            if (user != null) return user.Id;
            return 0;
        }
    }
}
