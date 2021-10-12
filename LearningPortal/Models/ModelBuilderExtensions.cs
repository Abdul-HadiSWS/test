using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;

namespace LearningPortal.Models
{
    public static class ModelBuilderExtensions
    {
        public static void Seed(LearningPortal.Models.ApplicationDbContext context)
        {
            context.Categories.AddOrUpdate(
                 new Categories() { CategoryName = "Jane Austen" }

                 );
        }
    }
}