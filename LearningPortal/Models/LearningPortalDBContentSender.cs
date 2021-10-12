using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace LearningPortal.Models
{
    public class LearningPortalDBContentSender : DropCreateDatabaseIfModelChanges<ApplicationDbContext>
    {
        protected override void Seed(ApplicationDbContext context)
        {
            //  This method will be called after migrating to the latest version.

            //  You can use the DbSet<T>.AddOrUpdate() helper extension method 
            //  to avoid creating duplicate seed data.
            context.Categories.AddOrUpdate(
                 new Categories() { CategoryName = "Audio/Video" },
                 new Categories() { CategoryName = "Communication" },
                 new Categories() { CategoryName = "Design" },
                 new Categories() { CategoryName = "Development" },
                 new Categories() { CategoryName = "Marketing" },
                 new Categories() { CategoryName = "Miscellaneous" },
                 new Categories() { CategoryName = "Project Management" },
                 new Categories() { CategoryName = "Soft Skills" },
                 new Categories() { CategoryName = "Writing" },
                 new Categories() { CategoryName = " IT & Networks" }

                     );
            context.SubCategories.AddOrUpdate(
                new SubCategories() { CategoryId = 1, SubCategoryName = "Java" },
                new SubCategories() { CategoryId = 1, SubCategoryName = ".Net" },
                new SubCategories() { CategoryId = 1, SubCategoryName = "Php" },
                new SubCategories() { CategoryId = 3, SubCategoryName = "Video Graphy" },
                new SubCategories() { CategoryId = 3, SubCategoryName = "Audio Editor" },
                new SubCategories() { CategoryId = 4, SubCategoryName = "Content Writing" },
                new SubCategories() { CategoryId = 4, SubCategoryName = "Technical Writing" },
                new SubCategories() { CategoryId = 4, SubCategoryName = "Eassy Writing" },
                new SubCategories() { CategoryId = 5, SubCategoryName = "Networking" },
                new SubCategories() { CategoryId = 5, SubCategoryName = "Cisico" },
                new SubCategories() { CategoryId = 6, SubCategoryName = "Web Designing" },
                new SubCategories() { CategoryId = 6, SubCategoryName = "Bootstrap" },
                new SubCategories() { CategoryId = 7, SubCategoryName = "Agile" },
                new SubCategories() { CategoryId = 7, SubCategoryName = "Scurm" },
                new SubCategories() { CategoryId = 8, SubCategoryName = "Time Management" },
                new SubCategories() { CategoryId = 8, SubCategoryName = "Temper Management" },
                new SubCategories() { CategoryId = 9, SubCategoryName = "Vision & Communication" },
                new SubCategories() { CategoryId = 9, SubCategoryName = "HCI" },
                new SubCategories() { CategoryId = 10, SubCategoryName = "Bowling Actions" },
                new SubCategories() { CategoryId = 10, SubCategoryName = "Rope skipping" }
                );
            context.Courses.AddOrUpdate(
                new Courses() { Description = "JAVA was developed by James Gosling at Sun Microsystems Inc in the year 1991, later acquired by Oracle Corporation.It is a simple programming language.Java makes writing, compiling, and debugging programming easy. ... Java applications are compiled to byte code that can run on any Java Virtual Machine", Levels = "Easy", Year = 2006, Image = "1280_rsz_aman - dhakal - 205796 - unsplash.jpg", SubCategoryId = 1, IsFeatured = true, CourseName = "Introduction To Java" },
                new Courses() { Description = "ASP.NET is a web application framework designed and developed by Microsoft. ASP.NET is open source and a subset of the . NET Framework and successor of the classic ASP(Active Server Pages). ... ASP.NET is built on the CLR(Common Language Runtime) which allows the programmers to execute its code using any .", Levels = "Easy", Year = 2007, Image = "256_rsz_karl-s-973833-unsplash.jpg", SubCategoryId = 2, IsFeatured = false, CourseName = "Introduction To .nET" },
                new Courses() { Description = "PHP started out as a small open source project that evolved as more and more people found out how useful it was. Rasmus Lerdorf unleashed the first version of PHP way back in 1994.", Levels = "Easy", Year = 2004, Image = "256_rsz_clem-onojeghuo-150467-unsplash.jpg", SubCategoryId = 3, IsFeatured = true, CourseName = "Intoduction To Php" },
                new Courses() { Description = "In statistics, ordinary least squares (OLS) is a type of linear least squares method for estimating the unknown parameters in a linear regression model. OLS chooses the parameters of a linear function of a set of explanatory variables by the principle of least squares: minimizing the sum of the squares of the differences between the observed dependent variable (values of the variable being observed) in the given dataset and those predicted by the linear function of the independent variable.", Levels = "Easy", Year = 2006, Image = "256_rsz_karl-s-973833-unsplash.jpg", SubCategoryId = 6, IsFeatured = true, CourseName = "OLSs" },
                new Courses() { Description = "A design is a plan or specification for the construction of an object or system or for the implementation of an activity or process, or the result of that plan or specification in the form of a prototype, product or process. The verb to design expresses the process of developing a design.", Levels = "Easy", Year = 2006, Image = "256_rsz_nicolas-horn-689011-unsplash.jpg", SubCategoryId = 13, IsFeatured = true, CourseName = "AutoCAD" },
                new Courses() { Description = "Machine learning is a subfield of artificial intelligence (AI). The goal of machine learning generally is to understand the structure of data and fit that data into models that can be understood and utilized by people.", Levels = "Medium", Year = 2019, Image = "256_rsz_sharina-mae-agellon-377466-unsplash.jpg", SubCategoryId = 3, IsFeatured = true, CourseName = "Introduction To ML" },
                new Courses() { Description = "Deep learning is a branch of machine learning which is completely based on artificial neural networks, as neural network is going to mimic the human brain so deep learning is also a kind of mimic of human brain. In deep learning, we don't need to explicitly program everything.", Levels = "Hard", Year = 2020, Image = "265_rsz_mubariz-mehdizadeh-364026-unsplash.jpg", SubCategoryId = 3, IsFeatured = true, CourseName = "Introduction To DeepLearing" },
                new Courses() { Description = "Computer vision is a field of study focused on the problem of helping computers to see. At an abstract level, the goal of computer vision problems is to use the observed image data to infer something about the world.", Levels = "Hard", Year = 2021, Image = "256_rsz_sharina-mae-agellon-377466-unsplash.jpg", SubCategoryId = 3, IsFeatured = true, CourseName = "Introduction To OpenCv" },
                new Courses() { Description = "Neuro-linguistic programming (NLP) is a psychological approach that involves analyzing strategies used by successful individuals and applying them to reach a personal goal. It relates thoughts, language, and patterns of behavior learned through experience to specific outcomes.", Levels = "Hard", Year = 2021, Image = "265_rsz_mubariz-mehdizadeh-364026-unsplash.jpg", SubCategoryId = 1, IsFeatured = true, CourseName = "Introduction To NLP" },
                new Courses() { Description = "Neuro-linguistic programming (NLP) is a psychological approach that involves analyzing strategies used by successful individuals and applying them to reach a personal goal. It relates thoughts, language, and patterns of behavior learned through experience to specific outcomes.", Levels = "Hard", Year = 2021, Image = "265_rsz_mubariz-mehdizadeh-364026-unsplash.jpg", SubCategoryId = 1, IsFeatured = true, CourseName = "Indepth NLP" },
                new Courses() { Description = "Neuro-linguistic programming (NLP) is a psychological approach that involves analyzing strategies used by successful individuals and applying them to reach a personal goal. It relates thoughts, language, and patterns of behavior learned through experience to specific outcomes.", Levels = "Hard", Year = 2021, Image = "265_rsz_mubariz-mehdizadeh-364026-unsplash.jpg", SubCategoryId = 1, IsFeatured = false, CourseName = "Open CV" },
                new Courses() { Description = "Neuro-linguistic programming (NLP) is a psychological approach that involves analyzing strategies used by successful individuals and applying them to reach a personal goal. It relates thoughts, language, and patterns of behavior learned through experience to specific outcomes.", Levels = "Hard", Year = 2021, Image = "265_rsz_mubariz-mehdizadeh-364026-unsplash.jpg", SubCategoryId = 1, IsFeatured = true, CourseName = "Big Data" }
            );
            context.Sections.AddOrUpdate(
                new Section() { SectionName = "Intro", CourseId = 1 },
                new Section() { SectionName = "Installation", CourseId = 1 },
                new Section() { SectionName = "Fundamentals", CourseId = 1 },
                new Section() { SectionName = "OOP", CourseId = 1 },
                new Section() { SectionName = "Data Structure", CourseId = 1 },
                new Section() { SectionName = "Project", CourseId = 1 },
                new Section() { SectionName = "Introduction", CourseId = 2 },
                new Section() { SectionName = "PhpIntro", CourseId = 3 }
            );


            context.SectionMedia.AddOrUpdate(
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/mzciCkrZLyI", SectionId = 1, VideoTitle = "Video1" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/TThZIt4r3eg", SectionId = 1, VideoTitle = "Video2" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/cb-p_gkhIC0", SectionId = 1, VideoTitle = "Video3" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = 2, VideoTitle = "Video1" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = 2, VideoTitle = "Video2" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = 2, VideoTitle = "Video3" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = 2, VideoTitle = "Video4" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = 3, VideoTitle = "Video1" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = 3, VideoTitle = "Video2" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = 3, VideoTitle = "Video3" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = 1, VideoTitle = "Video4" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = 5, VideoTitle = "Video1" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = 6, VideoTitle = "Video1" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = 7, VideoTitle = "Introduction" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = 8, VideoTitle = "Video1" }


              );
            base.Seed(context);

            //context.SaveChanges();
            //context.UserMediaHistories.AddOrUpdate(
            //    new UserMediaHistory() {UserId=,SectionMediaId=,WatchedTime=,UpdatedTime=0},
            //    new UserMediaHistory() { UserId =, SectionMediaId =, WatchedTime =, UpdatedTime = 0 },
            //    new UserMediaHistory() { UserId =, SectionMediaId =, WatchedTime =, UpdatedTime = 0 },
            //    new UserMediaHistory() { UserId =, SectionMediaId =, WatchedTime =, UpdatedTime = 0 },
            //    new UserMediaHistory() { UserId =, SectionMediaId =, WatchedTime =, UpdatedTime = 0 },
            //    new UserMediaHistory() { UserId =, SectionMediaId =, WatchedTime =, UpdatedTime = 0 },
            //    new UserMediaHistory() { UserId =, SectionMediaId =, WatchedTime =, UpdatedTime = 0 }

            //    );

        }
    }
}