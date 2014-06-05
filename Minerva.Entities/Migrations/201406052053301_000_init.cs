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
                .ForeignKey("dbo.DiskStructures", t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .Index(t => t.Id)
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
                        Discriminator = c.String(nullable: false, maxLength: 128),
                        Source_Id = c.Long(),
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
                        CreatedBy_Id = c.String(maxLength: 128),
                        DeletedBy_Id = c.String(maxLength: 128),
                        Directory_Id = c.Long(),
                        File_Id = c.Long(),
                        ModifiedBy_Id = c.String(maxLength: 128),
                        Source_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.DiskStructures", t => t.Parent_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.Directories", t => t.Directory_Id)
                .ForeignKey("dbo.Files", t => t.File_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id)
                .Index(t => t.Parent_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.Directory_Id)
                .Index(t => t.File_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Source_Id);
            
            CreateTable(
                "dbo.Comments",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Body = c.String(maxLength: 500),
                        Time = c.DateTime(nullable: false),
                        CreatedTime = c.DateTime(nullable: false),
                        ModificationTime = c.DateTime(),
                        DeletedTime = c.DateTime(),
                        Author_Id = c.String(maxLength: 128),
                        CreatedBy_Id = c.String(maxLength: 128),
                        DeletedBy_Id = c.String(maxLength: 128),
                        ModifiedBy_Id = c.String(maxLength: 128),
                        DiskStructure_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.DiskStructures", t => t.DiskStructure_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.DiskStructure_Id);
            
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        Extension = c.String(nullable: false, maxLength: 200),
                        MimeType = c.String(),
                        Content = c.String(),
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
                .ForeignKey("dbo.DiskStructures", t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .Index(t => t.Id)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.ModifiedBy_Id);
            
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
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.Sources", t => t.Source_Id, cascadeDelete: true)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Source_Id);
            
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
                        FtpSource_Id = c.Long(),
                        ModifiedBy_Id = c.String(maxLength: 128),
                        Owner_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.CreatedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.DeletedBy_Id)
                .ForeignKey("dbo.FtpSources", t => t.FtpSource_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ModifiedBy_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Owner_Id, cascadeDelete: true)
                .Index(t => t.CreatedBy_Id)
                .Index(t => t.DeletedBy_Id)
                .Index(t => t.FtpSource_Id)
                .Index(t => t.ModifiedBy_Id)
                .Index(t => t.Owner_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.FtpSources", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.AspNetUsers", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.Sources", "Owner_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sources", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sources", "FtpSource_Id", "dbo.FtpSources");
            DropForeignKey("dbo.DiskStructures", "Source_Id", "dbo.Sources");
            DropForeignKey("dbo.Sources", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Sources", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FtpSources", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FtpSources", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.FtpSources", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Directories", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Directories", "Id", "dbo.DiskStructures");
            DropForeignKey("dbo.DiskStructures", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DiskStructures", "File_Id", "dbo.Files");
            DropForeignKey("dbo.Files", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Files", "Id", "dbo.DiskStructures");
            DropForeignKey("dbo.Files", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Files", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DiskStructures", "Directory_Id", "dbo.Directories");
            DropForeignKey("dbo.DiskStructures", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DiskStructures", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "DiskStructure_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.Comments", "ModifiedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.DiskStructures", "Parent_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.Directories", "DeletedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Directories", "CreatedBy_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropIndex("dbo.Sources", new[] { "Owner_Id" });
            DropIndex("dbo.Sources", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Sources", new[] { "FtpSource_Id" });
            DropIndex("dbo.Sources", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Sources", new[] { "CreatedBy_Id" });
            DropIndex("dbo.FtpSources", new[] { "Source_Id" });
            DropIndex("dbo.FtpSources", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.FtpSources", new[] { "DeletedBy_Id" });
            DropIndex("dbo.FtpSources", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Files", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Files", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Files", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Files", new[] { "Id" });
            DropIndex("dbo.Comments", new[] { "DiskStructure_Id" });
            DropIndex("dbo.Comments", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Comments", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Comments", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropIndex("dbo.DiskStructures", new[] { "Source_Id" });
            DropIndex("dbo.DiskStructures", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.DiskStructures", new[] { "File_Id" });
            DropIndex("dbo.DiskStructures", new[] { "Directory_Id" });
            DropIndex("dbo.DiskStructures", new[] { "DeletedBy_Id" });
            DropIndex("dbo.DiskStructures", new[] { "CreatedBy_Id" });
            DropIndex("dbo.DiskStructures", new[] { "Parent_Id" });
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "User_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Source_Id" });
            DropIndex("dbo.Directories", new[] { "ModifiedBy_Id" });
            DropIndex("dbo.Directories", new[] { "DeletedBy_Id" });
            DropIndex("dbo.Directories", new[] { "CreatedBy_Id" });
            DropIndex("dbo.Directories", new[] { "Id" });
            DropTable("dbo.Sources");
            DropTable("dbo.FtpSources");
            DropTable("dbo.Files");
            DropTable("dbo.Comments");
            DropTable("dbo.DiskStructures");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Directories");
        }
    }
}
