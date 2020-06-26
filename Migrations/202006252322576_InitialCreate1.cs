namespace WorkNotes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate1 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Activity", "PersonID", "dbo.Person");
            DropIndex("dbo.Activity", new[] { "PersonID" });
            AlterColumn("dbo.Activity", "PersonID", c => c.Int());
            AlterColumn("dbo.Activity", "Notes", c => c.String(maxLength: 600));
            CreateIndex("dbo.Activity", "PersonID");
            AddForeignKey("dbo.Activity", "PersonID", "dbo.Person", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activity", "PersonID", "dbo.Person");
            DropIndex("dbo.Activity", new[] { "PersonID" });
            AlterColumn("dbo.Activity", "Notes", c => c.String(maxLength: 1200));
            AlterColumn("dbo.Activity", "PersonID", c => c.Int(nullable: false));
            CreateIndex("dbo.Activity", "PersonID");
            AddForeignKey("dbo.Activity", "PersonID", "dbo.Person", "ID", cascadeDelete: true);
        }
    }
}
