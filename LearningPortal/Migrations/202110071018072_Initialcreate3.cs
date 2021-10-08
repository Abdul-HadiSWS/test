namespace LearningPortal.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initialcreate3 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.UserMediaHistories", "WatchedTime", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.UserMediaHistories", "WatchedTime", c => c.Double(nullable: false));
        }
    }
}
