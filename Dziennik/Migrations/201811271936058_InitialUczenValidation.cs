namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialUczenValidation : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Uczen", "imie", c => c.String(nullable: false));
            AlterColumn("dbo.Uczen", "nazwisko", c => c.String(nullable: false));
            AlterColumn("dbo.Uczen", "login", c => c.String(nullable: false));
            AlterColumn("dbo.Uczen", "haslo", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Uczen", "haslo", c => c.String());
            AlterColumn("dbo.Uczen", "login", c => c.String());
            AlterColumn("dbo.Uczen", "nazwisko", c => c.String());
            AlterColumn("dbo.Uczen", "imie", c => c.String());
        }
    }
}
