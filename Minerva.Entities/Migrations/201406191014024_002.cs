namespace Minerva.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _002 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ResourceDiskStructures", "Resource_Id", "dbo.Resources");
            DropForeignKey("dbo.ResourceDiskStructures", "DiskStructure_Id", "dbo.DiskStructures");
            DropIndex("dbo.ResourceDiskStructures", new[] { "Resource_Id" });
            DropIndex("dbo.ResourceDiskStructures", new[] { "DiskStructure_Id" });
            AddColumn("dbo.DiskStructures", "Icon_Id", c => c.Int());
            AddColumn("dbo.AspNetUsers", "DiskStructure_Id", c => c.Int());
            CreateIndex("dbo.DiskStructures", "Icon_Id");
            CreateIndex("dbo.AspNetUsers", "DiskStructure_Id");
            AddForeignKey("dbo.AspNetUsers", "DiskStructure_Id", "dbo.DiskStructures", "Id");
            AddForeignKey("dbo.DiskStructures", "Icon_Id", "dbo.Resources", "Id");
            DropTable("dbo.ResourceDiskStructures");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ResourceDiskStructures",
                c => new
                    {
                        Resource_Id = c.Int(nullable: false),
                        DiskStructure_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Resource_Id, t.DiskStructure_Id });
            
            DropForeignKey("dbo.DiskStructures", "Icon_Id", "dbo.Resources");
            DropForeignKey("dbo.AspNetUsers", "DiskStructure_Id", "dbo.DiskStructures");
            DropIndex("dbo.AspNetUsers", new[] { "DiskStructure_Id" });
            DropIndex("dbo.DiskStructures", new[] { "Icon_Id" });
            DropColumn("dbo.AspNetUsers", "DiskStructure_Id");
            DropColumn("dbo.DiskStructures", "Icon_Id");
            CreateIndex("dbo.ResourceDiskStructures", "DiskStructure_Id");
            CreateIndex("dbo.ResourceDiskStructures", "Resource_Id");
            AddForeignKey("dbo.ResourceDiskStructures", "DiskStructure_Id", "dbo.DiskStructures", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ResourceDiskStructures", "Resource_Id", "dbo.Resources", "Id", cascadeDelete: true);
        }
    }
}
