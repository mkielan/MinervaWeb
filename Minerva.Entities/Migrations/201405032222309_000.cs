namespace Minerva.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _000 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Directories",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                        Source_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiskStructures", t => t.Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id)
                .Index(t => t.Id)
                .Index(t => t.Source_Id);
            
            CreateTable(
                "dbo.DiskStructures",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Path = c.String(nullable: false, maxLength: 500),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                        Parent_Id = c.Long(),
                        Directory_Id = c.Long(),
                        File_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiskStructures", t => t.Parent_Id)
                .ForeignKey("dbo.Directories", t => t.Directory_Id)
                .ForeignKey("dbo.Files", t => t.File_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.Directory_Id)
                .Index(t => t.File_Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 200),
                        Extension = c.String(nullable: false, maxLength: 200),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiskStructures", t => t.Id)
                .Index(t => t.Id);
            
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
                        Source_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id)
                .Index(t => t.Source_Id);
            
            CreateTable(
                "dbo.Sources",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 50),
                        Description = c.String(maxLength: 200),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                        Owner_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id, cascadeDelete: true)
                .Index(t => t.Owner_Id);
            
            CreateTable(
                "dbo.InternalSources",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                        Source_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id)
                .Index(t => t.Source_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        UserName = c.String(),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Source_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id)
                .Index(t => t.Source_Id);
            
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FtpSources", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.AspNetUsers", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.Sources", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.InternalSources", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.Directories", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.Directories", "Id", "dbo.DiskStructures");
            DropForeignKey("dbo.DiskStructures", "File_Id", "dbo.Files");
            DropForeignKey("dbo.Files", "Id", "dbo.DiskStructures");
            DropForeignKey("dbo.DiskStructures", "Directory_Id", "dbo.Directories");
            DropForeignKey("dbo.DiskStructures", "Parent_Id", "dbo.DiskStructures");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Source_Id" });
            DropIndex("dbo.InternalSources", new[] { "Source_Id" });
            DropIndex("dbo.Sources", new[] { "Owner_Id" });
            DropIndex("dbo.FtpSources", new[] { "Source_Id" });
            DropIndex("dbo.Files", new[] { "Id" });
            DropIndex("dbo.DiskStructures", new[] { "File_Id" });
            DropIndex("dbo.DiskStructures", new[] { "Directory_Id" });
            DropIndex("dbo.DiskStructures", new[] { "Parent_Id" });
            DropIndex("dbo.Directories", new[] { "Source_Id" });
            DropIndex("dbo.Directories", new[] { "Id" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.InternalSources");
            DropTable("dbo.Sources");
            DropTable("dbo.FtpSources");
            DropTable("dbo.Files");
            DropTable("dbo.DiskStructures");
            DropTable("dbo.Directories");
        }
    }
}
