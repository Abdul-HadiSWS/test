namespace LearningPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Categories", "INDEX_Title");
            DropIndex("dbo.SubCategories", "INDEX_Title");
            DropIndex("dbo.Courses", "INDEX_Title");
            AddColumn("dbo.Categories", "CategoryName", c => c.String(nullable: false, maxLength: 55));
            AddColumn("dbo.SubCategories", "SubCategoryName", c => c.String(nullable: false, maxLength: 75));
            AddColumn("dbo.Courses", "CourseName", c => c.String(nullable: false, maxLength: 55));
            AddColumn("dbo.Sections", "SectionName", c => c.String(nullable: false));
            CreateIndex("dbo.Categories", "CategoryName", unique: true, name: "INDEX_Title");
            CreateIndex("dbo.SubCategories", "SubCategoryName", unique: true, name: "INDEX_Title");
            CreateIndex("dbo.Courses", "CourseName", unique: true, name: "INDEX_Title");
            DropColumn("dbo.Categories", "Title");
            DropColumn("dbo.SubCategories", "Title");
            DropColumn("dbo.Courses", "Title");
            DropColumn("dbo.Sections", "Title");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sections", "Title", c => c.String(nullable: false));
            AddColumn("dbo.Courses", "Title", c => c.String(nullable: false, maxLength: 55));
            AddColumn("dbo.SubCategories", "Title", c => c.String(nullable: false, maxLength: 75));
            AddColumn("dbo.Categories", "Title", c => c.String(nullable: false, maxLength: 55));
            DropIndex("dbo.Courses", "INDEX_Title");
            DropIndex("dbo.SubCategories", "INDEX_Title");
            DropIndex("dbo.Categories", "INDEX_Title");
            DropColumn("dbo.Sections", "SectionName");
            DropColumn("dbo.Courses", "CourseName");
            DropColumn("dbo.SubCategories", "SubCategoryName");
            DropColumn("dbo.Categories", "CategoryName");
            CreateIndex("dbo.Courses", "Title", unique: true, name: "INDEX_Title");
            CreateIndex("dbo.SubCategories", "Title", unique: true, name: "INDEX_Title");
            CreateIndex("dbo.Categories", "Title", unique: true, name: "INDEX_Title");
        }
    }
}
