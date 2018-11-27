namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class _1234 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Administrator", "imie", c => c.String(nullable: false));
            AlterColumn("dbo.Administrator", "nazwisko", c => c.String(nullable: false));
            AlterColumn("dbo.Administrator", "login", c => c.String(nullable: false));
            AlterColumn("dbo.Administrator", "haslo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Administrator", "haslo", c => c.String());
            AlterColumn("dbo.Administrator", "login", c => c.String());
            AlterColumn("dbo.Administrator", "nazwisko", c => c.String());
            AlterColumn("dbo.Administrator", "imie", c => c.String());
        }
    }
}
