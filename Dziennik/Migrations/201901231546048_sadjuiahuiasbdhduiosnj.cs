namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sadjuiahuiasbdhduiosnj : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Zapytanie", "data_odpowiedz", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Zapytanie", "data_odpowiedz", c => c.DateTime(nullable: false));
        }
    }
}
