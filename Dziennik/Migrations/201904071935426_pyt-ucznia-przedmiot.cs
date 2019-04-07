namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pytuczniaprzedmiot : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pytanie_ucznia", "PrzedmiotID", c => c.Int());
            CreateIndex("dbo.Pytanie_ucznia", "PrzedmiotID");
            AddForeignKey("dbo.Pytanie_ucznia", "PrzedmiotID", "dbo.Przedmiot", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pytanie_ucznia", "PrzedmiotID", "dbo.Przedmiot");
            DropIndex("dbo.Pytanie_ucznia", new[] { "PrzedmiotID" });
            DropColumn("dbo.Pytanie_ucznia", "PrzedmiotID");
        }
    }
}
