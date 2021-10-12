namespace LearningPortal.Migrations
{
    using LearningPortal.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<LearningPortal.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(LearningPortal.Models.ApplicationDbContext context)
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
                         new Categories() { CategoryName = "IT & Networks" }


                             );
            context.SaveChanges();
            context.SubCategories.AddOrUpdate(

                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Audio/Video").CategoryId, SubCategoryName = "Video Graphy" },
                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Audio/Video").CategoryId, SubCategoryName = "Audio Editor" },

                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Communication").CategoryId, SubCategoryName = "Content Writing" },
                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Communication").CategoryId, SubCategoryName = "Technical Writing" },
                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Communication").CategoryId, SubCategoryName = "Eassy Writing" },

                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Design").CategoryId, SubCategoryName = "Web Designing" },
                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Design").CategoryId, SubCategoryName = "Bootstrap" },

                      new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Development").CategoryId, SubCategoryName = "Java" },
                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Development").CategoryId, SubCategoryName = ".Net" },
                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Development").CategoryId, SubCategoryName = "Php" },

                      new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Marketing").CategoryId, SubCategoryName = "HCI" },

                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Miscellaneous").CategoryId, SubCategoryName = "Bowling Actions" },
                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Miscellaneous").CategoryId, SubCategoryName = "Rope skipping" },

                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Project Management").CategoryId, SubCategoryName = "Agile" },
                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Project Management").CategoryId, SubCategoryName = "Scurm" },
                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Project Management").CategoryId, SubCategoryName = "Time Management" },
                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Project Management").CategoryId, SubCategoryName = "Temper Management" },


                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "Soft Skills").CategoryId, SubCategoryName = "Vision & Communication" },

                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "IT & Networks").CategoryId, SubCategoryName = "Networking" },
                     new SubCategories() { CategoryId = context.Categories.Single(i => i.CategoryName == "IT & Networks").CategoryId, SubCategoryName = "Cisico" }


                );
            context.SaveChanges();

            context.Courses.AddOrUpdate(
                new Courses() { Description = "JAVA was developed by James Gosling at Sun Microsystems Inc in the year 1991, later acquired by Oracle Corporation.It is a simple programming language.Java makes writing, compiling, and debugging programming easy. ... Java applications are compiled to byte code that can run on any Java Virtual Machine", Levels = "Easy", Year = 2006, Image = "1280_rsz_aman - dhakal - 205796 - unsplash.jpg", SubCategoryId = context.SubCategories.Single(i => i.SubCategoryName == "Java").SubCategoryId, IsFeatured = true, CourseName = "Introduction To Java" },
                new Courses() { Description = "ASP.NET is a web application framework designed and developed by Microsoft. ASP.NET is open source and a subset of the . NET Framework and successor of the classic ASP(Active Server Pages). ... ASP.NET is built on the CLR(Common Language Runtime) which allows the programmers to execute its code using any .", Levels = "Easy", Year = 2007, Image = "256_rsz_karl-s-973833-unsplash.jpg", SubCategoryId = context.SubCategories.Single(i => i.SubCategoryName == ".Net").SubCategoryId, IsFeatured = false, CourseName = "Introduction To .NET" },
                new Courses() { Description = "PHP started out as a small open source project that evolved as more and more people found out how useful it was. Rasmus Lerdorf unleashed the first version of PHP way back in 1994.", Levels = "Easy", Year = 2004, Image = "256_rsz_clem-onojeghuo-150467-unsplash.jpg", SubCategoryId = context.SubCategories.Single(i => i.SubCategoryName == "Php").SubCategoryId, IsFeatured = true, CourseName = "Introduction To Php" },
                new Courses() { Description = "In statistics, ordinary least squares (OLS) is a type of linear least squares method for estimating the unknown parameters in a linear regression model. OLS chooses the parameters of a linear function of a set of explanatory variables by the principle of least squares: minimizing the sum of the squares of the differences between the observed dependent variable (values of the variable being observed) in the given dataset and those predicted by the linear function of the independent variable.", Levels = "Easy", Year = 2006, Image = "256_rsz_karl-s-973833-unsplash.jpg", SubCategoryId = context.SubCategories.Single(i => i.SubCategoryName == "Video Graphy").SubCategoryId, IsFeatured = true, CourseName = "OLSs" },
                new Courses() { Description = "A design is a plan or specification for the construction of an object or system or for the implementation of an activity or process, or the result of that plan or specification in the form of a prototype, product or process. The verb to design expresses the process of developing a design.", Levels = "Easy", Year = 2006, Image = "256_rsz_nicolas-horn-689011-unsplash.jpg", SubCategoryId = context.SubCategories.Single(i => i.SubCategoryName == "Web Designing").SubCategoryId, IsFeatured = true, CourseName = "AutoCAD" },
                new Courses() { Description = "Machine learning is a subfield of artificial intelligence (AI). The goal of machine learning generally is to understand the structure of data and fit that data into models that can be understood and utilized by people.", Levels = "Medium", Year = 2019, Image = "256_rsz_sharina-mae-agellon-377466-unsplash.jpg", SubCategoryId = context.SubCategories.Single(i => i.SubCategoryName == ".Net").SubCategoryId, IsFeatured = true, CourseName = "Introduction To ML" },
                new Courses() { Description = "Deep learning is a branch of machine learning which is completely based on artificial neural networks, as neural network is going to mimic the human brain so deep learning is also a kind of mimic of human brain. In deep learning, we don't need to explicitly program everything.", Levels = "Hard", Year = 2020, Image = "265_rsz_mubariz-mehdizadeh-364026-unsplash.jpg", SubCategoryId = context.SubCategories.Single(i => i.SubCategoryName == ".Net").SubCategoryId, IsFeatured = true, CourseName = "Introduction To DeepLearing" },
                new Courses() { Description = "Computer vision is a field of study focused on the problem of helping computers to see. At an abstract level, the goal of computer vision problems is to use the observed image data to infer something about the world.", Levels = "Hard", Year = 2021, Image = "256_rsz_sharina-mae-agellon-377466-unsplash.jpg", SubCategoryId = context.SubCategories.Single(i => i.SubCategoryName == ".Net").SubCategoryId, IsFeatured = true, CourseName = "Introduction To OpenCv" },
                new Courses() { Description = "Neuro-linguistic programming (NLP) is a psychological approach that involves analyzing strategies used by successful individuals and applying them to reach a personal goal. It relates thoughts, language, and patterns of behavior learned through experience to specific outcomes.", Levels = "Hard", Year = 2021, Image = "265_rsz_mubariz-mehdizadeh-364026-unsplash.jpg", SubCategoryId = context.SubCategories.Single(i => i.SubCategoryName == ".Net").SubCategoryId, IsFeatured = true, CourseName = "Introduction To NLP" },
                new Courses() { Description = "Neuro-linguistic programming (NLP) is a psychological approach that involves analyzing strategies used by successful individuals and applying them to reach a personal goal. It relates thoughts, language, and patterns of behavior learned through experience to specific outcomes.", Levels = "Hard", Year = 2021, Image = "265_rsz_mubariz-mehdizadeh-364026-unsplash.jpg", SubCategoryId = context.SubCategories.Single(i => i.SubCategoryName == ".Net").SubCategoryId, IsFeatured = true, CourseName = "Indepth NLP" },
                new Courses() { Description = "Neuro-linguistic programming (NLP) is a psychological approach that involves analyzing strategies used by successful individuals and applying them to reach a personal goal. It relates thoughts, language, and patterns of behavior learned through experience to specific outcomes.", Levels = "Hard", Year = 2021, Image = "265_rsz_mubariz-mehdizadeh-364026-unsplash.jpg", SubCategoryId = context.SubCategories.Single(i => i.SubCategoryName == ".Net").SubCategoryId, IsFeatured = false, CourseName = "Open CV" },
                new Courses() { Description = "Neuro-linguistic programming (NLP) is a psychological approach that involves analyzing strategies used by successful individuals and applying them to reach a personal goal. It relates thoughts, language, and patterns of behavior learned through experience to specific outcomes.", Levels = "Hard", Year = 2021, Image = "265_rsz_mubariz-mehdizadeh-364026-unsplash.jpg", SubCategoryId = context.SubCategories.Single(i => i.SubCategoryName == ".Net").SubCategoryId, IsFeatured = true, CourseName = "Big Data" }
            );
            context.SaveChanges();

            context.Sections.AddOrUpdate(
                new Section() { SectionName = "Intro", CourseId = context.Courses.Single(i => i.CourseName == "Introduction To Java").CourseId },
                new Section() { SectionName = "Installation", CourseId = context.Courses.Single(i => i.CourseName == "Introduction To Java").CourseId },
                new Section() { SectionName = "Fundamentals", CourseId = context.Courses.Single(i => i.CourseName == "Introduction To Java").CourseId },
                new Section() { SectionName = "OOP", CourseId = context.Courses.Single(i => i.CourseName == "Introduction To Java").CourseId },
                new Section() { SectionName = "Data Structure", CourseId = context.Courses.Single(i => i.CourseName == "Introduction To Java").CourseId },
                new Section() { SectionName = "Project", CourseId = context.Courses.Single(i => i.CourseName == "Introduction To Java").CourseId },

                new Section() { SectionName = "Introduction", CourseId = context.Courses.Single(i => i.CourseName == "Introduction To .NET").CourseId },

            new Section() { SectionName = "PhpIntro", CourseId = context.Courses.Single(i => i.CourseName == "Introduction To Php").CourseId }
            );
            context.SaveChanges();


            context.SectionMedia.AddOrUpdate(
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/mzciCkrZLyI", SectionId = context.Sections.Single(i => i.SectionName == "Intro").SectionId, VideoTitle = "Video1" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/TThZIt4r3eg", SectionId = context.Sections.Single(i => i.SectionName == "Intro").SectionId, VideoTitle = "Video2" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/cb-p_gkhIC0", SectionId = context.Sections.Single(i => i.SectionName == "Intro").SectionId, VideoTitle = "Video3" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = context.Sections.Single(i => i.SectionName == "Installation").SectionId, VideoTitle = "Video1" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = context.Sections.Single(i => i.SectionName == "Installation").SectionId, VideoTitle = "Video2" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = context.Sections.Single(i => i.SectionName == "Installation").SectionId, VideoTitle = "Video3" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = context.Sections.Single(i => i.SectionName == "Installation").SectionId, VideoTitle = "Video4" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = context.Sections.Single(i => i.SectionName == "Fundamentals").SectionId, VideoTitle = "Video1" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = context.Sections.Single(i => i.SectionName == "Fundamentals").SectionId, VideoTitle = "Video2" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = context.Sections.Single(i => i.SectionName == "Fundamentals").SectionId, VideoTitle = "Video3" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = context.Sections.Single(i => i.SectionName == "Data Structure").SectionId, VideoTitle = "Video4" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = context.Sections.Single(i => i.SectionName == "Project").SectionId, VideoTitle = "Video1" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = context.Sections.Single(i => i.SectionName == "Introduction").SectionId, VideoTitle = "Video1" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = context.Sections.Single(i => i.SectionName == "PhpIntro").SectionId, VideoTitle = "Video1" },
              new SectionMedia() { VideoUrl = "https://www.youtube.com/embed/nPYuVfdJPaQ", SectionId = context.Sections.Single(i => i.SectionName == "PhpIntro").SectionId, VideoTitle = "Video2" }

            );



        }
    }
}
