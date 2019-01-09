namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class rodzic : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Rodzic", "Email", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Rodzic", "Email");
        }
    }
}
