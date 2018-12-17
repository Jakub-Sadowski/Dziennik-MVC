namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WynikTestuUcznia : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Testy_ucznia", "Wynik", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Testy_ucznia", "Wynik");
        }
    }
}
