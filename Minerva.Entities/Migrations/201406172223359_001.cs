namespace Minerva.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Files", "Extension", c => c.String(maxLength: 5));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Files", "Extension", c => c.String(nullable: false, maxLength: 5));
        }
    }
}
