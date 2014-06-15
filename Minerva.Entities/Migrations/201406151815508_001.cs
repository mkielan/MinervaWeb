namespace Minerva.Entities.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _001 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Email", c => c.String(maxLength: 50));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Email");
        }
    }
}
