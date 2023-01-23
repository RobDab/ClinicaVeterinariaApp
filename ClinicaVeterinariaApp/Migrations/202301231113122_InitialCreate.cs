namespace ClinicaVeterinariaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Animals",
                c => new
                    {
                        IDAnimal = c.Int(nullable: false, identity: true),
                        RegisterDate = c.DateTime(nullable: false, storeType: "date"),
                        Name = c.String(nullable: false, maxLength: 20),
                        SpecieID = c.Int(nullable: false),
                        Color = c.String(nullable: false, maxLength: 20),
                        BirthDate = c.DateTime(nullable: false, storeType: "date"),
                        HasChip = c.Boolean(nullable: false),
                        ChipNumber = c.String(maxLength: 10),
                        HasOwner = c.Boolean(nullable: false),
                        OwnerName = c.String(maxLength: 20),
                        OwnerLastname = c.String(maxLength: 20),
                        UrlPhoto = c.String(maxLength: 15),
                    })
                .PrimaryKey(t => t.IDAnimal)
                .ForeignKey("dbo.Species", t => t.SpecieID)
                .Index(t => t.SpecieID);
            
            CreateTable(
                "dbo.Exams",
                c => new
                    {
                        ExamID = c.Int(nullable: false, identity: true),
                        IDAnimal = c.Int(nullable: false),
                        ExamDate = c.DateTime(nullable: false, storeType: "date"),
                        Exam = c.String(nullable: false, maxLength: 30),
                        ExamNotes = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ExamID)
                .ForeignKey("dbo.Animals", t => t.IDAnimal)
                .Index(t => t.IDAnimal);
            
            CreateTable(
                "dbo.Species",
                c => new
                    {
                        SpecieID = c.Int(nullable: false, identity: true),
                        Specie = c.String(nullable: false, maxLength: 30),
                    })
                .PrimaryKey(t => t.SpecieID);
            
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Role = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.RoleID);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserID = c.Int(nullable: false, identity: true),
                        Username = c.String(nullable: false, maxLength: 20),
                        Password = c.String(nullable: false, maxLength: 20),
                        RoleID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserID)
                .ForeignKey("dbo.Roles", t => t.RoleID)
                .Index(t => t.RoleID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropForeignKey("dbo.Animals", "SpecieID", "dbo.Species");
            DropForeignKey("dbo.Exams", "IDAnimal", "dbo.Animals");
            DropIndex("dbo.Users", new[] { "RoleID" });
            DropIndex("dbo.Exams", new[] { "IDAnimal" });
            DropIndex("dbo.Animals", new[] { "SpecieID" });
            DropTable("dbo.Users");
            DropTable("dbo.Roles");
            DropTable("dbo.Species");
            DropTable("dbo.Exams");
            DropTable("dbo.Animals");
        }
    }
}
