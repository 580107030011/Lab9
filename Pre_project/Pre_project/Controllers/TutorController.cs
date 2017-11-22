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

namespace Pre_project.Controllers
{
    public class TutorController : Controller
    {
        private PreprojectEntities db = new PreprojectEntities();

        public ActionResult CreatPost()
        {
            return View();
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult CreatPost(string note, string description, string subject, string post_name, string username_post, string price, string sex)
        {
            Post_Tutor post = new Post_Tutor();

            if (description == "")
            {
                ViewBag.alertPost = "กรุณากรอกข้อความ";
                return View();
            }
            if (username_post != null)
            {
                post.Tutor = username_post;
                post.Post_name = post_name;
                post.Description = description;
                post.Note = note;
                post.Subject = subject;
                post.Price = price;
                post.Sex = Request.Cookies["C_Sex"].Value;

                try
                {
                    db.Post_Tutor.Add(post);
                    db.SaveChanges();
                    return RedirectToAction("MyPost", "Tutor");
                }
                catch (DbEntityValidationException ex)
                {
                    var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                    var fullErrorMessage = string.Join("; ", errorMessages);
                    var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                    throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                }
            }
            return View();
        }

        public ActionResult MyPost()
        {
            string username = Request.Cookies["C_Username"].Value;
            string text = "'" + username + "'";
            string query = "SELECT * FROM Post_Tutor WHERE Tutor = " + text;
            var mypost = db.Database.SqlQuery<Post_Tutor>(query).ToList();
            return View(mypost);
        }

        public ActionResult ReadPost(int id)
        {
            string query = "SELECT * FROM Post_Tutor WHERE Post_no = " + id;
            var post = db.Database.SqlQuery<Post_Tutor>(query).SingleOrDefault();
            return View(post);
        }

        public ActionResult ProfileTutor(string username)
        {
            string text = "'" + username + "'";
            string query = "SELECT * FROM Tutor WHERE Username = " + text;
            var profile = db.Database.SqlQuery<Tutor>(query).SingleOrDefault();
            return View(profile);
        }

        public ActionResult EditProfile(string username)
        {
            string text = "'" + username + "'";
            string query = "SELECT * FROM Tutor WHERE Username = " + text;
            var profile = db.Database.SqlQuery<Tutor>(query).SingleOrDefault();
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(HttpPostedFileBase file,string new_firstname, string new_lastname, string new_sex, string new_tel, string new_email, string new_education, string new_price, string new_image)
        {
            string new_username = Request.Cookies["C_Username"].Value;
            Tutor update = db.Tutors.Where(a => a.Username.Equals(new_username)).FirstOrDefault();
            update.Username = Request.Cookies["C_Username"].Value;
            update.Tutor_name = new_firstname;
            update.Tutor_lastname = new_lastname;
            update.Sex = new_sex;
            update.Tel = new_tel;
            update.Email = new_email;
            update.Education = new_education;
            update.Price = new_price;

            if (file != null)
            {
                if (file.ContentType != "image/jpeg" && file.ContentType != "image/png")
                {
                    ViewBag.Message1 = "XXXXX";
                    return View();
                }
                string ImageName = Path.GetFileName(file.FileName);

                if (file.ContentType == "image/jpeg")
                {
                    ImageName = new_username + ".jpg";
                }
                else if (file.ContentType == "image/png")
                {
                    ImageName = new_username + ".png";
                }

                string path = Server.MapPath("~/Uploadfiles/" + ImageName);

                file.SaveAs(path);
                update.Image = "~/Uploadfiles/" + ImageName;
            }

            if (update != null)
            {
                db.SaveChanges();
            }
            return RedirectToAction("ProfileStudent", "Student", new { username = new_username });
        }

        public ActionResult ChangePasswordT()
        {
            return View();

        }
        // GET: ChangePassword
        [HttpPost]
        public ActionResult ChangePasswordT(string username_old, string password_old, string password_new, string Comfirm_new)
        {
            if (password_old != null)
            {
                var check_username = db.Tutors.Where(a => a.Username.Equals(username_old)).FirstOrDefault<Tutor>();
                if (check_username.Password == password_old)
                {
                    if (password_new == Comfirm_new)
                    {
                        check_username.Password = password_new;
                        try
                        {
                            db.SaveChanges();
                        }
                        catch (DbEntityValidationException ex)
                        {
                            var errorMessages = ex.EntityValidationErrors.SelectMany(x => x.ValidationErrors).Select(x => x.ErrorMessage);
                            var fullErrorMessage = string.Join("; ", errorMessages);
                            var exceptionMessage = string.Concat(ex.Message, " The validation errors are: ", fullErrorMessage);
                            throw new DbEntityValidationException(exceptionMessage, ex.EntityValidationErrors);
                        }
                    }
                    else
                    {
                        ViewBag.Error1 = "Password don't match";
                        return View();
                    }
                }
                else
                {
                    ViewBag.Error2 = "Password is wrong !!!";
                    return View();
                }
            }
            return RedirectToAction("ProfileTutor", "Tutor", new { username = username_old });
        }
    }
}