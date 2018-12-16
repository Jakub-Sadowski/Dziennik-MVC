namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Test", "czas_trwania");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Test", "czas_trwania", c => c.DateTime(nullable: false));
        }
    }
}
