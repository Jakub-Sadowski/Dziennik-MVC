namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ImieRodzica : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uczen", "ImieRodzica", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uczen", "ImieRodzica");
        }
    }
}
