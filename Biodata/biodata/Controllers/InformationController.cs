using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
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
                entities.SaveChanges();
                return RedirectToAction("Personal");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult Personal()
        {
            return View(new Personal
            {
                ComplexionList = Support.ComplexionList(),
                HeightList = Support.HeightList()
            });
        }

        [HttpPost]
        public ActionResult Personal(Personal model)
        {
            if (model == null) return null;

            if (ModelState.IsValid)
            {
                var entities = new BiodataDb();
                entities.Personalinfoes.Add(new PersonalInfo
                {
                    Name = model.Name,
                    Dob = Convert.ToDateTime(model.DateOfBirth),
                    DobTime = Convert.ToDateTime(model.TimeOfBirth),
                    Complexion = model.Complexion,
                    CurrentCity = model.CurrentCity,
                    Height = model.Height,
                    UserId = Support.GetUserId(User.Identity.Name, entities)
                });
                entities.SaveChanges();
                return RedirectToAction("Religious");
            }


            return RedirectToAction("Religious");
        }

        [HttpGet]
        public ActionResult Religious()
        {
            return View(new Religious
            {
                ReligionList = Support.ReligiousList(),
                ZodiacList = Support.ZodiacList(),
                LanguagesList = Support.LanguagesList()
            });
        }

        [HttpPost]
        public ActionResult Religious(Religious model)
        {
            if (model == null) return null;

            if (ModelState.IsValid)
            {
                var entities = new BiodataDb();
                entities.Culturalinfoes.Add(new CulturalInfo
                {
                    Caste = model.Caste,
                    Gotra = model.Gotra,
                    Languages = model.Languages,
                    Religion = model.Religion,
                    Zodiac = model.Zodiac,
                    UserId = Support.GetUserId(User.Identity.Name, entities)
                });
                entities.SaveChanges();
            }

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
            if (model == null) return null;

            if (ModelState.IsValid)
            {
                var entities = new BiodataDb();
                entities.Educationinfoes.Add(new EducationInfo
                {
                    GraduateCollege = model.BachelorCollegeOrUni,
                    GraduateDegree = model.BachelorDegree,
                    GraduatedYear = model.BachelorPassoutYear,
                    PostGraduateCollege = model.PgCollegeOrUni,
                    PostGraduateDegree = model.PgDegree,
                    PostGraduateYear = model.PgPassoutYear,
                    UserId = Support.GetUserId(User.Identity.Name, entities)
                });
                entities.SaveChanges();
            }

            return RedirectToAction("Career");
        }

        [HttpGet]
        public ActionResult Career()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Career(Career model)
        {
            if (model == null) return null;

            if (ModelState.IsValid)
            {
                var entities = new BiodataDb();
                entities.Workexperienceinfoes.Add(new WorkExperienceInfo
                {
                    Company = model.Company,
                    Designation = model.Designation,
                    Location = model.Location,
                    TotalExperience = (DateTime.Now - Convert.ToDateTime(model.WorkingFrom)).TotalDays.ToString(CultureInfo.InvariantCulture),
                    UserId = Support.GetUserId(User.Identity.Name, entities)
                });
                entities.SaveChanges();
            }

            return RedirectToAction("Family");
        }

        [HttpGet]
        public ActionResult Family()
        {
            ViewBag.PostBackAction = "Family";
            var entities = new BiodataDb();
            int userId = Support.GetUserId(User.Identity.Name, entities);
            var familyList = entities.Familyinfoes.Where(x => x.UserId == userId).Select(y =>
                new Family
                {
                    Id = y.Id,
                    City = y.City,
                    State = y.State,
                    Designation = y.Designation,
                    RelationshipText = y.Relationship,
                    CompanyName = y.Company,
                    JobLocation = y.Joblocation,
                    Name = y.Name,
                }).ToList();

            if (familyList.Count > 0)
            {
                return View(
                    new Families
                    {
                        FamilyList = familyList,
                        FamilyMember = new Family { RelationshipList = Support.RelationshipList() }
                    }
                );
            }

            return View(new Families
                {
                    FamilyList = new List<Family>(),
                    FamilyMember = new Family { RelationshipList = Support.RelationshipList() }
                });
        }

        [HttpPost]
        public ActionResult Family(Family model)
        {
            if (model == null) return null;

            if (ModelState.IsValid)
            {
                var entities = new BiodataDb();
                entities.Familyinfoes.Add(new FamilyInfo
                {

                    Name = model.Name,
                    Relationship = model.RelationshipText,
                    City = model.City,
                    State = model.State,
                    Designation = model.Designation,
                    Company = model.CompanyName,
                    Joblocation = model.JobLocation,
                    UserId = Support.GetUserId(User.Identity.Name, entities)
                });
                entities.SaveChanges();
                return RedirectToAction("Family");
            }
            return View(model);
        }

        [HttpGet]
        public ActionResult EditFamily(int id)
        {
            ViewBag.PostBackAction = "EditFamily";

            var entities = new BiodataDb();
            var editfamily = entities.Familyinfoes.Where(x => x.Id == id).Select(y =>
                new Family
                {
                    Id = y.Id,
                    City = y.City,
                    State = y.State,
                    Designation = y.Designation,
                    RelationshipText = y.Relationship,
                    CompanyName = y.Company,
                    JobLocation = y.Joblocation,
                    Name = y.Name
                }).FirstOrDefault();


            if (editfamily != null)
            {
                editfamily.RelationshipList = Support.RelationshipList();
                return View(editfamily);
            }
            return RedirectToAction("Family");
        }

        [HttpPost]
        public ActionResult EditFamily(Family model)
        {
            if (model == null) return null;

            using (var entities = new BiodataDb())
            {
                var familyRecords = entities.Familyinfoes.SingleOrDefault(x => x.Id == model.Id);

                if (familyRecords != null)
                {
                    familyRecords.City = model.City;
                    familyRecords.State = model.State;
                    familyRecords.Company = model.CompanyName;
                    familyRecords.Designation = model.Designation;
                    familyRecords.Joblocation = model.JobLocation;
                    familyRecords.Name = model.Name;
                    familyRecords.Relationship = model.RelationshipText;
                    familyRecords.UserId = Support.GetUserId(User.Identity.Name, entities);
                }
                entities.SaveChanges();
            }

            return RedirectToAction("Family");
        }

        public ActionResult DeleteFamily(int id)
        {
            using (var entities = new BiodataDb())
            {
                var deleteFamily = entities.Familyinfoes.FirstOrDefault(x => x.Id == id);
                if (deleteFamily != null) entities.Familyinfoes.Remove(deleteFamily);
                entities.SaveChanges();
            }
            return RedirectToAction("Family");
        }

        [HttpGet]
        public ActionResult Pictures()
        {
            var pictureList = new PictureList { PicList = new List<PictureModel>() };
            using (var entities = new BiodataDb())
            {
                var userId = Support.GetUserId(User.Identity.Name, entities);
                var pictures = entities.Pictures.Where(x => x.UserId == userId).ToList();
                if (pictures.Any())
                {
                    foreach (var pic in pictures)
                    {
                        pictureList.PicList.Add(new PictureModel
                        {
                            Id = pic.Id,
                            PicBytes = pic.PictureBytes,
                            IsProfile = pic.IsProfile
                        });
                    }
                }
            }

            return View(pictureList);
        }

        [HttpPost]
        public ActionResult Pictures(IEnumerable<HttpPostedFileBase> inputfile)
        {
            if (inputfile == null) return RedirectToAction("pictures");
            var inputFileList = inputfile as IList<HttpPostedFileBase> ?? inputfile.ToList();
            if (inputFileList.Count > 0)
            {
                using (var entities = new BiodataDb())
                {
                    foreach (var file in inputFileList)
                    {
                        entities.Pictures.Add(new Picture
                      {
                          PictureBytes = Support.ConvertToBytes(file),
                          IsProfile = false,
                          UserId = Support.GetUserId(User.Identity.Name, entities)
                      });
                    }
                    entities.SaveChanges();
                }
            }

            return RedirectToAction("Pictures");
        }

        public ActionResult DeletePicture(int id)
        {
            using (var entities = new BiodataDb())
            {
                var deletePicture = entities.Pictures.FirstOrDefault(x => x.Id == id);
                if (deletePicture != null) entities.Pictures.Remove(deletePicture);
                entities.SaveChanges();
            }
            return RedirectToAction("Pictures");
        }

        public ActionResult ProfilePicture(int id)
        {
            using (var entities = new BiodataDb())
            {
                var userId = Support.GetUserId(User.Identity.Name, entities);
                var pictures = entities.Pictures.Where(x => x.UserId == userId).ToList();

                var profilePicture = pictures.SingleOrDefault(x => x.Id == id);
                if (profilePicture != null) profilePicture.IsProfile = true;
                var normalPictures = pictures.Where(x => x.Id != id);
                foreach (var normal in normalPictures) normal.IsProfile = false;
                entities.SaveChanges();
            }
            return RedirectToAction("Pictures");
        }

        [HttpGet]
        public ActionResult Download()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Download(string temp)
        {
            return View();
        }
    }
}