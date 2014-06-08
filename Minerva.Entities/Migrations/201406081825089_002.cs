namespace Minerva.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _002 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Sources", "Owner_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Sources", new[] { "Owner_Id" });
            RenameColumn(table: "dbo.Sources", name: "Owner_Id", newName: "ApplicationUser_Id");
            AddColumn("dbo.AspNetUsers", "Source_Id1", c => c.Long());
            AlterColumn("dbo.Sources", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "Source_Id1");
            CreateIndex("dbo.Sources", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "Source_Id1", "dbo.Sources", "Id");
            AddForeignKey("dbo.Sources", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Sources", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Source_Id1", "dbo.Sources");
            DropIndex("dbo.Sources", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Source_Id1" });
            AlterColumn("dbo.Sources", "ApplicationUser_Id", c => c.String(nullable: false, maxLength: 128));
            DropColumn("dbo.AspNetUsers", "Source_Id1");
            RenameColumn(table: "dbo.Sources", name: "ApplicationUser_Id", newName: "Owner_Id");
            CreateIndex("dbo.Sources", "Owner_Id");
            AddForeignKey("dbo.Sources", "Owner_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
