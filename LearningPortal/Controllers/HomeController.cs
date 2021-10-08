using LearningPortal.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LearningPortal.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public ApplicationDbContext Db = new ApplicationDbContext();
       
        // GET: Student
        
        public ActionResult StudentDashboard()
        {
            return View();
        }

        public PartialViewResult Menu()
        {
            var ls = Db.Categories.ToList(); 
            System.Web.HttpRuntime.Cache["Menu"] = ls;
            return PartialView();
        }
        // Get submenu
        public void get_Submenu(int catid)
        {
            var subCat = Db.SubCategories.Where(i => i.CategoryId == catid).ToList();
            System.Web.HttpRuntime.Cache["submenu"] = subCat;
        }

        // Get Subtosubmenu
        public void get_Subtosubmenu(int Subcat_id)
        {
            var cour = Db.Courses.Where(i => i.SubCategoryId == Subcat_id).ToList();
            System.Web.HttpRuntime.Cache["subtosubmenu"] = cour;
        }
        public PartialViewResult ViewAllCourse(int? id)
        {
            string SubCat = "", Cat = ""; int courseId = 0;
            var course = Db.Courses.Where(i => i.SubCategoryId == id).ToList();
            
            foreach (var item in course)
            {
                SubCat = item.SubCategories.SubCategoryName;
                Cat = item.SubCategories.Categories.CategoryName;
                courseId = item.CourseId;
            }
            ViewBag.BreadCrumbCat = Cat;
            ViewBag.BreadCrumbSubCat = SubCat;
            ViewBag.id = courseId;
            return PartialView(course);
        }

        public PartialViewResult ResumeCourse(string UserId)
        {
            var course = Db.Courses.SqlQuery("select  DISTINCT cor.*  from Courses cor inner join Sections sCat on cor.CourseId = sCat.CourseId inner join SectionMedias cat on cat.SectionId = sCat.SectionId inner join UserMediaHistories useMedHis on useMedHis.SectionMediaId = cat.SectionMediaId inner join AspNetUsers netUse on netUse.Id = useMedHis.UserId where netUse.Id = '" + UserId + "'").ToList();
            ViewBag.Counts = course.Count();
            return PartialView(course);
        }
      
        public PartialViewResult FeaturedCourse(string UserId)
        {
            //List<LearningPortal.Models.Courses> course = new List<LearningPortal.Models.Courses>();
                var course1 = Db.Courses.SqlQuery("Select * from Courses where Courses.CourseId NOT IN (select Distinct(Courses.CourseId) from  Courses join Sections on Courses.CourseId = Sections.CourseId join SectionMedias on Sections.SectionId = SectionMedias.SectionId join UserMediaHistories on SectionMedias.SectionMediaId = UserMediaHistories.SectionMediaId join AspNetUsers on UserMediaHistories.UserId = AspNetUsers.Id where AspNetUsers.Id = '" + UserId + "')and IsFeatured = 1").ToList();
                return PartialView(course1);
        }

        public ActionResult StudentCourse(int? id)
        {
            var courses = Db.Courses.Find(id)
;

            string Section = "Section " + courses.Sections.Count();
            int videocount = 0;
            foreach (var item in courses.Sections)
            {
                videocount = videocount + item.SectionMedia.Count();
            }
            string Video = " - Videos " + videocount;

            ViewBag.data = Section + Video;
            return View(courses);
        }
        public ActionResult StudentCourseVideo(int? id)
        {
            string userid = User.Identity.GetUserId();
            var count = Db.UserMediaHistories.Where(a => a.SectionMediaId == id && a.UserId == userid).FirstOrDefault();
            var startime = 0;

            if (count == null)
            {
                UserMediaHistory obj = new UserMediaHistory();
                obj.WatchedTime = startime;
                obj.UserId = userid;

                obj.SectionMediaId = Convert.ToInt32(id)
;
                Db.UserMediaHistories.Add(obj);
                Db.SaveChanges();

            }
            else
            {
                startime = count.WatchedTime;
            }
            ViewBag.StartTime = startime;
            var sectionMedia = Db.SectionMedia.Find(id);
;
            return View(sectionMedia);
        }
        [HttpPost]
        public string UpdateUserMedia(int number1, int number2)
        {

            string userid = User.Identity.GetUserId();
            UserMediaHistory count = Db.UserMediaHistories.Where(a => a.SectionMediaId == number1 && a.UserId == userid).FirstOrDefault();
            count.WatchedTime = number2;

            //db.UserMediaHistory.SqlQuery("update UserMediaHistories set UserMediaHistories.WatchedTime=" + number2 + "where UserMediaHistories.UserVideoHistoryId=" + count.UserVideoHistoryId);
            if (ModelState.IsValid)
            {
                Db.Entry(count).State = EntityState.Modified;
                Db.SaveChanges();
            }
            return "Changeiing";
        }

    }
}