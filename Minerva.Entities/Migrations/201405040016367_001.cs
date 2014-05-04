namespace Minerva.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001 : DbMigration
    {
        public override void Up()
        {
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
                        DiskStructure_Id = c.Long(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .ForeignKey("dbo.DiskStructures", t => t.DiskStructure_Id)
                .Index(t => t.Author_Id)
                .Index(t => t.DiskStructure_Id);
            
            DropColumn("dbo.Directories", "Name");
            DropColumn("dbo.Files", "Name");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Files", "Name", c => c.String(nullable: false, maxLength: 200));
            AddColumn("dbo.Directories", "Name", c => c.String(nullable: false, maxLength: 200));
            DropForeignKey("dbo.Comments", "DiskStructure_Id", "dbo.DiskStructures");
            DropForeignKey("dbo.Comments", "Author_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Comments", new[] { "DiskStructure_Id" });
            DropIndex("dbo.Comments", new[] { "Author_Id" });
            DropTable("dbo.Comments");
        }
    }
}
