using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Pre_project.Models;
using System.Data.Entity.Validation;
using System.IO;

namespace PreProject.Controllers
{
    public class RegisterController : Controller
    {
        private PreprojectEntities db = new PreprojectEntities();
        // GET: Register
        public ActionResult Register()
        {
            return View();
        }

        public ActionResult Tutor_Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Tutor_Register(HttpPostedFileBase file)
        {
            Tutor tutor = new Tutor ();
            string username = Request.Form["Username"];
            tutor.Username = username;
            tutor.Password = Request.Form["Password"];
            tutor.Tutor_name  = Request.Form["Tutor_name"];
            tutor.Tutor_lastname = Request.Form["Tutor_lastname"];
            tutor.Sex = Request.Form["Sex"];
            tutor.Tel  = Request.Form["Tel"];
            tutor.Email = Request.Form["Email"];
            tutor.Education = Request.Form["Education"];
            tutor.Price = Request.Form["Price"];
            tutor.Image = Request.Form["Image"];

            if (ModelState.IsValid)
            {
                if (username != null)
                {
                    var check2_Tutor = db.Tutors.Where(a => a.Username.Equals(username)).FirstOrDefault<Tutor>();
                    if (check2_Tutor != null)
                    {
                        ViewBag.Message = "Sorry, this account name has already been used. Please try again.";
                        return View();
                    }
                    else
                    {
                        try
                        {
                            if (file != null)
                            {
                                if(file.ContentType != "image/jpeg" && file.ContentType != "image/png")
                                {
                                    ViewBag.Message1 = "XXXXX";
                                    return View();
                                }
                               string ImageName = Path.GetFileName(file.FileName);

                                if (file.ContentType == "image/jpeg")
                                {
                                    ImageName = username + ".jpg";
                                }
                                else if(file.ContentType == "image/png")
                                {
                                    ImageName = username + ".png";
                                }

                                string path = Server.MapPath("~/Uploadfiles/" + ImageName);
                           
                                file.SaveAs(path);
                                tutor.Image = "~/Uploadfiles/" + ImageName;
                            }

                            db.Tutors.Add(tutor);
                            db.SaveChanges();                          

                            return RedirectToAction("Login", "Login");
                        }
                        catch (DbEntityValidationException ex)
                        {
                            var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                            var fullErrorMessage = string.Join("; ", errorMessages);
                            var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                            throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Index");
        }

        public ActionResult Student_Register()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Student_Register(Student student)
        {
            string username = Request.Form["Username"];
            student.Username = username;
            student.Password = Request.Form["Password"];
            student.Student_name = Request.Form["Student_name"];
            student.Student_lastname = Request.Form["Student_lastname"];
            student.Sex = Request.Form["Sex"];
            student.School = Request.Form["School"];
            student.Grade = Request.Form["Grade"];
            student.Email = Request.Form["Email"];

            if (ModelState.IsValid)
            {
                if (username != null)
                {
                    var check_student = db.Students.Where(a => a.Username.Equals(username)).FirstOrDefault<Student>();
                    if (check_student != null)
                    {
                        ViewBag.Message = "Sorry, this account name has already been used. Please try again.";
                        return View();
                    }
                    else
                    {
                        try
                        {
                            db.Students.Add(student);
                            db.SaveChanges();                           
                            return RedirectToAction("Login", "Login");
                        }
                        catch (DbEntityValidationException ex)
                        {
                            var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                            var fullErrorMessage = string.Join("; ", errorMessages);
                            var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                            throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                        }
                    }

                }

            }

            return RedirectToAction("Index", "Index");
        }
    }
}
    
