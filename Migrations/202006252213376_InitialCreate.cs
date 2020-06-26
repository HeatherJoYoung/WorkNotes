namespace WorkNotes.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Activity",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PersonID = c.Int(nullable: false),
                        JobID = c.Int(),
                        ApplicationID = c.Int(),
                        Date = c.DateTime(nullable: false),
                        Type = c.Int(nullable: false),
                        Notes = c.String(maxLength: 1200),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Application", t => t.ApplicationID)
                .ForeignKey("dbo.Person", t => t.PersonID, cascadeDelete: true)
                .ForeignKey("dbo.Job", t => t.JobID)
                .Index(t => t.PersonID)
                .Index(t => t.JobID)
                .Index(t => t.ApplicationID);
            
            CreateTable(
                "dbo.Application",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        JobID = c.Int(nullable: false),
                        Date = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Job", t => t.JobID, cascadeDelete: true)
                .Index(t => t.JobID);
            
            CreateTable(
                "dbo.Job",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyId = c.Int(nullable: false),
                        JobTitle = c.String(nullable: false, maxLength: 40),
                        Description = c.String(),
                        Qualifications = c.String(),
                        PostingDate = c.DateTime(nullable: false),
                        PostingSite = c.String(maxLength: 40),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyId, cascadeDelete: true)
                .Index(t => t.CompanyId);
            
            CreateTable(
                "dbo.Company",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 100),
                        Location = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Person",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        CompanyID = c.Int(nullable: false),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(),
                        Phone = c.String(),
                        Email = c.String(),
                        Title = c.Int(nullable: false),
                        Job_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Company", t => t.CompanyID, cascadeDelete: true)
                .ForeignKey("dbo.Job", t => t.Job_ID)
                .Index(t => t.CompanyID)
                .Index(t => t.Job_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Activity", "JobID", "dbo.Job");
            DropForeignKey("dbo.Activity", "PersonID", "dbo.Person");
            DropForeignKey("dbo.Activity", "ApplicationID", "dbo.Application");
            DropForeignKey("dbo.Application", "JobID", "dbo.Job");
            DropForeignKey("dbo.Person", "Job_ID", "dbo.Job");
            DropForeignKey("dbo.Person", "CompanyID", "dbo.Company");
            DropForeignKey("dbo.Job", "CompanyId", "dbo.Company");
            DropIndex("dbo.Person", new[] { "Job_ID" });
            DropIndex("dbo.Person", new[] { "CompanyID" });
            DropIndex("dbo.Job", new[] { "CompanyId" });
            DropIndex("dbo.Application", new[] { "JobID" });
            DropIndex("dbo.Activity", new[] { "ApplicationID" });
            DropIndex("dbo.Activity", new[] { "JobID" });
            DropIndex("dbo.Activity", new[] { "PersonID" });
            DropTable("dbo.Person");
            DropTable("dbo.Company");
            DropTable("dbo.Job");
            DropTable("dbo.Application");
            DropTable("dbo.Activity");
        }
    }
}
