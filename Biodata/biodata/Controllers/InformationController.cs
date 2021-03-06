using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using biodata.Database;
using biodata.Database.Tables;
using biodata.Helper;
using biodata.Models;
using Microsoft.Ajax.Utilities;
using biodata.Filter;

namespace biodata.Controllers
{
    [Authorize]
    public class InformationController : Controller
    {
        [HttpGet]
        [NoDirectAccess]
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
                return RedirectToAction("Pictures");
            }
            return View(model);
        }

        [HttpGet]
        [NoDirectAccess]
        public ActionResult Personal()
        {
            using (var entities = new BiodataDb())
            {
                int userId = Support.GetUserId(User.Identity.Name, entities);
                if (userId > 0) Support.DeleteExistingDataForUser(userId);
            }

            return View(new Personal
            {
                ComplexionList = Support.ComplexionList(),
                HeightList = Support.HeightList(),
                MaritalStatusList = Support.MaritalStatusList(),
                SmokeList = Support.SmokeList(),
                DrinkList = Support.DrinkList(),
                DietList = Support.DietList()
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
                    Diet = model.Diet,
                    Drink = model.Drink,
                    Hobbies = model.Hobbies,
                    Smoke = model.Smoke,
                    MaritalStatus = model.MaritalStatus,
                    Facebook = model.Facebook,
                    Instagram = model.Instagram,
                    Linkedin = model.Linkedin,
                    Twitter = model.Twitter,
                    Quora = model.Quora,
                    UserId = Support.GetUserId(User.Identity.Name, entities)
                });
                entities.SaveChanges();
                return RedirectToAction("Religious");
            }


            return RedirectToAction("Religious");
        }

        [HttpGet]
        [NoDirectAccess]
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
                    MotherTongue = model.MotherTongue,
                    UserId = Support.GetUserId(User.Identity.Name, entities)
                });
                entities.SaveChanges();
            }

            return RedirectToAction("Education");
        }

        [HttpGet]
        [NoDirectAccess]
        public ActionResult Education()
        {
            ViewBag.EducationPostBackAction = "Education";

            var entities = new BiodataDb();
            int userId = Support.GetUserId(User.Identity.Name, entities);
            var educationList = entities.Educationinfoes.Where(x => x.UserId == userId).Select(y =>
                new EducationFields
                {
                    Id = y.Id,
                    College = y.College,
                    University = y.University,
                    Degree = y.Degree,
                    Year = y.Year,
                    EducationQualText = y.EducationQualification
                }).ToList();

            return View(new Educations
            {
                EducationFieldses = educationList,
                Education = new EducationFields { EducationList = Support.EducationList().Except(educationList.Select(x => x.EducationQualText)).ToList() }
            });
        }

        [HttpPost]
        public ActionResult Education(EducationFields model)
        {
            if (model == null) return null;

            if (ModelState.IsValid)
            {
                var entities = new BiodataDb();
                entities.Educationinfoes.Add(new EducationInfo
                {
                    College = model.College,
                    Degree = model.Degree,
                    Year = model.Year,
                    EducationQualification = model.EducationQualText,
                    University = model.University,
                    UserId = Support.GetUserId(User.Identity.Name, entities)
                });
                entities.SaveChanges();
            }

            return RedirectToAction("Education");
        }

        [HttpGet]
        public ActionResult EditEducation(int id)
        {
            ViewBag.EducationPostBackAction = "EditEducation";

            var entities = new BiodataDb();
            var editEdu = entities.Educationinfoes.Where(x => x.Id == id).Select(y =>
                new EducationFields
                {
                    Id = y.Id,
                    University = y.University,
                    Year = y.Year,
                    College = y.College,
                    Degree = y.Degree,
                    EducationQualText = y.EducationQualification
                }).FirstOrDefault();


            if (editEdu != null)
            {
                editEdu.EducationList = Support.EducationList();
                return View(editEdu);
            }
            return RedirectToAction("Education");
        }

        [HttpPost]
        public ActionResult EditEducation(EducationFields model)
        {
            if (model == null) return null;

            using (var entities = new BiodataDb())
            {
                var educationRecords = entities.Educationinfoes.SingleOrDefault(x => x.Id == model.Id);

                if (educationRecords != null)
                {
                    educationRecords.Degree = model.Degree;
                    educationRecords.Year = model.Year;
                    educationRecords.University = model.University;
                    educationRecords.College = model.College;
                    educationRecords.EducationQualification = model.EducationQualText;
                    educationRecords.UserId = Support.GetUserId(User.Identity.Name, entities);
                }
                entities.SaveChanges();
            }
            return RedirectToAction("Education");
        }

        public ActionResult DeleteEducation(int id)
        {
            using (var entities = new BiodataDb())
            {
                var deleteEducation = entities.Educationinfoes.FirstOrDefault(x => x.Id == id);
                if (deleteEducation != null) entities.Educationinfoes.Remove(deleteEducation);
                entities.SaveChanges();
            }
            return RedirectToAction("Education");
        }

        [HttpGet]
        [NoDirectAccess]
        public ActionResult Career()
        {

            return View(new Career
            {
                YesWorkExperience = true,
                AnnualIncomeList = Support.AnnualIncomeList()
            });
        }

        [HttpPost]
        public ActionResult Career(Career model)
        {
            if (model == null) return null;
            var entities = new BiodataDb();
            if (model.YesWorkExperience)
            {
                if (ModelState.IsValid)
                {
                    entities.Workexperienceinfoes.Add(new WorkExperienceInfo
                    {
                        Company = model.Company,
                        Designation = model.Designation,
                        Location = model.Location,
                        TotalExperience = (DateTime.Now - Convert.ToDateTime(model.WorkingFrom)).TotalDays.ToString(CultureInfo.InvariantCulture),
                        AnnualIncome = model.AnnualIncomeText,
                        IsWorkingExprience = model.YesWorkExperience,
                        UserId = Support.GetUserId(User.Identity.Name, entities)
                    });
                    entities.SaveChanges();
                }

                return RedirectToAction("Family");
            }

            entities.Workexperienceinfoes.Add(new WorkExperienceInfo
            {
                Company = null,
                Designation = null,
                Location = null,
                TotalExperience = null,
                AnnualIncome = null,
                IsWorkingExprience = model.YesWorkExperience,
                UserId = Support.GetUserId(User.Identity.Name, entities)
            });
            entities.SaveChanges();


            return RedirectToAction("Family");
        }

        [HttpGet]
        [NoDirectAccess]
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

            if (TempData["FamilyVal"] != null)
            {
                string error = Convert.ToString(TempData["FamilyVal"]);
                ModelState.AddModelError("", error);
            }

            if (familyList.Count > 0)
            {
                return View(
                    new Families
                    {
                        FamilyList = familyList,
                        FamilyMember = new Family { RelationshipList = Support.FamilyRelationshipList().ToList() }
                    }
                );
            }

            return View(new Families
            {
                FamilyList = new List<Family>(),
                FamilyMember = new Family { RelationshipList = Support.FamilyRelationshipList() }
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
            return View(new Families
            {
                FamilyList = new List<Family>(),
                FamilyMember = new Family()
            });
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
                editfamily.RelationshipList = Support.FamilyRelationshipList();
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
        [NoDirectAccess]
        public ActionResult Pictures()
        {
            var pictureList = new PictureList { PicList = new List<PictureModel>(), UserEmail = User.Identity.Name };

            if (TempData["ErrorMessage"] != null)
            {
                if (Convert.ToBoolean(TempData["ErrorMessage"]) == true)
                {
                    ModelState.AddModelError("", "pictures should not be more than 4");
                }
            }

            using (var entities = new BiodataDb())
            {
                var userId = Support.GetUserId(User.Identity.Name, entities);
                var pictures = entities.Pictures.Where(x => x.UserId == userId).ToList();
                if (pictures.Any())
                {
                    foreach (var pic in pictures)
                    {
                        var ms = Image.FromStream(new MemoryStream(pic.PictureBytes));
                        pictureList.PicList.Add(new PictureModel
                        {
                            Id = pic.Id,
                            PicBytes = pic.PictureBytes,
                            IsProfile = pic.IsProfile,
                            Height = ms.Height,
                            Width = ms.Width
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
                    int userid = Support.GetUserId(User.Identity.Name, entities);
                    var pictures = entities.Pictures.Count(x => x.UserId == userid);
                    if ((pictures + inputFileList.Count) > 4)
                    {
                        TempData["ErrorMessage"] = true;
                        return RedirectToAction("Pictures");
                    }

                    foreach (var file in inputFileList)
                    {
                        System.Drawing.Image sourceimage = System.Drawing.Image.FromStream(file.InputStream);

                        var heightDynamic = 250;
                        var widthDynamic = 250;
                        var aspectRatio = 0f;
                        if (sourceimage.Height > sourceimage.Width)
                        {
                            aspectRatio = (float)sourceimage.Height / (float)sourceimage.Width;
                            widthDynamic = 250;
                            heightDynamic = (int)(250 * aspectRatio);
                        }
                        else
                        {
                            aspectRatio = ((float)sourceimage.Width / (float)sourceimage.Height);
                            heightDynamic = 250;
                            widthDynamic = (int)(250 * aspectRatio);
                        }

                        //var bitmap = Support.CropImage(sourceimage, new Rectangle(0, 0, 300, 300));

                        //var bitmap = Support.FixedSize(sourceimage, 300, 300, "#ffffff");

                        var images = Support.ImageToByte(sourceimage);
                        var ms = Image.FromStream(new MemoryStream(images));
                        var pbitmap = Support.FixedSize(ms, widthDynamic, heightDynamic, "#FFFFFF");
                        var pimages = Support.ImageToByte(pbitmap);

                        entities.Pictures.Add(new Picture
                        {
                            PictureBytes = pimages, //Support.ConvertToBytes(file),
                            IsProfile = false,
                            UserId = userid
                        });
                    }
                    entities.SaveChanges();

                    var profile = entities.Pictures.Where(x => x.UserId == userid).OrderBy(x => x.Id).FirstOrDefault();
                    if (profile != null)
                    {
                        profile.IsProfile = true;
                        //var ms = Image.FromStream(new MemoryStream(profile.PictureBytes));
                        //var pbitmap = Support.FixedSize(ms, 300, 300, "#FFFFFF");
                        //var pimages = Support.ImageToByte(pbitmap);
                        //profile.PictureBytes = pimages;
                        entities.SaveChanges();
                    }
                }
            }

            return RedirectToAction("Pictures");
        }

        public ActionResult DeletePicture(int id)
        {
            using (var entities = new BiodataDb())
            {
                var deletePicture = entities.Pictures.FirstOrDefault(x => x.Id == id);
                if (deletePicture != null && deletePicture.IsProfile)
                {
                    entities.Pictures.Remove(deletePicture);
                    entities.SaveChanges();

                    int userid = Support.GetUserId(User.Identity.Name, entities);
                    var profile = entities.Pictures.Where(x => x.UserId == userid).OrderBy(x => x.Id).FirstOrDefault();
                    if (profile != null)
                    {
                        profile.IsProfile = true;
                        entities.SaveChanges();
                    }
                }
                else
                {
                    if (deletePicture != null) entities.Pictures.Remove(deletePicture);
                    entities.SaveChanges();
                }
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
                if (profilePicture != null)
                {
                    profilePicture.IsProfile = true;
                    var ms = Image.FromStream(new MemoryStream(profilePicture.PictureBytes));
                    var pbitmap = Support.FixedSize(ms, 300, 300, "#FFFFFF");
                    var pimages = Support.ImageToByte(pbitmap);
                    profilePicture.PictureBytes = pimages;
                }
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

        public ActionResult FamilyValidation()
        {
            using (var entities = new BiodataDb())
            {
                var userId = Support.GetUserId(User.Identity.Name, entities);
                var familiesCount = entities.Familyinfoes.Count(x => x.UserId == userId);
                if (familiesCount < 2)
                {
                    TempData["FamilyVal"] = "Please add atleast 2 family members";
                    return RedirectToAction("Family");
                }
                return RedirectToAction("Contact");
            }
        }
    }
}