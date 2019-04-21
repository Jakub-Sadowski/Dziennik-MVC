namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class addocHist : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot");
            CreateTable(
                "dbo.OcenaHistoria",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID, cascadeDelete: true)
                .ForeignKey("dbo.Ocena", t => t.OcenaID, cascadeDelete: true)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID, cascadeDelete: false)
                .ForeignKey("dbo.Uczen", t => t.UczenID, cascadeDelete: false)
                .Index(t => t.PrzedmiotID)
                .Index(t => t.NauczycielID)
                .Index(t => t.UczenID)
                .Index(t => t.OcenaID);
            
            AddForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.OcenaHistoria", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.OcenaHistoria", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.OcenaHistoria", "OcenaID", "dbo.Ocena");
            DropForeignKey("dbo.OcenaHistoria", "NauczycielID", "dbo.Nauczyciel");
            DropIndex("dbo.OcenaHistoria", new[] { "OcenaID" });
            DropIndex("dbo.OcenaHistoria", new[] { "UczenID" });
            DropIndex("dbo.OcenaHistoria", new[] { "NauczycielID" });
            DropIndex("dbo.OcenaHistoria", new[] { "PrzedmiotID" });
            DropTable("dbo.OcenaHistoria");
            AddForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot", "ID");
        }
    }
}
