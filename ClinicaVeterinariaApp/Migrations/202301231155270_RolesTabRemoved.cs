namespace ClinicaVeterinariaApp.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RolesTabRemoved : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Users", "RoleID", "dbo.Roles");
            DropIndex("dbo.Users", new[] { "RoleID" });
            AddColumn("dbo.Users", "Role", c => c.String(maxLength: 20));
            DropColumn("dbo.Users", "RoleID");
            DropTable("dbo.Roles");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Roles",
                c => new
                    {
                        RoleID = c.Int(nullable: false, identity: true),
                        Role = c.String(nullable: false, maxLength: 20),
                    })
                .PrimaryKey(t => t.RoleID);
            
            AddColumn("dbo.Users", "RoleID", c => c.Int(nullable: false));
            DropColumn("dbo.Users", "Role");
            CreateIndex("dbo.Users", "RoleID");
            AddForeignKey("dbo.Users", "RoleID", "dbo.Roles", "RoleID");
        }
    }
}
