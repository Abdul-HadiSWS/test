using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LearningPortal.Models;


namespace LearningPortal.Controllers
{
    public class StudentController : Controller
    {
        public ApplicationDbContext Db = new ApplicationDbContext();
        // GET: Student
        [Authorize]
        public ActionResult StudentDashboard()
        {
            return View();
        }
        public ActionResult StudentCourse(int? id)
        {
            return View();
        }

        public PartialViewResult Menu()
        {
            var ls = Db.Categories.ToList();
            //DataSet ds = dblayer.get_category();
            //ViewBag.category = ds.Tables[0];

            Session["Menu"] = ls;

            return PartialView();
        }


        // Get submenu
        public void get_Submenu(int catid)
        {
            var subCat = Db.SubCategories.Where(i => i.CategoryId == catid).ToList();
            Session["submenu"] = subCat;
        }

        // Get Subtosubmenu
        public void get_Subtosubmenu(int Subcat_id)
        {
            var cour = Db.Courses.Where(i => i.SubCategoryId == Subcat_id).ToList();
            Session["subtosubmenu"] = cour;
        }


    }
}