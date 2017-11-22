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

namespace Pre_project.Controllers
{
    public class FindFacultyController : Controller
    {
        private PreprojectEntities db = new PreprojectEntities();

        public ActionResult Faculty()
        {
            return View();
        }

        public ActionResult Social()
        {
            int id = 1;
            string query = "SELECT * FROM Type_fac WHERE Type_no ="+id;
            var type = db.Database.SqlQuery<Type_fac>(query).SingleOrDefault();

            string query2 = "SELECT * FROM Faculty WHERE Type_no =" + id;
            var fac = db.Database.SqlQuery<Faculty>(query2).ToList();

            return View(Tuple.Create(type,fac));
        }

        public ActionResult Art()
        {
            int id = 2;
            string query = "SELECT * FROM Type_fac WHERE Type_no =" + id;
            var type = db.Database.SqlQuery<Type_fac>(query).SingleOrDefault();

            string query2 = "SELECT * FROM Faculty WHERE Type_no =" + id;
            var fac = db.Database.SqlQuery<Faculty>(query2).ToList();

            return View(Tuple.Create(type, fac));
        }

        public ActionResult Tech()
        {
            int id = 3;
            string query = "SELECT * FROM Type_fac WHERE Type_no =" + id;
            var type = db.Database.SqlQuery<Type_fac>(query).SingleOrDefault();

            string query2 = "SELECT * FROM Faculty WHERE Type_no =" + id;
            var fac = db.Database.SqlQuery<Faculty>(query2).ToList();

            return View(Tuple.Create(type, fac));
        }

        public ActionResult Doc()
        {
            int id = 4;
            string query = "SELECT * FROM Type_fac WHERE Type_no =" + id;
            var type = db.Database.SqlQuery<Type_fac>(query).SingleOrDefault();

            string query2 = "SELECT * FROM Faculty WHERE Type_no =" + id;
            var fac = db.Database.SqlQuery<Faculty>(query2).ToList();

            return View(Tuple.Create(type, fac));
        }

        public ActionResult Nature()
        {
            int id = 5;
            string query = "SELECT * FROM Type_fac WHERE Type_no =" + id;
            var type = db.Database.SqlQuery<Type_fac>(query).SingleOrDefault();

            string query2 = "SELECT * FROM Faculty WHERE Type_no =" + id;
            var fac = db.Database.SqlQuery<Faculty>(query2).ToList();

            return View(Tuple.Create(type, fac));
        }
    }
}