namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class removeocHist : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.OcenaHistoria", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.OcenaHistoria", "OcenaID", "dbo.Ocena");
            DropForeignKey("dbo.OcenaHistoria", "ID", "dbo.Przedmiot");
            DropForeignKey("dbo.OcenaHistoria", "ID", "dbo.Uczen");
            DropForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot");
            DropIndex("dbo.OcenaHistoria", new[] { "ID" });
            DropIndex("dbo.OcenaHistoria", new[] { "NauczycielID" });
            DropIndex("dbo.OcenaHistoria", new[] { "OcenaID" });
            AddForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot", "ID");
            DropTable("dbo.OcenaHistoria");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OcenaHistoria",
                c => new
                    {
                        ID = c.Int(nullable: false),
                        ocena = c.Double(nullable: false),
                        waga = c.Int(nullable: false),
                        data = c.DateTime(nullable: false),
                        tresc = c.String(),
                        PrzedmiotID = c.Int(nullable: false),
                        NauczycielID = c.Int(nullable: false),
                        UczenID = c.Int(nullable: false),
                        OcenaID = c.Int(nullable: false),
                        IdEdytujacego = c.Int(),
                        dataEdycji = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot");
            CreateIndex("dbo.OcenaHistoria", "OcenaID");
            CreateIndex("dbo.OcenaHistoria", "NauczycielID");
            CreateIndex("dbo.OcenaHistoria", "ID");
            AddForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot", "ID", cascadeDelete: true);
            AddForeignKey("dbo.OcenaHistoria", "ID", "dbo.Uczen", "ID");
            AddForeignKey("dbo.OcenaHistoria", "ID", "dbo.Przedmiot", "ID");
            AddForeignKey("dbo.OcenaHistoria", "OcenaID", "dbo.Ocena", "ID", cascadeDelete: true);
            AddForeignKey("dbo.OcenaHistoria", "NauczycielID", "dbo.Nauczyciel", "NauczycielID", cascadeDelete: true);
        }
    }
}
