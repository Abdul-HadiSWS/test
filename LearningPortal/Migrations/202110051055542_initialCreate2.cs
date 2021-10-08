namespace LearningPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class initialCreate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.SectionMedias", "VideoTitle", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.SectionMedias", "VideoTitle");
        }
    }
}
