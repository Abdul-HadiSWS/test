using LearningPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.IO.Compression;
using System.Text.RegularExpressions;
using System.Data.Entity.Infrastructure;
using System.Drawing;

namespace LearningPortal.Controllers
{
    public class AdminController : Controller
    {
        public ApplicationDbContext Db = new ApplicationDbContext();
        // GET: Admin
        static string catname = "";
        static string catImage = "";
        public ActionResult Index()
        {
            return View();
        }
        List<Courses> Course = new List<Courses>();
        public ActionResult AddCourse()
        {
            return View();
        }
        //[HttpPost]
        public PartialViewResult CourseList(int? catid, int? subcatid, string tag, bool check)
        {
            
            if (catid==null&&subcatid==null&&tag=="" && check==false)
            {
                Course = Db.Courses.SqlQuery("Select * from Courses where IsActive='true' order by Time desc ").ToList();


            }
            else if (catid == null && subcatid == null && tag == "" && check == true)
            {
                Course = Db.Courses.SqlQuery("Select * from Courses where  IsActive='true' and IsFeatured = 'True' order by Time desc").ToList();

            }

            else if (catid != null && subcatid == null && tag != "" && check == true)
            {
                Course = Db.Courses.SqlQuery("select * from Courses cor inner join SubCategories subc on cor.SubCategoryId = subc.SubCategoryId inner join Categories cat on subc.CategoryId = cat.CategoryId where  cor.IsActive='true' and cat.CategoryId = " + catid + " and cor.CourseName like '%" + tag + "%' and cor.IsFeatured = 'True' order by cor.Time desc").ToList();
            }
            else if (catid != null && subcatid == null && tag != "")
            {
                    Course = Db.Courses.SqlQuery("select * from Courses cor inner join SubCategories subc on cor.SubCategoryId = subc.SubCategoryId inner join Categories cat on subc.CategoryId = cat.CategoryId where cat.CategoryId = " + catid + " and cor.IsActive='true' and cor.CourseName like '%" + tag + "%' order by cor.Time desc").ToList();
            }
            

            else if (catid != null && subcatid != null && tag == "" && check == true)
            {
                Course = Db.Courses.SqlQuery("select * from Courses cor inner join SubCategories subc on cor.SubCategoryId=subc.SubCategoryId inner join Categories cat on subc.CategoryId = cat.CategoryId where cat.CategoryId = " + catid + " and cor.IsActive='true' and subc.SubCategoryId =" + subcatid + " and cor.IsFeatured = 'True' order by cor.Time desc").ToList();
            }
            else if (catid != null && subcatid != null && tag == "")
            {
                Course = Db.Courses.SqlQuery("select * from Courses cor inner join SubCategories subc on cor.SubCategoryId=subc.SubCategoryId inner join Categories cat on subc.CategoryId = cat.CategoryId where cat.CategoryId = " + catid + " and cor.IsActive='true' and subc.SubCategoryId =" + subcatid+ "order by cor.Time desc").ToList();
            }
           

            else if (catid != null && subcatid == null && tag == "" && check == true)
            {
                Course = Db.Courses.SqlQuery("select * from Courses cor inner join SubCategories subc on cor.SubCategoryId = subc.SubCategoryId inner join Categories cat on subc.CategoryId = cat.CategoryId where cat.CategoryId = " + catid + " and cor.IsActive='true' and cor.IsFeatured = 'True' order by cor.Time desc").ToList();
            }
            else if (catid != null && subcatid == null && tag == "")
                {
                    Course = Db.Courses.SqlQuery("select * from Courses cor inner join SubCategories subc on cor.SubCategoryId = subc.SubCategoryId inner join Categories cat on subc.CategoryId = cat.CategoryId where cat.CategoryId = " + catid+ "and cor.IsActive='true' order by cor.Time desc").ToList();
                }
            


            else if (catid == null && subcatid == null && tag != "" && check == true)
            {
                Course = Db.Courses.SqlQuery("select * from Courses where CourseName like '%" + tag + "%' and cor.IsActive='true' and cor.IsFeatured = 'True' order by cor.Time desc").ToList();
            }
            else if (catid == null && subcatid == null && tag != "")
            {
                Course = Db.Courses.SqlQuery("select * from Courses where CourseName like '%" + tag + "%' cor.IsActive='true' and order by cor.Time desc").ToList();
            }
            else if (catid != null && subcatid != null && tag != "" && check == true)
            {
                Course = Db.Courses.SqlQuery("select * from Courses cor inner join SubCategories subc on cor.SubCategoryId = subc.SubCategoryId inner join Categories cat on subc.CategoryId = cat.CategoryId where cat.CategoryId = " + catid + " and cor.IsActive='true' and subc.SubCategoryId = " + subcatid + "and cor.IsFeatured = 'True' and cor.CourseName like '%" + tag + "%' order by cor.Time desc").ToList();
            }
            else if (catid != null && subcatid != null && tag != "")
            {
                 Course = Db.Courses.SqlQuery("select * from Courses cor inner join SubCategories subc on cor.SubCategoryId = subc.SubCategoryId inner join Categories cat on subc.CategoryId = cat.CategoryId where cat.CategoryId = " + catid + " and cor.IsActive='true' and subc.SubCategoryId = " + subcatid + " and cor.CourseName like '%" + tag + "%' order by cor.Time desc").ToList();
            }
            return PartialView(Course);
        }
        public PartialViewResult CourseFilter()
        {
            List<Categories> categories = Db.Categories.SqlQuery("SELECT * FROM Categories where IsActive='true' ORDER BY Time DESC").ToList();
            ViewBag.CateogryList = new SelectList(categories, "CategoryId", "CategoryName");
            // Session["Menu"] = ls;
            return PartialView();
        }
        //public List<Categories> GetCategoriesList()
        //{

