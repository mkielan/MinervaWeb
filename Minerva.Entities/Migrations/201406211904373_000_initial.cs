namespace Minerva.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _000_initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.DiskStructures",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Description = c.String(maxLength: 400),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                        Source_Id = c.Int(),
                        CreatedBy_Id = c.String(maxLength: 128),
                        DeletedBy_Id = c.String(maxLength: 128),
                        ModifiedBy_Id = c.String(maxLength: 128),
                        Parent_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.DiskStructures", t => t.Parent_Id)
                .Index(t => t.Source_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Parent_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Phone = c.String(maxLength: 20),
                        Email = c.String(maxLength: 50),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Source_Id = c.Int(),
                        Source_Id1 = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id1)
                .Index(t => t.Source_Id)
                .Index(t => t.Source_Id1);
            
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
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        ApplicationUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id)
                .Index(t => t.ApplicationUser_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Body = c.String(maxLength: 500),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                        CreatedBy_Id = c.String(maxLength: 128),
                        DeletedBy_Id = c.String(maxLength: 128),
                        DiskStructure_Id = c.Int(),
                        ModifiedBy_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.DiskStructures", t => t.DiskStructure_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.DiskStructure_Id)
                .Index(t => t.ModifiedBy_Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        DiskStructureId = c.Int(nullable: false),
                        Extension = c.String(maxLength: 5),
                    })
                .PrimaryKey(t => t.DiskStructureId)
                .ForeignKey("dbo.DiskStructures", t => t.DiskStructureId)
                .Index(t => t.DiskStructureId);
            
            CreateTable(
                "dbo.Resources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
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
            
            CreateTable(
                "dbo.ResourceDiskStructures",
                c => new
                    {
                        Resource_Id = c.Int(nullable: false),
                        DiskStructure_Id = c.Int(nullable: false),
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
                        Tag_Id = c.Int(nullable: false),
                        DiskStructure_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Tag_Id, t.DiskStructure_Id })
                .ForeignKey("dbo.Tags", t => t.Tag_Id, cascadeDelete: true)
                .ForeignKey("dbo.DiskStructures", t => t.DiskStructure_Id, cascadeDelete: true)
                .Index(t => t.Tag_Id)
                .Index(t => t.DiskStructure_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TagDiskStructures", "DiskStructure_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.TagDiskStructures", "Tag_Id", "dbo.Tags");
            DropForeignKey("dbo.Tags", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Tags", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ResourceDiskStructures", "DiskStructure_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.ResourceDiskStructures", "Resource_Id", "dbo.Resources");
            DropForeignKey("dbo.DiskStructures", "Parent_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.DiskStructures", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Files", "DiskStructureId", "dbo.DiskStructures");
            DropForeignKey("dbo.DiskStructures", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DiskStructures", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "DiskStructure_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.Comments", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sources", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "Source_Id1", "dbo.Sources");
            DropForeignKey("dbo.AspNetUsers", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.DiskStructures", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.ApplicationUserDiskStructures", "DiskStructure_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.ApplicationUserDiskStructures", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.TagDiskStructures", new[] { "DiskStructure_Id" });
            DropIndex("dbo.TagDiskStructures", new[] { "Tag_Id" });
            DropIndex("dbo.ResourceDiskStructures", new[] { "DiskStructure_Id" });
            DropIndex("dbo.ResourceDiskStructures", new[] { "Resource_Id" });
            DropIndex("dbo.ApplicationUserDiskStructures", new[] { "DiskStructure_Id" });
            DropIndex("dbo.ApplicationUserDiskStructures", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.Tags", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Tags", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Tags", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Files", new[] { "DiskStructureId" });
            DropIndex("dbo.Comments", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Comments", new[] { "DiskStructure_Id" });
            DropIndex("dbo.Comments", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Comments", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Sources", new[] { "ApplicationUser_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Source_Id1" });
            DropIndex("dbo.AspNetUsers", new[] { "Source_Id" });
            DropIndex("dbo.DiskStructures", new[] { "Parent_Id" });
            DropIndex("dbo.DiskStructures", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.DiskStructures", new[] { "DeletedBy_Id" });
            DropIndex("dbo.DiskStructures", new[] { "CreatedBy_Id" });
            DropIndex("dbo.DiskStructures", new[] { "Source_Id" });
            DropTable("dbo.TagDiskStructures");
            DropTable("dbo.ResourceDiskStructures");
            DropTable("dbo.ApplicationUserDiskStructures");
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
        }
    }
}
