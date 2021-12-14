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

        static List<string> data = new List<string>();
        static List<string> Files = new List<string>();
        static string TBimage;
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }
        List<Courses> Course = new List<Courses>();

        [HttpGet]
        public ActionResult AddCourse()
        {
            return View();
        }
        [HttpPost]
        public PartialViewResult CourseList(int? catid, int? subcatid, string tag, bool check)
       {
            if (catid==null&&subcatid==null&&tag=="" && check==false)
            {
                Course = Db.Courses.SqlQuery("Select * from Courses where IsActive='true' order by Time desc").ToList();
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
                Course = Db.Courses.SqlQuery("select * from Courses where CourseName like '%" + tag + "%' and IsActive='true'  order by Time desc").ToList();
            }
            else if (catid != null && subcatid != null && tag != "" && check == true)
            {
                Course = Db.Courses.SqlQuery("select * from Courses cor inner join SubCategories subc on cor.SubCategoryId = subc.SubCategoryId inner join Categories cat on subc.CategoryId = cat.CategoryId where cat.CategoryId = " + catid + " and cor.IsActive='true' and subc.SubCategoryId = " + subcatid + "and cor.IsFeatured = 'True' and cor.CourseName like '%" + tag + "%' order by cor.Time desc").ToList();
            }
            else if (catid != null && subcatid != null && tag != "")
            {
                 Course = Db.Courses.SqlQuery("select * from Courses cor inner join SubCategories subc on cor.SubCategoryId = subc.SubCategoryId inner join Categories cat on subc.CategoryId = cat.CategoryId where cat.CategoryId = " + catid + " and cor.IsActive='true' and subc.SubCategoryId = " + subcatid + " and cor.CourseName like '%" + tag + "%' order by cor.Time desc").ToList();
            }
            ViewBag.modelCount = Course.Count;
            return PartialView(Course);
        }
        public PartialViewResult CourseFilter()
        {
            List<Categories> categories = Db.Categories.SqlQuery("SELECT * FROM Categories where IsActive='true' ORDER BY Time DESC").ToList();
            ViewBag.CateogryList = new SelectList(categories, "CategoryId", "CategoryName");
            // Session["Menu"] = ls;
            return PartialView();
        }
       
        public JsonResult GetSubCategoriesList(int CategoryId)
        {
            Db.Configuration.ProxyCreationEnabled = false;
            List<SubCategories> subCat = Db.SubCategories.SqlQuery("select * from SubCategories where IsActive = 'true' and CategoryId = "+ CategoryId+" order by time desc").ToList();
            ViewBag.SubCatList = new SelectList(subCat, "SubCategoryId", "SubCategoryName");
            return Json(subCat, JsonRequestBehavior.AllowGet);
        }
        public ActionResult CourseEdit()
        {
            ViewBag.PData = data;
            ViewBag.QData = Files;
            ViewBag.ImageUrl = TBimage;
            return View();
        }
        public PartialViewResult DeleCourse(int? Catid)
        {
            var cid = Catid;
            var check = Db.Courses.SqlQuery("SELECT * FROM Courses where CourseId = " + cid).ToList();
            foreach (var item in check)
            {
                ViewBag.CatId = item.CourseId;

            }

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
        [HttpGet]
        public PartialViewResult CourseUploader()
        {

            // ViewBag.PData = data;
            return PartialView();
        }
        [HttpPost]
        public ActionResult CourseUploader(HttpPostedFileBase files)
        {

            string filen = Path.GetFileName(files.FileName);
            var extension = Path.GetExtension(files.FileName);
            var size = files.ContentLength;
            //if (!Regex.Match(filen, "^/.(Zip|zip)$").Success)
            if (extension.ToLower().Equals(".zip"))
            {
                int pos = filen.IndexOf(".zip");
                string filename = filen.Remove(pos);

                string fullpath = Server.MapPath("~/assets/videos/");
                string CompletePath = Path.Combine(filename, fullpath);


                using (ZipArchive archive = new ZipArchive(files.InputStream))
                {



                    archive.ExtractToDirectory(CompletePath);

                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        if (entry.Name != "")
                        {
                            data.Add(entry.FullName);
                            ViewBag.PData = data;
                            if (!Files.Contains(Path.GetDirectoryName(entry.FullName)))
                            {
                                Files.Add(Path.GetDirectoryName(entry.FullName));
                                ViewBag.QData = Files;
                            }

                        }

                        //Files.Add(Path.GetDirectoryName(entry.FullName));
                        //ViewBag.QData = Files;
                    }
                }

                // files.SaveAs(filename);
                ViewBag.success = "File Uloaded";
                return RedirectToAction("CourseEdit");
            }

            else if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".png"))
            {
                if (size < (10 * 1024 * 1024))
                {
                    Image fp = System.Drawing.Image.FromStream(files.InputStream);
                    if (fp.Width % fp.Height == 0)
                    {
                        string path = Path.Combine(Server.MapPath("~/assets/images"), Path.GetFileName(files.FileName));
                        files.SaveAs(path);
                        TBimage = "/assets/images/" + filen;
                        ViewBag.ImageUrl = TBimage;
                        return RedirectToAction("CourseEdit");
                    }

                }

                ViewBag.success = "File is not uploaded";
                return RedirectToAction("CourseEdit");
            }
            ViewBag.success = "File is not uploaded";
            return RedirectToAction("CourseEdit");

        }
        [HttpGet]
        public PartialViewResult TagManager()
        {
            return PartialView();
        }
        [HttpPost]
        public PartialViewResult TagManager(string model)
        {

            return PartialView("CoureEdit");
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
        [HttpGet]
        public ActionResult AddCategory() {
            return View();
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

                        var check = Db.Categories.SqlQuery("SELECT * FROM Categories where CategoryName= '" + cname + "'").ToList();

                        if (check.Count == 0)
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
        public PartialViewResult CategoryList()
        {
            var catt= Db.Categories.SqlQuery("SELECT Cat.CategoryId, Cat.CategoryName,Cat.Time,Cat.IsActive,Cat.Image, count(Subcat.CategoryId) as TotalSubCatagories from Categories Cat LEFT JOIN SubCategories Subcat on Cat.CategoryId = Subcat.CategoryId WHERE Cat.IsActive = 'true'  GROUP BY Cat.CategoryId,Cat.CategoryName,Cat.Time,Cat.IsActive,Cat.Image ORDER BY Cat.Time DESC ").ToList();
            ViewBag.count = catt.Count;
            //var catt= Db.Categories.SqlQuery("SELECT * FROM Categories where IsActive='true' ORDER BY Time DESC").ToList();
            return PartialView(catt);
        }
        public JsonResult DeleteCat(int? Cid)
        {
            bool result = false;
            var cat = Db.Categories.Where(p => p.CategoryId == Cid).SingleOrDefault();
            if (cat != null)
            {
                cat.IsActive = false;
                Db.SaveChanges();
                result = true;
                var subcat = Db.SubCategories.Where(s => s.CategoryId == Cid).ToList();
                if (subcat.Count!=0)
                {
                    foreach (var item in subcat)
                    {
                        item.IsActive = false;
                        var cour = Db.Courses.Where(c => c.CourseId == item.SubCategoryId).ToList();
                        if (cour.Count!=0)
                        {
                            foreach (var cou in cour)
                            {
                                cou.IsActive = false;
                            }
                            Db.SaveChanges();
                        }
                    }
                    Db.SaveChanges();
                }
                
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult DeleteCategory(int? Catid)
        {
            var cid = Catid;
            var check = Db.Categories.SqlQuery("SELECT * FROM Categories where CategoryId = " + cid).ToList();
            foreach (var item in check)
            {
                ViewBag.CatId = item.CategoryId;
                
            }

            return PartialView();

        }   
        [HttpGet]
        public PartialViewResult EditCat(int? Catid)
        {
            var cid = Catid;
            var check = Db.Categories.SqlQuery("SELECT * FROM Categories where CategoryId = " + cid).ToList();
            foreach (var item in check)
            {
                ViewBag.CatId = item.CategoryId;
                ViewBag.check = item.CategoryName;
                ViewBag.Image = item.Image;
            }

            return PartialView();
        }
        [HttpPost]
        public ActionResult EditCat(HttpPostedFileBase catImage, string catName, int? catId)
        {
            //var check = Db.Categories.SqlQuery("SELECT * FROM Categories where CategoryId = " + catId).ToList();
            string cname = catName;

            if (catImage != null)
            {
                string filen = Path.GetFileName(catImage.FileName);
                var extension = Path.GetExtension(catImage.FileName);
                var size = catImage.ContentLength;
                //if (!Regex.Match(filen, "^/.(Zip|zip)$").Success)
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".png"))
                {
                    if (size < (10 * 1024 * 1024))
                    {
                        Image fp = Image.FromStream(catImage.InputStream);
                        if (fp.Width % fp.Height == 0)
                        {
                            string path = Path.Combine(Server.MapPath("~/assets/images/categories"), Path.GetFileName(catImage.FileName));
                            catImage.SaveAs(path);

                            // var check = Db.Categories.SqlQuery("SELECT * FROM Categories where CategoryId = " + catId).ToList();
                            var result = Db.Categories.SingleOrDefault(b => b.CategoryId == catId);
                            if (result != null)
                            {
                                result.CategoryName = cname;
                                result.Image = filen;
                                result.Time = result.Time;
                                result.IsActive = true;

                                Db.SaveChanges();

                            }


                            return RedirectToAction("CategoryList");
                        }

                    }

                    ViewBag.success = "File is not uploaded";
                    return RedirectToAction("CategoryList");
                }
            }
            else
            {
                var result = Db.Categories.SingleOrDefault(b => b.CategoryId == catId);
                if (result != null)
                {
                    result.CategoryName = cname;
                    //result.Image = filen;
                    result.Time = result.Time;
                    result.IsActive = true;

                    Db.SaveChanges();

                }


                return RedirectToAction("CategoryList");
            }
            ViewBag.success = "File is not uploaded";
            return RedirectToAction("CategoryList");
           
        }
        [HttpGet]
        public ActionResult AddSubCategory(string id)
        {
            if (id == null)
            {
                return View("Error404", "Error");
            }
            string tempid = id;
            id = id.Replace('!', '+');
            id = id.Replace('%', 'a');
            var decsc = helpper.Decrypto(id.Replace('$', '/'));

            if (decsc == "")
            {
                return RedirectToAction("ErrorSC", "Error");
            }
            else
            {
                int cid = Convert.ToInt32(decsc);
                var subcat = Db.SubCategories.Where(i => i.CategoryId == cid).ToList();
                var subname = Db.Categories.Where(n => n.CategoryId == cid).ToList();
                foreach (var item in subname)
                {
                    ViewBag.name = item.CategoryName;
                }
                return View();
            }
        }
        
        [HttpPost]
        public ActionResult AddSubCategory(HttpPostedFileBase files, string SubCatname, int? subid)
        {

            string Subcname = SubCatname;

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
                        string path = Path.Combine(Server.MapPath("~/assets/images/Subcategories"), Path.GetFileName(files.FileName));
                        files.SaveAs(path);

                        var check = Db.SubCategories.SqlQuery("SELECT * FROM SubCategories where SubCategoryName= '" + Subcname + "'").ToList();

                        if (check.Count == 0)
                        {
                            SubCategories objCat = new SubCategories();
                            objCat.SubCategoryName = Subcname;
                            objCat.CategoryId = (int)subid;
                            objCat.Image = filen;
                            objCat.Time = DateTime.Now;
                            objCat.IsActive = true;


                            Db.SubCategories.Add(objCat);
                            Db.SaveChanges();
                        }
                        string cid = helpper.Encrypt("" + subid, true);
                        cid = cid.Replace('%', 'a');
                        cid = cid.Replace('+', '!');

                        return RedirectToAction("AddSubCategory",new { id= cid });
                    }

                }

                ViewBag.success = "File is not uploaded";
                return PartialView("AddSubCategory");
            }
            ViewBag.success = "File is not uploaded";
            return PartialView("AddSubCategory");

        }
        public ActionResult SubCategoryList(string id)
        {
            if (id == null)
            {
                return View("Error404", "Error");
            }
            string tempid = id;
            id = id.Replace('!', '+');
            id = id.Replace('%', 'a');
            var decsc = helpper.Decrypto(id.Replace('$', '/'));

            if (decsc == "")
            {
                return RedirectToAction("ErrorSC", "Error");
            }
            else
            {
                int cid = Convert.ToInt32(decsc);
                var catt = Db.SubCategories.SqlQuery("SELECT * FROM SubCategories where   CategoryId ="+ cid+ " and  IsActive='true' ORDER BY Time DESC").ToList();
                ViewBag.CAtId = cid;
                ViewBag.count = catt.Count;
                return PartialView(catt);
            }
        }
        public JsonResult DeleteSubCat(int? SubCatid)
        {
            bool result = false;
            var cat = Db.SubCategories.Where(s => s.SubCategoryId == SubCatid).SingleOrDefault();
            if (cat != null)
            {
                cat.IsActive = false;
                Db.SaveChanges();
                result = true;
                var subcat = Db.Courses.Where(s => s.SubCategoryId == SubCatid).ToList();
                if (subcat.Count != 0)
                {
                    foreach (var item in subcat)
                    {
                        item.IsActive = false;
                    }
                    Db.SaveChanges();
                }

            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult DeleteSubCategory(int? Subcatid)
        {
            var cid = Subcatid;
            var check = Db.SubCategories.SqlQuery("SELECT * FROM SubCategories where SubCategoryId = " + Subcatid).ToList();
            foreach (var item in check)
            {
                ViewBag.SubCatId = item.SubCategoryId;

            }

            return PartialView();

        }
        [HttpGet]
        public PartialViewResult EditSubCat(int? Subcatid)
        {
            var subcid = Subcatid;
            var check = Db.SubCategories.SqlQuery("SELECT * FROM Subcategories where SubCategoryId = " + subcid).ToList();
            foreach (var item in check)
            {
                ViewBag.catId = item.CategoryId;
                ViewBag.subCatId = item.SubCategoryId;
                ViewBag.subName = item.SubCategoryName;
                ViewBag.subImage = item.Image;
            }

            return PartialView();
        }
        [HttpPost]
        public ActionResult EditSubCat(HttpPostedFileBase subcatImage, string subcatName, int? subcatId, int? Cid)
        {
            //var check = Db.Categories.SqlQuery("SELECT * FROM Categories where CategoryId = " + catId).ToList();
            string subcname = subcatName;

            if (subcatImage != null)
            {
                string filen = Path.GetFileName(subcatImage.FileName);
                var extension = Path.GetExtension(subcatImage.FileName);
                var size = subcatImage.ContentLength;
                //if (!Regex.Match(filen, "^/.(Zip|zip)$").Success)
                if (extension.ToLower().Equals(".jpg") || extension.ToLower().Equals(".png"))
                {
                    if (size < (10 * 1024 * 1024))
                    {
                        Image fp = Image.FromStream(subcatImage.InputStream);
                        if (fp.Width % fp.Height == 0)
                        {
                            string path = Path.Combine(Server.MapPath("~/assets/images/categories"), Path.GetFileName(subcatImage.FileName));
                            subcatImage.SaveAs(path);

                            //var result = Db.SubCategories.SqlQuery("SELECT * FROM SubCategories where SubcatagoriesId = " + subcatId+ "").ToList();
                            var result = Db.SubCategories.SingleOrDefault(b => b.SubCategoryId == subcatId);
                            if (result.SubCategoryId == subcatId)
                            {
                                result.SubCategoryName = subcname;
                                result.Image = filen;
                                result.Time = result.Time;
                                result.IsActive = true;

                                Db.SaveChanges();

                            }
                            string cid = helpper.Encrypt("" + Cid, true);
                            cid = cid.Replace('%', 'a');
                            cid = cid.Replace('+', '!');

                            return RedirectToAction("AddSubCategory", new { id = cid });
                        }

                    }

                    ViewBag.success = "File is not uploaded";
                    return RedirectToAction("SubCategoryList");
                }
            }
            else
            {
                var result = Db.SubCategories.SingleOrDefault(b => b.SubCategoryId == subcatId);
                if (result != null)
                {
                    result.SubCategoryName = subcname;
                    //result.Image = filen;
                    result.Time = result.Time;
                    result.IsActive = true;

                    Db.SaveChanges();

                }


                string cid = helpper.Encrypt("" + Cid, true);
                cid = cid.Replace('%', 'a');
                cid = cid.Replace('+', '!');

                return RedirectToAction("AddSubCategory", new { id = cid });
            }
            ViewBag.success = "File is not uploaded";
            return RedirectToAction("SubCategoryList");

        }
       
    }

}