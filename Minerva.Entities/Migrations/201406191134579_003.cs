namespace Minerva.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _003 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.AspNetUsers", "DiskStructure_Id", "dbo.DiskStructures");
            DropIndex("dbo.AspNetUsers", new[] { "DiskStructure_Id" });
            CreateTable(
                "dbo.ApplicationUserDiskStructures",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        DiskStructure_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.DiskStructure_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.DiskStructures", t => t.DiskStructure_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.DiskStructure_Id);
            
            DropColumn("dbo.AspNetUsers", "DiskStructure_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.AspNetUsers", "DiskStructure_Id", c => c.Int());
            DropForeignKey("dbo.ApplicationUserDiskStructures", "DiskStructure_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.ApplicationUserDiskStructures", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserDiskStructures", new[] { "DiskStructure_Id" });
            DropIndex("dbo.ApplicationUserDiskStructures", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserDiskStructures");
            CreateIndex("dbo.AspNetUsers", "DiskStructure_Id");
            AddForeignKey("dbo.AspNetUsers", "DiskStructure_Id", "dbo.DiskStructures", "Id");
        }
    }
}
