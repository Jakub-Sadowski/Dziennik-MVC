namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class donosyv2DI201870 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nauczyciel", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Nauczyciel", "Email");
        }
    }
}
