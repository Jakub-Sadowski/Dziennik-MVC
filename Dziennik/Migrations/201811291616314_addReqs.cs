namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addReqs : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Tresc_ksztalcenia", "plikSciezka", c => c.String(nullable: false));
            AlterColumn("dbo.Przedmiot", "nazwa", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Przedmiot", "nazwa", c => c.String());
            DropColumn("dbo.Tresc_ksztalcenia", "plikSciezka");
        }
    }
}
