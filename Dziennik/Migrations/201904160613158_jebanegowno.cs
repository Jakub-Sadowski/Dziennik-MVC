namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class jebanegowno : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Donos",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NauczycielID = c.Int(),
                        UczenID = c.Int(),
                        wiadomosc = c.String(),
                        data_pytania = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Uczen", t => t.UczenID)
                .Index(t => t.NauczycielID)
                .Index(t => t.UczenID);
            
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
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID, cascadeDelete: true)
                .ForeignKey("dbo.Ocena", t => t.OcenaID, cascadeDelete: true)
                .ForeignKey("dbo.Przedmiot", t => t.ID)
                .ForeignKey("dbo.Uczen", t => t.ID)
                .Index(t => t.ID)
                .Index(t => t.NauczycielID)
                .Index(t => t.OcenaID);
            
            CreateTable(
                "dbo.Pytanie_ucznia",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NauczycielID = c.Int(),
                        UczenID = c.Int(),
                        PrzedmiotID = c.Int(),
                        Pytanie = c.String(),
                        Odpowiedz = c.String(),
                        Data_pytania = c.DateTime(nullable: false),
                        Data_odpowiedzi = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID)
                .ForeignKey("dbo.Uczen", t => t.UczenID)
                .Index(t => t.NauczycielID)
                .Index(t => t.UczenID)
                .Index(t => t.PrzedmiotID);
            
            AddColumn("dbo.Nauczyciel", "Email", c => c.String());
            DropColumn("dbo.Ocena", "IdEdytujacego");
            DropColumn("dbo.Ocena", "dataEdycji");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Ocena", "dataEdycji", c => c.DateTime());
            AddColumn("dbo.Ocena", "IdEdytujacego", c => c.Int());
            DropForeignKey("dbo.Pytanie_ucznia", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Pytanie_ucznia", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Pytanie_ucznia", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.OcenaHistoria", "ID", "dbo.Uczen");
            DropForeignKey("dbo.OcenaHistoria", "ID", "dbo.Przedmiot");
            DropForeignKey("dbo.OcenaHistoria", "OcenaID", "dbo.Ocena");
            DropForeignKey("dbo.OcenaHistoria", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Donos", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Donos", "NauczycielID", "dbo.Nauczyciel");
            DropIndex("dbo.Pytanie_ucznia", new[] { "PrzedmiotID" });
            DropIndex("dbo.Pytanie_ucznia", new[] { "UczenID" });
            DropIndex("dbo.Pytanie_ucznia", new[] { "NauczycielID" });
            DropIndex("dbo.OcenaHistoria", new[] { "OcenaID" });
            DropIndex("dbo.OcenaHistoria", new[] { "NauczycielID" });
            DropIndex("dbo.OcenaHistoria", new[] { "ID" });
            DropIndex("dbo.Donos", new[] { "UczenID" });
            DropIndex("dbo.Donos", new[] { "NauczycielID" });
            DropColumn("dbo.Nauczyciel", "Email");
            DropTable("dbo.Pytanie_ucznia");
            DropTable("dbo.OcenaHistoria");
            DropTable("dbo.Donos");
        }
    }
}
