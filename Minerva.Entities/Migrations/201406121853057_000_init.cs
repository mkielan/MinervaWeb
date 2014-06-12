namespace Minerva.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _000_init : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directories",
                c => new
                    {
                        DiskStructureId = c.Long(nullable: false),
                    })
                .PrimaryKey(t => t.DiskStructureId)
                .ForeignKey("dbo.DiskStructures", t => t.DiskStructureId)
                .Index(t => t.DiskStructureId);
            
            CreateTable(
                "dbo.DiskStructures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 400),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                        ApplicationUser_Id = c.String(maxLength: 128),
                        Source_Id = c.Long(),
                        Parent_Id = c.Long(),
                        CreatedBy_Id = c.String(maxLength: 128),
                        DeletedBy_Id = c.String(maxLength: 128),
                        ModifiedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id)
                .ForeignKey("dbo.DiskStructures", t => t.Parent_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.Source_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Phone = c.String(maxLength: 20),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Source_Id = c.Long(),
                        Source_Id1 = c.Long(),
                        DiskStructure_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id1)
                .ForeignKey("dbo.DiskStructures", t => t.DiskStructure_Id)
                .Index(t => t.Source_Id)
                .Index(t => t.Source_Id1)
                .Index(t => t.DiskStructure_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id, cascadeDelete: true)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.LoginProvider, t.ProviderKey })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                        CreatedBy_Id = c.String(maxLength: 128),
                        DeletedBy_Id = c.String(maxLength: 128),
                        ModifiedBy_Id = c.String(maxLength: 128),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Body = c.String(maxLength: 500),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                        CreatedBy_Id = c.String(maxLength: 128),
                        DeletedBy_Id = c.String(maxLength: 128),
                        ModifiedBy_Id = c.String(maxLength: 128),
                        DiskStructure_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.DiskStructures", t => t.DiskStructure_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.DiskStructure_Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        DiskStructureId = c.Long(nullable: false),
                        Extension = c.String(nullable: false, maxLength: 5),
                    })
                .PrimaryKey(t => t.DiskStructureId)
                .ForeignKey("dbo.DiskStructures", t => t.DiskStructureId)
                .Index(t => t.DiskStructureId);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Directories", "DiskStructureId", "dbo.DiskStructures");
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
            DropForeignKey("dbo.DiskStructures", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Files", "DiskStructureId", "dbo.DiskStructures");
            DropForeignKey("dbo.DiskStructures", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DiskStructures", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "DiskStructure_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.Comments", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DiskStructures", "Parent_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.AspNetUsers", "DiskStructure_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.Sources", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Source_Id1", "dbo.Sources");
            DropForeignKey("dbo.AspNetUsers", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.Sources", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DiskStructures", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.Sources", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sources", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.DiskStructures", "ApplicationUser_Id", "dbo.AspNetUsers");
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
            DropIndex("dbo.Files", new[] { "DiskStructureId" });
            DropIndex("dbo.Comments", new[] { "DiskStructure_Id" });
            DropIndex("dbo.Comments", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Comments", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Comments", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Sources", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Sources", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Sources", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Sources", new[] { "CreatedBy_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "DiskStructure_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Source_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "Source_Id" });
            DropIndex("dbo.DiskStructures", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.DiskStructures", new[] { "DeletedBy_Id" });
            DropIndex("dbo.DiskStructures", new[] { "CreatedBy_Id" });
            DropIndex("dbo.DiskStructures", new[] { "Parent_Id" });
            DropIndex("dbo.DiskStructures", new[] { "Source_Id" });
            DropIndex("dbo.DiskStructures", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Directories", new[] { "DiskStructureId" });
            DropTable("dbo.TagDiskStructures");
            DropTable("dbo.ResourceDiskStructures");
            DropTable("dbo.Tags");
            DropTable("dbo.Resources");
            DropTable("dbo.Files");
            DropTable("dbo.Comments");
            DropTable("dbo.Sources");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.DiskStructures");
            DropTable("dbo.Directories");
        }
    }
}
