namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class asd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Nieobecnosc", "Status", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Nieobecnosc", "Status");
        }
    }
}
