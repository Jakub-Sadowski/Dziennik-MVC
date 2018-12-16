namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class abc : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrator",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        imie = c.String(nullable: false),
                        nazwisko = c.String(nullable: false),
                        login = c.String(nullable: false),
                        haslo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Klasa",
                c => new
                    {
                        KlasaID = c.Int(nullable: false, identity: true),
                        nazwa = c.String(),
                        level = c.Int(),
                        WychowawcaID = c.Int(),
                    })
                .PrimaryKey(t => t.KlasaID)
                .ForeignKey("dbo.Nauczyciel", t => t.WychowawcaID)
                .Index(t => t.WychowawcaID);
            
            CreateTable(
                "dbo.Lekcja",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NauczycielID = c.Int(),
                        KlasaID = c.Int(),
                        PrzedmiotID = c.Int(),
                        godzina = c.Int(nullable: false),
                        dzien = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klasa", t => t.KlasaID, cascadeDelete: true)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID, cascadeDelete: true)
                .Index(t => t.NauczycielID)
                .Index(t => t.KlasaID)
                .Index(t => t.PrzedmiotID);
            
            CreateTable(
                "dbo.Nauczyciel",
                c => new
                    {
                        NauczycielID = c.Int(nullable: false, identity: true),
                        imie = c.String(),
                        nazwisko = c.String(),
                        login = c.String(),
                        haslo = c.String(),
                    })
                .PrimaryKey(t => t.NauczycielID);
            
            CreateTable(
                "dbo.Ocena",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        ocena = c.Int(nullable: false),
                        waga = c.Int(nullable: false),
                        data = c.DateTime(nullable: false),
                        tresc = c.String(),
                        PrzedmiotID = c.Int(),
                        NauczycielID = c.Int(),
                        UczenID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID, cascadeDelete: true)
                .ForeignKey("dbo.Uczen", t => t.UczenID, cascadeDelete: true)
                .Index(t => t.PrzedmiotID)
                .Index(t => t.NauczycielID)
                .Index(t => t.UczenID);
            
            CreateTable(
                "dbo.Przedmiot",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        nazwa = c.String(nullable: false),
                        level = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Plik",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        data = c.DateTime(nullable: false),
                        PrzedmiotID = c.Int(),
                        KlasaID = c.Int(),
                        NauczycielID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klasa", t => t.KlasaID, cascadeDelete: true)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID, cascadeDelete: true)
                .Index(t => t.PrzedmiotID)
                .Index(t => t.KlasaID)
                .Index(t => t.NauczycielID);
            
            CreateTable(
                "dbo.Test",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PrzedmiotID = c.Int(),
                        KlasaID = c.Int(),
                        NauczycielID = c.Int(),
                        czas_trwania = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klasa", t => t.KlasaID, cascadeDelete: true)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID, cascadeDelete: true)
                .Index(t => t.PrzedmiotID)
                .Index(t => t.KlasaID)
                .Index(t => t.NauczycielID);
            
            CreateTable(
                "dbo.Pytanie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TestID = c.Int(),
                        tresc = c.String(),
                        odpowiedz1 = c.String(),
                        odpowiedz2 = c.String(),
                        odpowiedz3 = c.String(),
                        odpowiedz4 = c.String(),
                        punktacja = c.Int(nullable: false),
                        odp = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Test", t => t.TestID, cascadeDelete: true)
                .Index(t => t.TestID);
            
            CreateTable(
                "dbo.Testy_ucznia",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UczenID = c.Int(),
                        TestID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Test", t => t.TestID)
                .ForeignKey("dbo.Uczen", t => t.UczenID, cascadeDelete: true)
                .Index(t => t.UczenID)
                .Index(t => t.TestID);
            
            CreateTable(
                "dbo.Uczen",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        imie = c.String(nullable: false),
                        nazwisko = c.String(nullable: false),
                        login = c.String(nullable: false),
                        haslo = c.String(nullable: false),
                        KlasaID = c.Int(),
                        RodzicID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klasa", t => t.KlasaID, cascadeDelete: true)
                .ForeignKey("dbo.Rodzic", t => t.RodzicID, cascadeDelete: true)
                .Index(t => t.KlasaID)
                .Index(t => t.RodzicID);
            
            CreateTable(
                "dbo.Nieobecnosc",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UczenID = c.Int(),
                        LekcjaID = c.Int(),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lekcja", t => t.LekcjaID)
                .ForeignKey("dbo.Uczen", t => t.UczenID, cascadeDelete: true)
                .Index(t => t.UczenID)
                .Index(t => t.LekcjaID);
            
            CreateTable(
                "dbo.Rodzic",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        imie = c.String(nullable: false),
                        nazwisko = c.String(nullable: false),
                        login = c.String(nullable: false),
                        haslo = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Ogloszenie_dla_rodzicow",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NauczycielID = c.Int(),
                        RodzicID = c.Int(),
                        naglowek = c.String(),
                        tresc = c.String(),
                        data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Rodzic", t => t.RodzicID, cascadeDelete: true)
                .Index(t => t.NauczycielID)
                .Index(t => t.RodzicID);
            
            CreateTable(
                "dbo.Spoznienie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        UczenID = c.Int(),
                        LekcjaID = c.Int(),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lekcja", t => t.LekcjaID)
                .ForeignKey("dbo.Uczen", t => t.UczenID, cascadeDelete: true)
                .Index(t => t.UczenID)
                .Index(t => t.LekcjaID);
            
            CreateTable(
                "dbo.Uwaga",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NauczycielID = c.Int(),
                        UczenID = c.Int(),
                        naglowek = c.String(),
                        tresc = c.String(),
                        date = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Uczen", t => t.UczenID, cascadeDelete: true)
                .Index(t => t.NauczycielID)
                .Index(t => t.UczenID);
            
            CreateTable(
                "dbo.Tresc_ksztalcenia",
                c => new
                    {
                        PrzedmiotID = c.Int(nullable: false),
                        plikSciezka = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.PrzedmiotID)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID)
                .Index(t => t.PrzedmiotID);
            
            CreateTable(
                "dbo.Ogloszenie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NauczycielID = c.Int(),
                        naglowek = c.String(),
                        tresc = c.String(),
                        data = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .Index(t => t.NauczycielID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Klasa", "WychowawcaID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Lekcja", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Ogloszenie", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Ocena", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Ocena", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Tresc_ksztalcenia", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Testy_ucznia", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Uwaga", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Uwaga", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Spoznienie", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Spoznienie", "LekcjaID", "dbo.Lekcja");
            DropForeignKey("dbo.Uczen", "RodzicID", "dbo.Rodzic");
            DropForeignKey("dbo.Ogloszenie_dla_rodzicow", "RodzicID", "dbo.Rodzic");
            DropForeignKey("dbo.Ogloszenie_dla_rodzicow", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Nieobecnosc", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Nieobecnosc", "LekcjaID", "dbo.Lekcja");
            DropForeignKey("dbo.Uczen", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Testy_ucznia", "TestID", "dbo.Test");
            DropForeignKey("dbo.Pytanie", "TestID", "dbo.Test");
            DropForeignKey("dbo.Test", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Test", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Test", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Plik", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Plik", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Plik", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Ocena", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Lekcja", "KlasaID", "dbo.Klasa");
            DropIndex("dbo.Ogloszenie", new[] { "NauczycielID" });
            DropIndex("dbo.Tresc_ksztalcenia", new[] { "PrzedmiotID" });
            DropIndex("dbo.Uwaga", new[] { "UczenID" });
            DropIndex("dbo.Uwaga", new[] { "NauczycielID" });
            DropIndex("dbo.Spoznienie", new[] { "LekcjaID" });
            DropIndex("dbo.Spoznienie", new[] { "UczenID" });
            DropIndex("dbo.Ogloszenie_dla_rodzicow", new[] { "RodzicID" });
            DropIndex("dbo.Ogloszenie_dla_rodzicow", new[] { "NauczycielID" });
            DropIndex("dbo.Nieobecnosc", new[] { "LekcjaID" });
            DropIndex("dbo.Nieobecnosc", new[] { "UczenID" });
            DropIndex("dbo.Uczen", new[] { "RodzicID" });
            DropIndex("dbo.Uczen", new[] { "KlasaID" });
            DropIndex("dbo.Testy_ucznia", new[] { "TestID" });
            DropIndex("dbo.Testy_ucznia", new[] { "UczenID" });
            DropIndex("dbo.Pytanie", new[] { "TestID" });
            DropIndex("dbo.Test", new[] { "NauczycielID" });
            DropIndex("dbo.Test", new[] { "KlasaID" });
            DropIndex("dbo.Test", new[] { "PrzedmiotID" });
            DropIndex("dbo.Plik", new[] { "NauczycielID" });
            DropIndex("dbo.Plik", new[] { "KlasaID" });
            DropIndex("dbo.Plik", new[] { "PrzedmiotID" });
            DropIndex("dbo.Ocena", new[] { "UczenID" });
            DropIndex("dbo.Ocena", new[] { "NauczycielID" });
            DropIndex("dbo.Ocena", new[] { "PrzedmiotID" });
            DropIndex("dbo.Lekcja", new[] { "PrzedmiotID" });
            DropIndex("dbo.Lekcja", new[] { "KlasaID" });
            DropIndex("dbo.Lekcja", new[] { "NauczycielID" });
            DropIndex("dbo.Klasa", new[] { "WychowawcaID" });
            DropTable("dbo.Ogloszenie");
            DropTable("dbo.Tresc_ksztalcenia");
            DropTable("dbo.Uwaga");
            DropTable("dbo.Spoznienie");
            DropTable("dbo.Ogloszenie_dla_rodzicow");
            DropTable("dbo.Rodzic");
            DropTable("dbo.Nieobecnosc");
            DropTable("dbo.Uczen");
            DropTable("dbo.Testy_ucznia");
            DropTable("dbo.Pytanie");
            DropTable("dbo.Test");
            DropTable("dbo.Plik");
            DropTable("dbo.Przedmiot");
            DropTable("dbo.Ocena");
            DropTable("dbo.Nauczyciel");
            DropTable("dbo.Lekcja");
            DropTable("dbo.Klasa");
            DropTable("dbo.Administrator");
        }
    }
}
