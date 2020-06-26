namespace WorkNotes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DateFormatting : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Activity", "Date", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Application", "Date", c => c.DateTime(nullable: false, storeType: "date"));
            AlterColumn("dbo.Job", "PostingDate", c => c.DateTime(nullable: false, storeType: "date"));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Job", "PostingDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Application", "Date", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Activity", "Date", c => c.DateTime(nullable: false));
        }
    }
}
