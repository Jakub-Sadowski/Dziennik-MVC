namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Nauczyciel", "Email");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Nauczyciel", "Email", c => c.String());
        }
    }
}
