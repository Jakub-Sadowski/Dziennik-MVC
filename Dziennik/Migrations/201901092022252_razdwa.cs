namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class razdwa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ogloszenie_dla_rodzicow", "klasa_KlasaID", c => c.Int());
            CreateIndex("dbo.Ogloszenie_dla_rodzicow", "klasa_KlasaID");
            AddForeignKey("dbo.Ogloszenie_dla_rodzicow", "klasa_KlasaID", "dbo.Klasa", "KlasaID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ogloszenie_dla_rodzicow", "klasa_KlasaID", "dbo.Klasa");
            DropIndex("dbo.Ogloszenie_dla_rodzicow", new[] { "klasa_KlasaID" });
            DropColumn("dbo.Ogloszenie_dla_rodzicow", "klasa_KlasaID");
        }
    }
}
