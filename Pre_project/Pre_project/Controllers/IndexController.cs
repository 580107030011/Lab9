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
    public class IndexController : Controller
    {
        private PreprojectEntities db = new PreprojectEntities();
        // GET: Index
        public ActionResult Index()
        {
            return View();
        }       
    }
}