namespace Minerva.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FtpSources", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FtpSources", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FtpSources", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sources", "FtpSource_Id", "dbo.FtpSources");
            DropForeignKey("dbo.FtpSources", "Source_Id", "dbo.Sources");
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.FtpSources", new[] { "CreatedBy_Id" });
            DropIndex("dbo.FtpSources", new[] { "DeletedBy_Id" });
            DropIndex("dbo.FtpSources", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.FtpSources", new[] { "Source_Id" });
            DropIndex("dbo.Sources", new[] { "FtpSource_Id" });
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                        CreatedBy_Id = c.String(maxLength: 128),
                        DeletedBy_Id = c.String(maxLength: 128),
                        ModifiedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(maxLength: 100),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                        CreatedBy_Id = c.String(maxLength: 128),
                        DeletedBy_Id = c.String(maxLength: 128),
                        ModifiedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.ResourceDiskStructures",
                c => new
                    {
                        Resource_Id = c.Long(nullable: false),
                        DiskStructure_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Resource_Id, t.DiskStructure_Id })
                .ForeignKey("dbo.Resources", t => t.Resource_Id, cascadeDelete: true)
                .ForeignKey("dbo.DiskStructures", t => t.DiskStructure_Id, cascadeDelete: true)
                .Index(t => t.Resource_Id)
                .Index(t => t.DiskStructure_Id);
            
            CreateTable(
                "dbo.TagDiskStructures",
                c => new
                    {
                        Tag_Id = c.Long(nullable: false),
                        DiskStructure_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.DiskStructure_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.DiskStructures", t => t.DiskStructure_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.DiskStructure_Id);
            
            AddColumn("dbo.AspNetUsers", "Phone", c => c.String(maxLength: 20));
            AddColumn("dbo.AspNetUsers", "DiskStructure_Id", c => c.Long());
            AddColumn("dbo.DiskStructures", "ApplicationUser_Id", c => c.String(maxLength: 128));
            CreateIndex("dbo.AspNetUsers", "DiskStructure_Id");
            CreateIndex("dbo.DiskStructures", "ApplicationUser_Id");
            AddForeignKey("dbo.AspNetUsers", "DiskStructure_Id", "dbo.DiskStructures", "Id");
            AddForeignKey("dbo.DiskStructures", "ApplicationUser_Id", "dbo.AspNetUsers", "Id");
            DropColumn("dbo.Comments", "Time");
            DropColumn("dbo.Comments", "Author_Id");
            DropColumn("dbo.Sources", "FtpSource_Id");
            DropTable("dbo.FtpSources");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.FtpSources",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Username = c.String(nullable: false),
                        Password = c.String(nullable: false),
                        PasswordSalt = c.String(nullable: false),
                        Address = c.String(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                        CreatedBy_Id = c.String(maxLength: 128),
                        DeletedBy_Id = c.String(maxLength: 128),
                        ModifiedBy_Id = c.String(maxLength: 128),
                        Source_Id = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.Sources", "FtpSource_Id", c => c.Long());
            AddColumn("dbo.Comments", "Author_Id", c => c.String(maxLength: 128));
            AddColumn("dbo.Comments", "Time", c => c.DateTime(nullable: false));
            DropForeignKey("dbo.DiskStructures", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tags", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TagDiskStructures", "DiskStructure_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.TagDiskStructures", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Tags", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tags", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Resources", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ResourceDiskStructures", "DiskStructure_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.ResourceDiskStructures", "Resource_Id", "dbo.Resources");
            DropForeignKey("dbo.Resources", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Resources", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "DiskStructure_Id", "dbo.DiskStructures");
            DropIndex("dbo.TagDiskStructures", new[] { "DiskStructure_Id" });
            DropIndex("dbo.TagDiskStructures", new[] { "Tag_Id" });
            DropIndex("dbo.ResourceDiskStructures", new[] { "DiskStructure_Id" });
            DropIndex("dbo.ResourceDiskStructures", new[] { "Resource_Id" });
            DropIndex("dbo.Tags", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Tags", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Tags", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Resources", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Resources", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Resources", new[] { "CreatedBy_Id" });
            DropIndex("dbo.DiskStructures", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "DiskStructure_Id" });
            DropColumn("dbo.DiskStructures", "ApplicationUser_Id");
            DropColumn("dbo.AspNetUsers", "DiskStructure_Id");
            DropColumn("dbo.AspNetUsers", "Phone");
            DropTable("dbo.TagDiskStructures");
            DropTable("dbo.ResourceDiskStructures");
            DropTable("dbo.Tags");
            DropTable("dbo.Resources");
            CreateIndex("dbo.Sources", "FtpSource_Id");
            CreateIndex("dbo.FtpSources", "Source_Id");
            CreateIndex("dbo.FtpSources", "ModifiedBy_Id");
            CreateIndex("dbo.FtpSources", "DeletedBy_Id");
            CreateIndex("dbo.FtpSources", "CreatedBy_Id");
            CreateIndex("dbo.Comments", "Author_Id");
            AddForeignKey("dbo.FtpSources", "Source_Id", "dbo.Sources", "Id", cascadeDelete: true);
            AddForeignKey("dbo.Sources", "FtpSource_Id", "dbo.FtpSources", "Id");
            AddForeignKey("dbo.FtpSources", "ModifiedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FtpSources", "DeletedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.FtpSources", "CreatedBy_Id", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Comments", "Author_Id", "dbo.AspNetUsers", "Id");
        }
    }
}
