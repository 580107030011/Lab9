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

namespace PreProject.Controllers
{
    public class LoginController : Controller
    {
        private PreprojectEntities db = new PreprojectEntities();        
        // GET: Login
        public ActionResult Login()
        {
            string username = Request.Form["username"];
            string password = Request.Form["password"];

            if (ModelState.IsValid)
            {
                if (username != null & password != null)
                {
                    var check = db.Students.Where(a => a.Username.Equals(username) && a.Password.Equals(password)).FirstOrDefault();
                    var check2 = db.Tutors.Where(a => a.Username.Equals(username) && a.Password.Equals(password)).FirstOrDefault();

                    if (check != null)
                    {
                        var Cookie_Username = new HttpCookie("C_Username");
                        Cookie_Username.Value = check.Username;
                        Response.Cookies.Add(Cookie_Username);

                        var Cookie_Password = new HttpCookie("C_Password");
                        Cookie_Password.Value = check.Password;
                        Response.Cookies.Add(Cookie_Password);

                        var Cookie_Firstname = new HttpCookie("C_Firstname");
                        Cookie_Firstname.Value = check.Student_name;
                        Response.Cookies.Add(Cookie_Firstname);

                        var Cookie_Lastname = new HttpCookie("C_Lastname");
                        Cookie_Lastname.Value = check.Student_lastname;
                        Response.Cookies.Add(Cookie_Lastname);

                        var Cookie_Sex = new HttpCookie("C_Sex");
                        Cookie_Sex.Value = check.Sex;
                        Response.Cookies.Add(Cookie_Sex);

                        var Cookie_School = new HttpCookie("C_School");
                        Cookie_School.Value = check.School;
                        Response.Cookies.Add(Cookie_School);

                        var Cookie_Grade = new HttpCookie("C_Grade");
                        Cookie_Grade.Value = check.Grade;
                        Response.Cookies.Add(Cookie_Grade);

                        var Cookie_Email = new HttpCookie("C_Email");
                        Cookie_Email.Value = check.Email;
                        Response.Cookies.Add(Cookie_Email);
                        return RedirectToAction("FindYourself", "Student");
                    }

                    if (check2 != null)
                    {

                        var Cookie_Username = new HttpCookie("C_Username");
                        Cookie_Username.Value = check2.Username;
                        Response.Cookies.Add(Cookie_Username);

                        var Cookie_Password = new HttpCookie("C_Password");
                        Cookie_Password.Value = check2.Password;
                        Response.Cookies.Add(Cookie_Password);

                        var Cookie_Firstname = new HttpCookie("C_Firstname");
                        Cookie_Firstname.Value = check2.Tutor_name;
                        Response.Cookies.Add(Cookie_Firstname);

                        var Cookie_Lastname = new HttpCookie("C_Lastname");
                        Cookie_Lastname.Value = check2.Tutor_lastname;
                        Response.Cookies.Add(Cookie_Lastname);

                        var Cookie_Sex = new HttpCookie("C_Sex");
                        Cookie_Sex.Value = check2.Sex;
                        Response.Cookies.Add(Cookie_Sex);

                        var Cookie_Tel = new HttpCookie("C_Tel");
                        Cookie_Tel.Value = check2.Tel;
                        Response.Cookies.Add(Cookie_Tel);

                        var Cookie_Email = new HttpCookie("C_Email");
                        Cookie_Email.Value = check2.Email;
                        Response.Cookies.Add(Cookie_Email);

                        var Cookie_Education = new HttpCookie("C_Education");
                        Cookie_Education.Value = check2.Education;
                        Response.Cookies.Add(Cookie_Education);

                        var Cookie_Price = new HttpCookie("C_Price");
                        Cookie_Price.Value = check2.Price;
                        Response.Cookies.Add(Cookie_Price);

                        return RedirectToAction("MyPost", "Tutor");
                    }
                    else
                    {
                        ViewBag.Message = "username or password is wrong";
                    }
                }
            }
            return View();
        }
    }
}