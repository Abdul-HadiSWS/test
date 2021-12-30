using LearningPortal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.IO.Compression;
using System.Drawing;
using Newtonsoft.Json;
using System.Data.Entity;

namespace LearningPortal.Controllers
{
    public class AdminController : Controller
    {
        public string uploadImage(HttpPostedFileBase files, string folder)
        {
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
                        if (folder == "category")
                        {
                            string path = Path.Combine(Server.MapPath("~/assets/images/categories"), Path.GetFileName(files.FileName));
                            files.SaveAs(path);
                        }
                        else if (folder == "subcategory")
                        {
                            string path = Path.Combine(Server.MapPath("~/assets/images/Subcategories"), Path.GetFileName(files.FileName));
                            files.SaveAs(path);
                        }

                    }
                }
            }
            return filen;
        }
        public ApplicationDbContext Db = new ApplicationDbContext();
        List<Courses> Course = new List<Courses>();
        static List<string> data = new List<string>();
        static List<string> Files = new List<string>();
        List<Courses> CourseCount = new List<Courses>();
        List<SubCategories> SubCategoryCount = new List<SubCategories>();
        static string root;
        static List<string> Tags = new List<string>();
        static List<string> WWYL = new List<string>();
        static string CrsTitle;
        static string CrsDescription;
        static string TBimage;
        static string filen;
        static string FileExists;
        static string Temp;
        // GET: Admin

        public ActionResult Index()
        {
            return View();
        }
        //course start//

        public ActionResult AddCourse(string cid, string scid)
        {
            if (cid == null || scid == null)
            {
                return View();
            }

            cid = cid.Replace('!', '+');
            cid = cid.Replace('%', 'a');
            var dec = helpper.Decrypto(cid.Replace('$', '/'));

            scid = scid.Replace('!', '+');
            scid = scid.Replace('%', 'a');
            var decsc = helpper.Decrypto(scid.Replace('$', '/'));

            if (decsc == "" || dec == "")
            {
                return RedirectToAction("Error404A", "Error");
            }
            else
            {
                int caid = Convert.ToInt32(dec);
                int scaid = Convert.ToInt32(decsc);
                ViewBag.CategoryId = caid;
                ViewBag.SubCategoryId = scaid;
                return View();

            }

        }

        [HttpPost]
        public PartialViewResult CourseList(int? catid, int? subcatid, string tag, bool check)
        {
            if (catid == null && subcatid == null && tag == "" && check == false)
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
                Course = Db.Courses.SqlQuery("select * from Courses cor inner join SubCategories subc on cor.SubCategoryId=subc.SubCategoryId inner join Categories cat on subc.CategoryId = cat.CategoryId where cat.CategoryId = " + catid + " and cor.IsActive='true' and subc.SubCategoryId =" + subcatid + "order by cor.Time desc").ToList();
            }
            else if (catid != null && subcatid == null && tag == "" && check == true)
            {
                Course = Db.Courses.SqlQuery("select * from Courses cor inner join SubCategories subc on cor.SubCategoryId = subc.SubCategoryId inner join Categories cat on subc.CategoryId = cat.CategoryId where cat.CategoryId = " + catid + " and cor.IsActive='true' and cor.IsFeatured = 'True' order by cor.Time desc").ToList();
            }
            else if (catid != null && subcatid == null && tag == "")
            {
                Course = Db.Courses.SqlQuery("select * from Courses cor inner join SubCategories subc on cor.SubCategoryId = subc.SubCategoryId inner join Categories cat on subc.CategoryId = cat.CategoryId where cat.CategoryId = " + catid + "and cor.IsActive='true' order by cor.Time desc").ToList();
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
        public PartialViewResult CourseFilter(int? catid, int? subcatid)
        {
            List<Categories> categories = Db.Categories.SqlQuery("SELECT * FROM Categories where IsActive='true' ORDER BY Time DESC").ToList();
            ViewBag.CateogryList = new SelectList(categories, "CategoryId", "CategoryName");
            if (catid != null && subcatid != null)
            {
                ViewBag.CategoryId = catid;
                ViewBag.SubCategoryId = subcatid;

                // Session["Menu"] = ls;
                return PartialView();

            }
            else
            {
                //List<Categories> categories = Db.Categories.SqlQuery("SELECT * FROM Categories where IsActive='true' ORDER BY Time DESC").ToList();
                //ViewBag.CateogryList = new SelectList(categories, "CategoryId", "CategoryName");
                // Session["Menu"] = ls;
                return PartialView();
            }
        }
        public JsonResult GetSubCategoriesList(int CategoryId)
        {
            Db.Configuration.ProxyCreationEnabled = false;
            List<SubCategories> subCat = Db.SubCategories.SqlQuery("select * from SubCategories where IsActive = 'true' and CategoryId = " + CategoryId + " order by time desc").ToList();
            ViewBag.SubCatList = new SelectList(subCat, "SubCategoryId", "SubCategoryName");
            return Json(subCat, JsonRequestBehavior.AllowGet);
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



        public JsonResult MarkFeatureCourse(int? Cid)
        {
            bool result = false;
            var cour = Db.Courses.Where(p => p.CourseId == Cid).SingleOrDefault();
            if (cour != null)
            {
                if (cour.IsFeatured != true)
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


        //course end//

        //category start//
        [HttpGet]
        public ActionResult AddCategory()
        {
            return View();
        }
        [HttpPost]
        public PartialViewResult AddCategory(HttpPostedFileBase files, string Catname)
        {
            //var check = Db.SubCategories.SqlQuery("SELECT * FROM SubCategories where SubCategoryName= '" + SubCatname + "'").SingleOrDefault();
            var check = Db.Categories.SqlQuery("SELECT * FROM Categories where CategoryName= '" + Catname + "'").SingleOrDefault();

            if (check == null)
            {
                Categories objCat = new Categories();
                objCat.CategoryName = Catname;
                objCat.Image = uploadImage(files, "category");
                objCat.Time = DateTime.Now;
                objCat.IsActive = true;


                Db.Categories.Add(objCat);
                Db.SaveChanges();
            }
            else
            {
                check.IsActive = true;
                check.Image = uploadImage(files, "category");
                Db.SaveChanges();

            }

            return PartialView("AddCategory");
        }
        public PartialViewResult CategoryList()
        {
            //var catt = Db.Categories.SqlQuery("SELECT Cat.CategoryId, Cat.CategoryName,Cat.Time,Cat.IsActive,Cat.Image, count(Subcat.CategoryId) as TotalSubCatagories from Categories Cat LEFT JOIN SubCategories Subcat on Cat.CategoryId = Subcat.CategoryId WHERE Cat.IsActive = 'true'  GROUP BY Cat.CategoryId,Cat.CategoryName,Cat.Time,Cat.IsActive,Cat.Image ORDER BY Cat.Time DESC ").ToList();
            var catt = Db.Categories.SqlQuery("SELECT * from Categories WHERE IsActive = 'true' ORDER BY Time DESC").ToList();

            var count = Db.SubCategories.SqlQuery("Select * from subcategories").ToList();
            foreach (var item in count)
            {
                SubCategoryCount.Add(item);
                ViewBag.Courcount = SubCategoryCount;
            }
            ViewBag.count = catt.Count;
            //var catt= Db.Categories.SqlQuery("SELECT * FROM Categories where IsActive='true' ORDER BY Time DESC").ToList();
            return PartialView(catt);
        }
        public JsonResult DeleteCat(int? Cid, int? DefaultId)
        {
            bool result = false;
            //var defaultCatId = Db.Categories.Where(x => x.CategoryName == "Miscellaneous").SingleOrDefault();
            //int id = defaultCatId.CategoryId;
            if (Cid == DefaultId)
            {
                result = false;
            }
            else
            {
                var cat = Db.Categories.Where(p => p.CategoryId == Cid).SingleOrDefault();
                if (cat != null)
                {
                    cat.IsActive = false;
                    Db.SaveChanges();
                    result = true;
                    var subcat = Db.SubCategories.Where(s => s.CategoryId == Cid).ToList();
                    if (subcat.Count != 0)
                    {
                        foreach (var item in subcat)
                        {
                            item.CategoryId = (int)DefaultId;
                        }
                        Db.SaveChanges();
                    }
                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult DeleteCategory(int? Catid)
        {
            var cid = Catid;
            //var check = Db.Categories.SqlQuery("SELECT * FROM Categories where CategoryId = " + cid).SingleOrDefault();
            var defaultCatId = Db.Categories.Where(x => x.CategoryName == "Miscellaneous").SingleOrDefault();
            int id = defaultCatId.CategoryId;
            ViewBag.CatId = cid;
            ViewBag.defaultId = id;
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
                var result = Db.Categories.SingleOrDefault(b => b.CategoryId == catId);
                if (result != null)
                {
                    result.CategoryName = cname;
                    result.Image = uploadImage(catImage, "category");
                    result.Time = result.Time;
                    result.IsActive = true;
                    Db.SaveChanges();
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

            }

            return RedirectToAction("CategoryList");

        }
        //category end//

        //subcategory start//
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

            var check = Db.SubCategories.SqlQuery("SELECT * FROM SubCategories where SubCategoryName= '" + SubCatname + "'").SingleOrDefault();

            if (check == null)
            {
                SubCategories objCat = new SubCategories();
                objCat.SubCategoryName = SubCatname;
                objCat.CategoryId = (int)subid;
                objCat.Image = uploadImage(files, "subcategory");
                objCat.Time = DateTime.Now;
                objCat.IsActive = true;
                Db.SubCategories.Add(objCat);
                Db.SaveChanges();
            }
            else
            {
                check.IsActive = true;
                check.Image = uploadImage(files, "subcategory");
                Db.SaveChanges();

            }
            string cid = helpper.Encrypt("" + subid, true);
            cid = cid.Replace('%', 'a');
            cid = cid.Replace('+', '!');
            return RedirectToAction("AddSubCategory", new { id = cid });

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
                var catt = Db.SubCategories.SqlQuery("SELECT * FROM SubCategories where   CategoryId =" + cid + " and  IsActive='true' ORDER BY Time DESC").ToList();
                ViewBag.CAtId = cid;
                var countc = Db.Courses.SqlQuery("Select * from courses").ToList();
                foreach (var item in countc)
                {
                    CourseCount.Add(item);
                    ViewBag.CourseCount = CourseCount;
                }
                ViewBag.count = catt.Count;
                return PartialView(catt);
            }
        }
        public JsonResult DeleteSubCat(int? SubCatid, int? DefaultId)
        {
            bool result = false;



            if (SubCatid == DefaultId)
            {
                result = false;
            }
            else
            {
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
                            item.SubCategoryId = (int)DefaultId;
                        }
                        Db.SaveChanges();
                    }

                }
            }
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public PartialViewResult DeleteSubCategory(int? Subcatid)
        {
            var cid = Subcatid;
            //var check = Db.SubCategories.SqlQuery("SELECT * FROM SubCategories where SubCategoryId = " + Subcatid).SingleOrDefault();
            var defaultCatId = Db.SubCategories.Where(x => x.SubCategoryName == "Others").SingleOrDefault();
            int id = defaultCatId.SubCategoryId;
            ViewBag.SubCatId = cid;
            ViewBag.defaultId = id;
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
            string subcname = subcatName;
            if (subcatImage != null)
            {
                //var result = Db.SubCategories.SqlQuery("SELECT * FROM SubCategories where SubcatagoriesId = " + subcatId+ "").ToList();
                var result = Db.SubCategories.SingleOrDefault(b => b.SubCategoryId == subcatId);
                if (result.SubCategoryId == subcatId)
                {
                    result.SubCategoryName = subcname;
                    result.Image = uploadImage(subcatImage, "subcategory"); ;
                    result.Time = result.Time;
                    result.IsActive = true;
                    Db.SaveChanges();
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
            }
            string cid = helpper.Encrypt("" + Cid, true);
            cid = cid.Replace('%', 'a');
            cid = cid.Replace('+', '!');
            return RedirectToAction("AddSubCategory", new { id = cid });
        }



        //subcategory end//




        public ActionResult CourseEdit()
        {

            ViewBag.Span = Tags;
            ViewBag.PData = data;
            ViewBag.QData = Files;
            ViewBag.ImageUrl = TBimage;
            ViewBag.WWYL = WWYL;
            ViewBag.CourseTitle = CrsTitle;
            ViewBag.CourseDescription = CrsDescription;
            ViewBag.Placeholder = root;
            return View();
        }




        [HttpGet]
        public ActionResult CourseOutline()
        {
            List<string> Files = new List<string>();
            List<List<string>> data = new List<List<string>>();

            string rootfoldername = root;
            ViewBag.Placeholder = rootfoldername;

            if (root == null)
            {
                data.Clear();
                Files.Clear();
            }
            else
            {
                string folder = Server.MapPath(string.Format("~/assets/videos/{0}/", rootfoldername));

            

                if (Directory.Exists(folder))
                {
                    /* Section Name*/
                    string[] Filespath = Directory.GetDirectories(folder);



                    foreach (string filePath in Filespath)
                    {
                        Files.Add(Path.GetFileName(filePath));
                    }


                    /* Section File*/
                    for (int i = 0; i < Files.Count; i++)
                    { // Loop through List with for

                        List<string> data1 = new List<string>();



                        Filespath = Directory.GetFiles(Server.MapPath(string.Format("~/assets/videos/{0}/{1}/", rootfoldername, Files[i])));



                        foreach (string filePath in Filespath)
                        {
                            string filen = Path.GetFileName(filePath);
                            int lastindex = filen.IndexOf('.');

                            data1.Add(filen.ToString());

                        }
                        data.Add(data1);

                    }

                    ViewBag.PData = data;
                    ViewBag.QData = Files;

                }
                else
                {

                }
            }
         
            ViewBag.PData = data;
            ViewBag.QData = Files;
            return PartialView();
        }


        public void deletedirectory(string root, string head)
        {

            string rootfolder = Server.MapPath(string.Format("~/assets/{0}/{1}", head, root));


            if (Directory.Exists(rootfolder))
            {
                List<string> Files = new List<string>();

                string[] Filespath = Directory.GetDirectories(rootfolder);
                foreach (string filePath in Filespath)
                {
                    Files.Add(Path.GetFileName(filePath));

                }

                for (int i = 0; i < Files.Count; i++)
                { // Loop through List with for





                    string sectiond= Server.MapPath(string.Format("~/assets/{0}/{1}/{2}/", head, root, Files[i]));


                    string[] Filenames = Directory.GetFiles(sectiond);
                    foreach (var item in Filenames)
                    {
                        System.IO.File.Delete(item);
                    }

                    Directory.Delete(sectiond);



                }

            }



            Directory.Delete(rootfolder);

        }


        public void copydirectory(string source,string destination,string root)
        {

            string rootfolder = Server.MapPath(string.Format("~/assets/{0}/{1}",source, root));





            if (Directory.Exists(rootfolder))
            {
                List<string> Files = new List<string>();
                string temphead = Server.MapPath(string.Format("~/assets/{0}/",destination));


                if (!Directory.Exists(temphead))
                {

                    Directory.CreateDirectory(temphead);

                }

                string temproot = Server.MapPath(string.Format("~/assets/{0}/{1}/", destination,root));


                if (!Directory.Exists(temproot))
                {

                    Directory.CreateDirectory(temproot);

                }
                else
                {

                       deletedirectory(root, destination);
                       
                      Directory.CreateDirectory(temproot);
                    //Directory.Delete(temproot);
                }



                string[] Filespath = Directory.GetDirectories(rootfolder);
                foreach (string filePath in Filespath)
                {
                    string tempsection = Server.MapPath(string.Format("~/assets/{0}/{1}/{2}",destination,root, Path.GetFileName(filePath)));
                   /* if (Directory.Exists(tempsection))
                    {

                        string[] Filenames = Directory.GetFiles(tempsection);
                        foreach (var item in Filenames)
                        {
                            System.IO.File.Delete(item);
                        }

                    }
                    else
                    {
                       

                    }*/

                    Directory.CreateDirectory(tempsection);
                    Files.Add(Path.GetFileName(filePath));

                }

                for (int i = 0; i < Files.Count; i++)
                { // Loop through List with for








                    Filespath = Directory.GetFiles(Server.MapPath(string.Format("~/assets/{0}/{1}/{2}/",source, root, Files[i])));


                    foreach (string filePath in Filespath)
                    {

                        string filen = Path.GetFileName(filePath);


                        var destpath = Server.MapPath(string.Format("~/assets/{0}/{1}/{2}/{3}",destination, root, Files[i], filen));

                        System.IO.File.Copy(filePath, destpath);





                    }



                }
            }

        }

        public ActionResult CourseUpdate(string id)
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
                var cour = Db.Courses.Find(cid);

                var Learning = Db.CourseLearnings.Where(x => x.CourseId == cid).ToList();
                var tg = Db.CourseTag.Where(x => x.CourseId == cid).ToList();

                string WWL = "";
                foreach (var item in Learning)
                {
                    WWL += item.Description + "," + "\n";
                }



                Tags.Clear();
                foreach (var item in tg)
                {
                    Tags.Add(item.TagManager.TagName);
                }

                ViewBag.Span = Tags;
                ViewBag.WWYLU = WWL;
                root = cour.CourseName;
                ViewBag.Placeholder = root;

                copydirectory("videos", "temp",root);
              
                return View(cour);
            }
        }
        

        [HttpGet]
        public ActionResult CourseOutlineEdit()
        {
            string rootfoldername = root;
            string folder = Server.MapPath(string.Format("~/assets/temp/{0}/", rootfoldername));

            List<string> Files = new List<string>();
            List<List<string>> data = new List<List<string>>();


            if (Directory.Exists(folder))
            {
                /* Section Name*/
                string[] Filespath = Directory.GetDirectories(folder);



                foreach (string filePath in Filespath)
                {
                    Files.Add(Path.GetFileName(filePath));
                }


                /* Section File*/
                for (int i = 0; i < Files.Count; i++)
                { // Loop through List with for

                    List<string> data1 = new List<string>();



                    Filespath = Directory.GetFiles(Server.MapPath(string.Format("~/assets/temp/{0}/{1}/", rootfoldername, Files[i])));



                    foreach (string filePath in Filespath)
                    {
                        string filen = Path.GetFileName(filePath);
                        int lastindex = filen.IndexOf('.');

                        data1.Add(filen.ToString());

                    }
                    data.Add(data1);

                }

                ViewBag.PData = data;
                ViewBag.QData = Files;

            }
            else
            {

            }
            ViewBag.PData = data;
            ViewBag.QData = Files;
            return PartialView();
        }




        [HttpPost]
        public ActionResult CourseUpdate(string courseid, string CourseName, string CourseDesc, int courseYear, int? subcategoryId, string courseLevel, string coursepic, string whatulearn,string sectionnamedata, string sectionfiledata, string sectionfileduration)
        {

            string[] sectionName = JsonConvert.DeserializeObject<string[]>(sectionnamedata);
            string[][] sectionMediaName = JsonConvert.DeserializeObject<string[][]>(sectionfiledata);
            string[][] sectionMediaTime = JsonConvert.DeserializeObject<string[][]>(sectionfileduration);



            Courses obj = Db.Courses.Find(Convert.ToInt32(courseid));
            
          
            
            
            coursepic = coursepic.Substring(15);
          
            if (obj != null)
            {

                obj.CourseName = CourseName;
                obj.Description = CourseDesc;
                obj.Year = courseYear;
                obj.Levels = courseLevel;
                obj.Image = coursepic;
                obj.IsFeatured = true;
                obj.IsActive = true;
                obj.SubCategoryId = (int)subcategoryId;
                obj.UploadedDate = DateTime.Now;
                obj.Time = DateTime.Now;
                Db.Entry(obj).State = EntityState.Modified;
                Db.SaveChanges();

                //var result1 = Db.Courses.Where(x => x.CourseName == CourseName).SingleOrDefault();
              
                var totaltag = Db.CourseTag.Where(x => x.CourseId == obj.CourseId ).ToList();
                foreach (var item in totaltag)
                {
                    CourseTag courseTag1 = Db.CourseTag.Find(item.CourseTagId);

                    Db.CourseTag.Remove(courseTag1);
                    Db.SaveChanges();
                }
           

                foreach (var item in Tags)
                {

                        var check = Db.TagManager.Where(x => x.TagName == item).SingleOrDefault();

                   
                        CourseTag courseTag = new CourseTag();
                        courseTag.TagId = check.TagId;
                        courseTag.CourseId = Convert.ToInt32(courseid);
                        Db.CourseTag.Add(courseTag);
                        Db.SaveChanges();
                   

                   
                    //Tags.Clear();
                }

                var totalwwyl = Db.CourseLearnings.Where(x => x.CourseId == obj.CourseId).ToList();
                foreach (var item1 in totalwwyl)
                {
                    CourseLearning courseLearning1 = Db.CourseLearnings.Find(item1.LearnId);

                    Db.CourseLearnings.Remove(courseLearning1);
                    Db.SaveChanges();
                }

                foreach (var wwul in WWYL)
                {

                    if (wwul == "")
                    {

                    }
                    else
                    {
                        CourseLearning courLearn = new CourseLearning();
                        courLearn.CourseId = Convert.ToInt32(courseid);
                        courLearn.Description = wwul;
                        Db.CourseLearnings.Add(courLearn);
                        Db.SaveChanges();
                    }
                   
                    //WWYL.Clear();
                }

                var totalsection = Db.Sections.Where(x => x.CourseId == obj.CourseId).ToList();
                foreach (var item1 in totalsection)
                {
                    Section sections1 = Db.Sections.Find(item1.SectionId);

                    Db.Sections.Remove(sections1);
                    Db.SaveChanges();
                }


                int countoutside = 0;
                foreach (var item2 in sectionName)
                {
                    var name = item2;
                    Section sec = new Section();
                    sec.SectionName = item2;
                    sec.CourseId = Convert.ToInt32(courseid);
                    Db.Sections.Add(sec);
                    Db.SaveChanges();

                    var section = Db.Sections.Where(x => x.SectionName == item2 && x.CourseId == obj.CourseId).SingleOrDefault();
                    int countinside = 0;
                    foreach (var item3 in sectionMediaName[countoutside])
                    {


                        string sectionmedianame = item3;
                        string duration = sectionMediaTime[countoutside][countinside];
                        //string output = Convert.ToDateTime(duration).ToString("yyyy-MM-dd");
                        double seconds = TimeSpan.Parse(duration).TotalSeconds;
                        SectionMedia sectionMedia = new SectionMedia();
                        sectionMedia.VideoTitle = sectionmedianame;
                        sectionMedia.Videotype = "video/mp4";
                        sectionMedia.VideoUrl = sectionmedianame;
                        sectionMedia.SectionId = section.SectionId;
                        sectionMedia.VideoDuration = Convert.ToInt32(seconds);
                        Db.SectionMedia.Add(sectionMedia);
                        Db.SaveChanges();
                        countinside++;
                    }
                    countoutside++;
                }

                
                copydirectory("temp", "videos", obj.CourseName);
                root = "";
                Tags.Clear();
                WWYL.Clear();
                data.Clear();
                Files.Clear();

                ViewBag.PData = null;
                ViewBag.QData = null;
                return RedirectToAction("AddCourse");
            }

           
            return RedirectToAction("AddCourse");

            

        }



        /* Upload  Zip*/
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
            root = Path.GetFileNameWithoutExtension(filen);
            //if (!Regex.Match(filen, "^/.(Zip|zip)$").Success)
            if (extension.ToLower().Equals(".zip"))
            {
                int pos = filen.IndexOf(".zip");
                string filename = filen.Remove(pos);

                string fullpath = Server.MapPath("~/assets/videos/");
                string CompletePath = Path.Combine(filename, fullpath);

                try
                {
                    using (ZipArchive archive = new ZipArchive(files.InputStream))
                    {
                        archive.ExtractToDirectory(CompletePath);
                      
                    }
                    return Json("File Uploaded");
                }
                catch (Exception)
                {

                    root = null;
                    return Json("File NAme is already Exist Please Change the name");
                }
              
                // files.SaveAs(filename);
                //ViewBag.success = "File Uloaded";


                
            }

            return RedirectToAction("CourseEdit");

        }
        /* End Upload  Zip*/



        /* Upload Image*/

        [HttpPost]
        public ActionResult CourseFileUploader(HttpPostedFileBase files)
        {
            string filen = Path.GetFileName(files.FileName);
            var extension = Path.GetExtension(files.FileName);
            var size = files.ContentLength;
            //root = Path.GetFileNameWithoutExtension(filen);
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
                    //archive.Dispose();
                    //foreach (ZipArchiveEntry entry in archive.Entries)
                    //{
                    //    if (entry.Name != "")
                    //    {
                    //        //data.Add(entry.FullName);
                    //        //ViewBag.PData = data;
                    //        if (!Files.Contains(Path.GetDirectoryName(entry.FullName)))
                    //        {
                    //            //Files.Add(Path.GetDirectoryName(entry.FullName));
                    //            //ViewBag.QData = Files;
                    //        }
                    //    }
                    //    //Files.Add(Path.GetDirectoryName(entry.FullName));
                    //    //ViewBag.QData = Files;
                    //}
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
                        //TBimage = "/assets/images/" + filen;
                        ViewBag.ImageUrl = "/assets/images/" + Path.GetFileName(files.FileName);
                        ViewBag.SplitImage = Path.GetFileName(files.FileName);
                        return Json(ViewBag.ImageUrl);
                    }

                }
                else
                {
                    return Json("Invalid Image Size");

                }

                ViewBag.success = "File is not uploaded";
                return Json("Invalid Image Size");

            }
            ViewBag.success = "File is not uploaded";
            return RedirectToAction("CourseEdit");

        }

        /* End Upload Image*/




        /* Upload Videos in  folder*/
        [HttpPost]
       
        public ActionResult Uploads()
        {
            string rootfolder = root;
            string sectionfolder = Request.Form[0];
            string start = Request.Form[1].ToString();
            if (start == "update")
            {
                 start = "temp";

            }
            else
            {
                start = "videos";
            }
          

            if (Request.Files.Count > 0)
            {
                try
                {

                    //  Get all files from Request object  


                    string rootfoldername = Server.MapPath(string.Format("~/assets/{0}/{1}/",start,rootfolder));
                    if (Directory.Exists(rootfoldername))
                    {
                        string Sectionfoldername = Server.MapPath(string.Format("~/assets/{0}/{1}/{2}/",start, rootfolder, sectionfolder));
                        if (Directory.Exists(Sectionfoldername))
                        {

                            HttpFileCollectionBase files = Request.Files;
                            for (int i = 0; i < files.Count; i++)
                            {
                                //string path = AppDomain.CurrentDomain.BaseDirectory + "Uploads/";  

                                HttpPostedFileBase file = files[i];

                                var extension = Path.GetExtension(file.FileName);
                                string filename = Path.GetFileName(file.FileName);
                                //string Filefoldername = Server.MapPath(string.Format("~/assets/videos/{0}/{1}/{2}/", rootfolder, sectionfolder, filename));
                                string _path = Path.Combine(Server.MapPath(string.Format("~/assets/{0}/{1}/{2}",start, rootfolder, sectionfolder)), filename);




                                file.SaveAs(_path);




                                //string Filefoldername = Server.MapPath(string.Format("~/assets/videos/{0}/{1}/{2}/", rootfolder, sectionfolder, filename));


                            }



                            //Directory.CreateDirectory(Filename);
                            return Json("File Upload Successfully!");
                        }
                        else
                        {

                            return Json("Please Select Correct Zip File!");
                        }

                    }

                    return Json("File Upload Successfully");

                }
                catch (Exception ex)
                {
                    return Json("File Upload Failed! " + ex.Message);
                }
            }
            else
            {
                return Json("File is Empty");
            }





        }

        /* End Upload Videos in  folder*/


        /* Create folder*/
        public ActionResult CreateSection(string SectionName,string check)
        {

            if (check != "update")
            {
                check = "videos";
            }
            else
            {
                check = "temp";
            }


            string folder = Server.MapPath(string.Format("~/assets/{0}/{1}/", check, root));
            if (Directory.Exists(folder))
            {
                string folder1 = Server.MapPath(string.Format("~/assets/{0}/{1}/{2}/",check, root, SectionName));
                if (!Directory.Exists(folder1))
                {
                    FileExists = "File is created successfully";
                    Directory.CreateDirectory(folder1);
                    return Json("Successfully section Created");
                }
                else
                {
                    FileExists = "File Is Already Exist";
                    return Json("Already Exist Section Please Change the Name");
                }

            }
            FileExists = "File Is Already Exist";
            return Json("Already Exist Section Please Change the Name");
        }

        /* End Create folder*/



        /* Tag Add And Remove*/


        [HttpGet]
        public PartialViewResult TagManager(string iTag)
        {
            //var check = Db.TagManager.SqlQuery("SELECT * FROM TagManagers where TagName=" + model).ToList();


            var check = Db.TagManager.Where(x => x.TagName == iTag).SingleOrDefault();


            if (iTag == "removessws101")
            {

            }
            else
            {
                if (check == null)
                {
                    TagManager objCat = new TagManager();
                    objCat.TagName = iTag;
                    Db.TagManager.Add(objCat);
                    Db.SaveChanges();
                    Tags.Add(iTag);
                    ViewBag.Span = Tags;
                }
                else
                {
                    Tags.Remove(iTag);
                    Tags.Add(iTag);
                    ViewBag.Span = Tags;
                    return PartialView();
                }
                //Tags.Add(iTag);

            }
            ViewBag.Span = Tags;
            return PartialView();
        }
   
        [HttpGet]
        public ActionResult TagRemover(string value)
        {
            foreach (var item in Tags)
            {
                if (value == item)
                {
                    Tags.Remove(item);
                    ViewBag.Span = Tags;
                    return RedirectToAction("CourseEdit");
                }

            }
            ViewBag.Span = Tags;
            return RedirectToAction("CourseEdit");
        }



        /* End Tag Add And Remove*/





        public PartialViewResult DDCatSub()
        {
            List<Categories> categories = Db.Categories.SqlQuery("SELECT * FROM Categories where IsActive='true' ORDER BY Time DESC").ToList();
            ViewBag.CateogryList = new SelectList(categories, "CategoryId", "CategoryName");
            // Session["Menu"] = ls;
            return PartialView();

        }
        [HttpGet]
        public PartialViewResult WWYLearn()
        {

            return PartialView();
        }
        [HttpPost]
        public ActionResult WWYLearn(string paragraph, string OrderList, string UnorderedList)
        {


            if (OrderList != "")
            {
                string[] array = OrderList.Split(',');

                for (int i = 0; i < array.Length; i++)
                {
                    WWYL.Add(array[i].ToString());
                }
            }
            else if (paragraph != "")
            {
                string[] array = paragraph.Split(',');

                for (int i = 0; i < array.Length; i++)
                {
                    WWYL.Add(array[i].ToString());
                }
            }
            else if (UnorderedList != "")
            {
                string[] array = UnorderedList.Split(',');

                for (int i = 0; i < array.Length; i++)
                {
                    WWYL.Add(array[i].ToString());
                }
            }


            return RedirectToAction("CourseEdit");
        }
        [HttpGet]
        public PartialViewResult AddDesc()
        {
            ViewBag.Placeholder = root;
            return PartialView();
        }

        [HttpPost]
        public ActionResult AddDesc(string CourseTitle, string CourseDescription,string check)
        {
            CrsTitle = CourseTitle;
            CrsDescription = CourseDescription;

            if (check != "update")
            {
                check = "videos";
            }
            else
            {
                check = "temp";
            }

            string folder = Server.MapPath(string.Format("~/assets/{0}/{1}/", check, root));
            string folder1 = Server.MapPath(string.Format("~/assets/{0}/{1}/", check ,CourseTitle));

               var checkname = Db.Courses.Where(x => x.CourseName == CourseTitle).FirstOrDefault();

            if (root == CourseTitle)
            {

                return Json("Folder Name Change Successfully");
            }
            else
            {
                if (checkname == null)
                {
                    if (!Directory.Exists(folder1))
                    {

                        //Directory.CreateDirectory(folder1);
                        Directory.Move(folder, folder1);

                        root = CourseTitle;
                        return Json("Folder Name Change Successfully");
                    }
                }
                else
                {
                    return Json("Course Name Already Exists, Change the Title.");
                }
            }
                
            
               
           
           
          
            return Json("");
        }


        [HttpPost]





        public ActionResult DeleteDir(string DirName, string Filename,string check)
        {

            if (check != "update")
            {
                check = "videos";
            }
            else
            {
                check = "temp";
            }

            if (root == DirName)
            {
                string RootDir = Server.MapPath(string.Format("~/assets/{0}/{1}/",check, root));
                Directory.Delete(RootDir);
            }
            else
            {
                if (Filename != "")
                {
                    string SUBfile = Server.MapPath(string.Format("~/assets/{0}/{1}/{2}/{3}", check, root, DirName, Filename));
                    if (Directory.Exists(SUBfile))
                    {

                    }


                    System.IO.File.Delete(SUBfile);
                }
                else
                {

                    string SUBfolder = Server.MapPath(string.Format("~/assets/{0}/{1}/{2}", check,root, DirName));
                    if (Directory.Exists(SUBfolder))
                    {
                        string[] Filenames = Directory.GetFiles(SUBfolder);
                        foreach (var item in Filenames)
                        {
                            System.IO.File.Delete(item);
                        }
                    }
                    else
                    {


                    }
                }
            }

            return RedirectToAction("");
        }
        [HttpPost]
        public ActionResult EditSubName(string newname, string oldname, string editfile,string check)
        {

            if (check == "update")
            {
               
                check = "temp";
            }
            else
            {
                check = "videos";

            }

            if (editfile == "")
            {
                string folder = Server.MapPath(string.Format("~/assets/{0}/{1}/{2}", check, root, oldname));
                string folder1 = Server.MapPath(string.Format("~/assets/{0}/{1}/{2}/", check, root, newname));
                //     Directory.Move(folder,folder1);

                // Ensure the source directory exists
                if (Directory.Exists(folder) == true)
                {
                    // Ensure the destination directory doesn't already exist
                    if (Directory.Exists(folder1) == false)
                    {
                        // Perform the move
                        Directory.Move(folder, folder1);
                    }
                }
                return Json("Successfully Edit SectionName");
            }
            else
            {
                string filen = oldname;
                int lastindex = filen.IndexOf('.');

                int length = filen.Length;
                string extension = filen.Substring(lastindex);
                newname = newname + extension;


                string folder = Server.MapPath(string.Format("~/assets/{0}/{1}/{2}/",check, root, editfile));

                // Ensure the source directory exists
                if (Directory.Exists(folder))
                {
                    folder = Server.MapPath(string.Format("~/assets/{0}/{1}/{2}/{3}", check,root, editfile, oldname));

                    string folder1 = Server.MapPath(string.Format("~/assets/{0}/{1}/{2}/{3}",check ,root, editfile, newname));

                    // Ensure the destination directory doesn't already exist

                    // Perform the move

                    System.IO.File.Move(folder, folder1);
                    return Json("Successfully Edit SectionFile");

                }
            }


            //Temp = SubDirTitleh;

            //ViewBag.temp = Temp;

            return RedirectToAction("CourseEdit");
        }
        
     
        
        [HttpPost]
        public ActionResult VideoModal1(string subFoldername, string video, string check)
        {

            if (check == "update")
            {

                check = "temp";
            }
            else
            {
                check = "videos";

            }
            string mainFolder = root;
            string SubFolder = subFoldername;
            string Video = video;
            string ext = Path.GetExtension(Video);
            ext = "video/" + ext.Replace(".", "");
            string src = string.Format("/assets/{0}/{1}/{2}/{3}",check,mainFolder, SubFolder, Video);


            ViewBag.Src = src;
            ViewBag.type = ext;
            return Json(""+src);
        }


        public ActionResult gettime(string video, string section, int count1, string extension,string check)
        {

            if (check == "update")
            {
                check = "temp";
                
            }
            else
            {
                check = "videos";
            }


            var videosrc = "/assets/"+ check+"/" + root + "/" + section + "/" + video;

            ViewBag.urlvi = videosrc;
            ViewBag.urlvi1 = "A" + count1;



            ViewBag.ext = "video/" + extension.Substring(1);


            return View();
        }







        public ActionResult InsertCourse(string CourseName, string CourseDesc, int courseYear, int? subcategoryId, string courseLevel, string coursepic, string sectionnamedata, string sectionfiledata, string sectionfileduration)
        {
            string[] sectionName = JsonConvert.DeserializeObject<string[]>(sectionnamedata);
            string[][] sectionMediaName = JsonConvert.DeserializeObject<string[][]>(sectionfiledata);
            string[][] sectionMediaTime = JsonConvert.DeserializeObject<string[][]>(sectionfileduration);

            var result = Db.Courses.Where(x => x.CourseName == CourseName).SingleOrDefault();
            coursepic = coursepic.Substring(15);
            Courses obj = new Courses();
            if (result == null)
            {

                obj.CourseName = CourseName;
                obj.Description = CourseDesc;
                obj.Year = courseYear;
                obj.Levels = courseLevel;
                obj.Image = coursepic;
                obj.IsFeatured = true;
                obj.IsActive = true;
                obj.SubCategoryId = (int)subcategoryId;
                obj.UploadedDate = DateTime.Now;
                obj.Time = DateTime.Now;
                Db.Courses.Add(obj);
                Db.SaveChanges();

                var result1 = Db.Courses.Where(x => x.CourseName == CourseName).SingleOrDefault();

                foreach (var item in Tags)
                {

                    var check = Db.TagManager.Where(x => x.TagName == item).SingleOrDefault();
                        
                    CourseTag courseTag = new CourseTag();
                    courseTag.TagId = check.TagId;
                    courseTag.CourseId = result1.CourseId;

                    Db.CourseTag.Add(courseTag);
                    Db.SaveChanges();
                    //Tags.Clear();
                }
                foreach (var wwul in WWYL)
                {
                    if (wwul == "")
                    {

                    }
                    else
                    {
                        var Wwul = Db.CourseLearnings.Where(x => x.Description == wwul).SingleOrDefault();
                        CourseLearning courLearn = new CourseLearning();
                        courLearn.CourseId = result1.CourseId;
                        courLearn.Description = wwul;
                        Db.CourseLearnings.Add(courLearn);
                        Db.SaveChanges();
                    }

                  
                    //WWYL.Clear();
                }
                int countoutside = 0;
                foreach (var item in sectionName)
                {
                    var name = item;
                    Section sec = new Section();
                    sec.SectionName = item;
                    sec.CourseId = result1.CourseId;
                    Db.Sections.Add(sec);
                    Db.SaveChanges();

                    var section = Db.Sections.Where(x => x.SectionName == item && x.CourseId == result1.CourseId).SingleOrDefault();
                    int countinside=0;
                    foreach (var item1 in sectionMediaName[countoutside])
                    {
                      

                        string sectionmedianame = item1;
                        string duration = sectionMediaTime[countoutside][countinside];
                        //string output = Convert.ToDateTime(duration).ToString("yyyy-MM-dd");
                        double seconds = TimeSpan.Parse(duration).TotalSeconds;
                        SectionMedia sectionMedia = new SectionMedia();
                        sectionMedia.VideoTitle = sectionmedianame;
                        sectionMedia.Videotype = "video/mp4";
                        sectionMedia.VideoUrl = sectionmedianame;
                        sectionMedia.SectionId = section.SectionId;
                        sectionMedia.VideoDuration = Convert.ToInt32(seconds);
                        Db.SectionMedia.Add(sectionMedia);
                        Db.SaveChanges();
                        countinside++;
                    }
                    countoutside++;
                }
                root = "";
                Tags.Clear();
                WWYL.Clear();
                data.Clear();
                Files.Clear();

                ViewBag.PData = null;
                ViewBag.QData = null;
                return RedirectToAction("AddCourse");
            }
            return RedirectToAction("AddCourse");
        }











        public ActionResult check()
        {
            /*
             *  create folder
             * 
            string foldername = "ad";
            string folder = Server.MapPath(string.Format("~/assets/videos/{0}/",foldername));
            if (Directory.Exists(folder))
            {
                string folder1 = Server.MapPath(string.Format("~/assets/videos/{0}/{1}", foldername,"ada23"));
                Directory.CreateDirectory(folder1);
                ViewBag.message = "created";
            }
            */

            /* Create list from folder
              
             */
            string rootfoldername = "ad";
            string folder = Server.MapPath(string.Format("~/assets/videos/{0}/", rootfoldername));

            List<string> Files = new List<string>();
            List<List<string>> data = new List<List<string>>();

            if (Directory.Exists(folder))
            {
                /* Section Name*/
                string[] Filespath = Directory.GetDirectories(folder);



                foreach (string filePath in Filespath)
                {
                    Files.Add(Path.GetFileName(filePath));
                }


                /* Section File*/
                for (int i = 0; i < Files.Count; i++)
                { // Loop through List with for

                    List<string> data1 = new List<string>();

                    Filespath = Directory.GetFiles(Server.MapPath(string.Format("~/assets/videos/{0}/{1}/", rootfoldername, Files[i])));



                    foreach (string filePath in Filespath)
                    {
                        data1.Add(Path.GetFileName(filePath));
                    }
                    data.Add(data1);
                }

                ViewBag.PData = data;
                ViewBag.QData = Files;
            }
            else
            {

            }


            ViewBag.message = "daw";
            return View();
        }


        [HttpGet]
        public string AddSection(string sectionname, string foldername, string check)
        {
            /*
           *  create folder
          */

            if (check == "update")
            {
                check = "videos";
            }
            else
            {
                check = "temp";
            }

            string folder = Server.MapPath(string.Format("~/assets/{0}/{1}/", check, foldername));
            if (Directory.Exists(folder))
            {
                string folder1 = Server.MapPath(string.Format("~/assets/{0}/{1}/{2}/", check, foldername, sectionname));
                if (!Directory.Exists(folder1))
                {
                    Directory.CreateDirectory(folder1);
                    return "true";
                }
                else
                {

                    return "false";
                }

            }

            return "false";
        }


    }
}