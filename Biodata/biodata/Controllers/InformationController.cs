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
    [Authorize]
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

            if (ModelState.IsValid)
            {
                var entities = new BiodataDb();
                entities.Contactinfoes.Add(new ContactInfo
                {
                    Email = model.Email,
                    Name = model.Name,
                    Relationship = model.RelationshipText,
                    City = model.City,
                    ContactNumber = model.Phone,
                    State = model.State,
                    UserId = Support.GetUserId(User.Identity.Name, entities)
                });
                //entities.SaveChanges();
                return RedirectToAction("Personal");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Personal()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Personal(Personal model)
        {
            return RedirectToAction("Religious");
        }

        [HttpGet]
        public ActionResult Religious()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Religious(Religious model)
        {
            return RedirectToAction("Education");
        }

        [HttpGet]
        public ActionResult Education()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Education(Education model)
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
    }
}
