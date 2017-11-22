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
    public class StudentController : Controller
    {
        private PreprojectEntities db = new PreprojectEntities();
        // GET: Student
        public ActionResult FindYourself()
        {
            return View();
        }

        public ActionResult Test()
        {
            string query = "SELECT * FROM Quiz";
            var quiz = db.Database.SqlQuery<Quiz>(query).ToList();

            string query2 = "SELECT * FROM Answer";
            var answer = db.Database.SqlQuery<Answer>(query2).ToList();

            return View(Tuple.Create(quiz, answer));
        }

        public ActionResult CalculateTest()
        {
            var result = Request.Form;
            int type1 = 0;
            int type2 = 0;
            int type3 = 0;
            int type4 = 0;
            int type5 = 0;
            for (int i = 0; i < result.Count; i++)
            {
                var value = result[i];
                if (value == "1")
                {
                    type1++;
                }
                else if (value == "2")
                {
                    type2++;
                }
                else if (value == "3")
                {
                    type3++;
                }
                else if (value == "4")
                {
                    type4++;
                }
                else if (value == "5")
                {
                    type5++;
                }
            }

            int[] arr = new[] { type1, type2, type3, type4, type5 };
            int max = arr.Max();
            int index = Array.LastIndexOf(arr, max);
            return RedirectToAction("Result","Student",new { type_no = index+1 });
        }

        public ActionResult Result(int type_no)
        {
            Result result = new Result();
            result.Student = Request.Cookies["C_Username"].Value;
            result.R_date = DateTime.Now;
            result.Type_no = type_no;

            db.Results.Add(result);
            db.SaveChanges();

            string query = "SELECT * FROM Type_Fac WHERE Type_no = " + type_no;
            var type = db.Database.SqlQuery<Type_fac>(query).SingleOrDefault();

            string query2 = "SELECT * FROM Faculty WHERE Type_no = " + type_no;
            var fac = db.Database.SqlQuery<Faculty>(query2).ToList();
            return View(Tuple.Create(type, fac));
        }

        public ActionResult FindTutor()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult FindTutor(string subject, string price, string sex)
        {
            if (subject == " " && price == " " && sex == " ")
            {
                string query = "SELECT * FROM Post_Tutor";
                return RedirectToAction("SeeTutor", "Student", new { query = query });
            }
            else if (subject == " " && price == " ")
            {
                sex = "'%" + sex + "%'";
                string query = "SELECT * FROM Post_Tutor WHERE Sex LIKE N"+sex;
                return RedirectToAction("SeeTutor", "Student", new { query = query });
            }
            else if(subject == " " && sex == " ")
            {
                price = "'%" + price + "%'";
                string query = "SELECT * FROM Post_Tutor WHERE Price LIKE N" + price;
                return RedirectToAction("SeeTutor", "Student", new { query = query });
            }
            else if (price == " " && sex == " ")
            {
                subject = "'" + subject + "'";
                string query = "SELECT * FROM Post_Tutor WHERE Subject LIKE N" + subject;
                return RedirectToAction("SeeTutor", "Student", new { query = query });
            }
            else if (subject == " ")
            {
                price = "'%" + price + "%'";
                sex = "'%" + sex + "%'";
                string query = "SELECT * FROM Post_Tutor WHERE Price LIKE N" + price+ " AND Sex LIKE N" + sex;
                return RedirectToAction("SeeTutor", "Student", new { query = query });
            }
            else if (price == " ")
            {
                subject = "'" + subject + "'";
                sex = "'%" + sex + "%'";
                string query = "SELECT * FROM Post_Tutor WHERE Subject LIKE N" + subject + " AND Sex LIKE N" + sex;
                return RedirectToAction("SeeTutor", "Student", new { query = query });
            }
            else if (sex == " ")
            {
                subject = "'" + subject + "'";
                price = "'%" + price + "%'";
                string query = "SELECT * FROM Post_Tutor WHERE Subject LIKE N" + subject + " AND Price LIKE N" + price;
                return RedirectToAction("SeeTutor", "Student", new { query = query });
            }
            else
            {
                subject = "'" + subject + "'";
                price = "'%" + price + "%'";
                sex = "'%" + sex + "%'";
                string query = "SELECT * FROM Post_Tutor WHERE Subject LIKE N" + subject + " AND Price LIKE N" + price + " AND Sex LIKE N" + sex;
                return RedirectToAction("SeeTutor", "Student", new { query = query });
            }

            return View();
        }

        public ActionResult SeeTutor(string query)
        {
            var data = db.Database.SqlQuery<Post_Tutor>(query).ToList();
            return View(data);
        }

        public ActionResult StudentReadPost(int id, string tutor)
        {
            string query = "SELECT * FROM Post_Tutor WHERE Post_no = " + id;
            var post = db.Database.SqlQuery<Post_Tutor>(query).SingleOrDefault();

            tutor = "'" + tutor + "'";
            string query2 = "SELECT * FROM Tutor WHERE Username = " + tutor;
            var dataTutor = db.Database.SqlQuery<Tutor>(query2).SingleOrDefault();
            return View(Tuple.Create(post, dataTutor));
        }

        public ActionResult Evaluation()
        {
            return View();
        }

        public ActionResult ProfileStudent(string username)
        {
            string text = "'"+ username +"'";
            string query = "SELECT * FROM Student WHERE Username = " + text;
            var profile = db.Database.SqlQuery<Student>(query).SingleOrDefault();
            return View(profile);
        }

        public ActionResult EditProfile(string username)
        {
            string text = "'" + username + "'";
            string query = "SELECT * FROM Student WHERE Username = " + text;
            var profile = db.Database.SqlQuery<Student>(query).SingleOrDefault();
            return View(profile);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult EditProfile(string new_firstname, string new_lastname, string new_sex, string new_school, string new_grade, string new_email)
        {
            string new_username = Request.Cookies["C_Username"].Value;
            var update = db.Students.Where(a => a.Username.Equals(new_username)).FirstOrDefault<Student>();
            update.Student_name = new_firstname;
            update.Student_lastname = new_lastname;
            update.Sex = new_sex;
            update.School = new_school;
            update.Grade = new_grade;
            update.Email = new_email;

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
            return RedirectToAction("ProfileStudent", "Student", new { username = new_username });
        }

        public ActionResult ChangePasswordS()
        {
            return View();

        }
        // GET: ChangePassword
        [HttpPost]
        public ActionResult ChangePasswordS(string username_old, string password_old, string password_new, string Comfirm_new)
        {
            if (password_old != null)
            {
                var check_username = db.Students.Where(a => a.Username.Equals(username_old)).FirstOrDefault<Student>();
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
            return RedirectToAction("ProfileStudent", "Student", new { username = username_old });
        }
    }
}