        //    List<Categories> categories = Db.Categories.OrderBy(x=>x.Time).ToList();
        //    return categories;

        //}
        public JsonResult GetSubCategoriesList(int CategoryId)
        {
            Db.Configuration.ProxyCreationEnabled = false;
            List<SubCategories> subCat = Db.SubCategories.SqlQuery("select * from SubCategories where IsActive = 'true' and CategoryId = "+ CategoryId+" order by time desc").ToList();
            ViewBag.SubCatList = new SelectList(subCat, "SubCategoryId", "SubCategoryName");
            return Json(subCat, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CourseEdit()
        {
            return View();
        }
        [HttpGet]
        public PartialViewResult CourseUploader()
        {
            List<string> data = new List<string>() { "no data" };
            ViewBag.PData = data;
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult CourseUploader(HttpPostedFileBase files)
        {

            string filen = Path.GetFileName(files.FileName);
            if (!Regex.Match(filen, "^.(Zip|zip)$").Success)
            {
                int pos = filen.IndexOf(".zip");
                string filename = filen.Remove(pos);
                string fullpath = Server.MapPath("~/assets/videos/");
                string CompletePath = Path.Combine(filename, fullpath);
                List<string> data = new List<string>() {"no data" };
                
                using (ZipArchive archive = new ZipArchive(files.InputStream))
                {
                    archive.ExtractToDirectory(CompletePath);

                    foreach(ZipArchiveEntry entry in archive.Entries)
                    {
                        data.Add(entry.FullName);
                        ViewBag.PData = data;
                    }
                }
                // files.SaveAs(filename);
                ViewBag.success = "File Uloaded";
                return PartialView();
            }
            ViewBag.success = "File is not uploaded";
            return PartialView();

        }


        public JsonResult DeleteCourse(int? Cid)
        {
            bool result = false;
            var cour = Db.Courses.Where(p => p.CourseId == Cid).SingleOrDefault();
            if (cour != null)
            {
                cour.IsActive = false;
                Db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
           
            
        }
        public JsonResult MarkFeatureCourse(int? Cid)
        {
            bool result = false;
            var cour = Db.Courses.Where(p => p.CourseId == Cid).SingleOrDefault();
            if (cour != null)
            {
                if (cour.IsFeatured!=true)
                {
                    cour.IsFeatured = true;
                    Db.SaveChanges();
                }
                else
                {
                    cour.IsFeatured = false;
                    Db.SaveChanges();
                }
               
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);


        }

        public ActionResult AddCategory() {
            return View();
        }
        public PartialViewResult CategoryList()
        {
            var catt= Db.Categories.SqlQuery("SELECT * FROM Categories where IsActive='true' ORDER BY Time DESC ").ToList();
            if (catname!="")
            {
                ViewBag.CatName = catname;
                ViewBag.CatIamge = catImage;
                catImage = "";
                catname = "";
                ViewBag.Condition = "check";
            }
           
               
            

            return PartialView(catt);
        }
        public JsonResult TotalSubCategories(int? Cid)
        {
            var totalSubCour = Db.Courses.SqlQuery(""); //.Where(p => p.CourseId == Cid).SingleOrDefault();
            return Json(totalSubCour, JsonRequestBehavior.AllowGet);
        }

       // DeleteCategory
        public JsonResult DeleteCategory(int? Cid)
        {
            bool result = false;
            var cat = Db.Categories.Where(p => p.CategoryId == Cid).SingleOrDefault();
            if (cat != null)
            {
                cat.IsActive = false;
                Db.SaveChanges();
                result = true;
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public PartialViewResult AddCategory(HttpPostedFileBase files, string Catname)
        {
            string cname = Catname;
            string filen = Path.GetFileName(files.FileName);
            var extension = Path.GetExtension(files.FileName);
            var size = files.ContentLength;
            //if (!Regex.Match(filen, "^/.(Zip|zip)$").Success)
            if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".png"))
            {
                if (size < (10 * 1024 * 1024))
                {
                    Image fp = Image.FromStream(files.InputStream);
                    if (fp.Width % fp.Height == 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/assets/images/categories"), Path.GetFileName(files.FileName));
                        files.SaveAs(path);

                        var check = Db.Categories.SqlQuery("SELECT * FROM Categories where CategoryName=" + cname);

                        if (check==null)
                        {
                            Categories objCat = new Categories();
                            objCat.CategoryName = cname;
                            objCat.Image = filen;
                            objCat.Time = DateTime.Now;
                            objCat.IsActive = true;


                            Db.Categories.Add(objCat);
                            Db.SaveChanges();
                        }

                        return PartialView("AddCategory");
                    }

                }

                ViewBag.success = "File is not uploaded";
                return PartialView("AddCategory");
            }
            ViewBag.success = "File is not uploaded";
            return PartialView("AddCategory");

        }
    
        public PartialViewResult EditCategory(int? Catid)
        {
            var cid = Catid;
            var check = Db.Categories.SqlQuery("SELECT * FROM Categories where CategoryId = " + cid).ToList();
            
            return PartialView(check);

        }
        public PartialViewResult EditCat(int? Catid)
        {
            var cid = Catid;
            var check = Db.Categories.SqlQuery("SELECT * FROM Categories where CategoryId = " + cid).ToList();

            return PartialView(check);

        }

    }

}