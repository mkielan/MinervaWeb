namespace Minerva.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ApplicationUserDiskStructures", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserDiskStructures", "DiskStructure_Id", "dbo.DiskStructures");
            DropIndex("dbo.ApplicationUserDiskStructures", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.ApplicationUserDiskStructures", new[] { "DiskStructure_Id" });
            CreateTable(
                "dbo.DiskStructureAccesses",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.DiskStructureDiskStructureAccesses",
                c => new
                    {
                        DiskStructure_Id = c.Int(nullable: false),
                        DiskStructureAccess_UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.DiskStructure_Id, t.DiskStructureAccess_UserId })
                .ForeignKey("dbo.DiskStructures", t => t.DiskStructure_Id, cascadeDelete: true)
                .ForeignKey("dbo.DiskStructureAccesses", t => t.DiskStructureAccess_UserId, cascadeDelete: true)
                .Index(t => t.DiskStructure_Id)
                .Index(t => t.DiskStructureAccess_UserId);
            
            DropTable("dbo.ApplicationUserDiskStructures");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ApplicationUserDiskStructures",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        DiskStructure_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.DiskStructure_Id });
            
            DropForeignKey("dbo.DiskStructureDiskStructureAccesses", "DiskStructureAccess_UserId", "dbo.DiskStructureAccesses");
            DropForeignKey("dbo.DiskStructureDiskStructureAccesses", "DiskStructure_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.DiskStructureAccesses", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.DiskStructureDiskStructureAccesses", new[] { "DiskStructureAccess_UserId" });
            DropIndex("dbo.DiskStructureDiskStructureAccesses", new[] { "DiskStructure_Id" });
            DropIndex("dbo.DiskStructureAccesses", new[] { "UserId" });
            DropTable("dbo.DiskStructureDiskStructureAccesses");
            DropTable("dbo.DiskStructureAccesses");
            CreateIndex("dbo.ApplicationUserDiskStructures", "DiskStructure_Id");
            CreateIndex("dbo.ApplicationUserDiskStructures", "ApplicationUser_Id");
            AddForeignKey("dbo.ApplicationUserDiskStructures", "DiskStructure_Id", "dbo.DiskStructures", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ApplicationUserDiskStructures", "ApplicationUser_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
    }
}
