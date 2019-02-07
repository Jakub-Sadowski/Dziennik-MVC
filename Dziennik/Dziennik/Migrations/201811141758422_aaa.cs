namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;

    public partial class aaa : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Administrator",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    imie = c.String(),
                    nazwisko = c.String(),
                    login = c.String(),
                    haslo = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Klasa",
                c => new
                {
                    KlasaID = c.Int(nullable: false, identity: true),
                    NauczycielID = c.Int(),
                    nazwa = c.String(),
                    level = c.Int(),
                })
                .PrimaryKey(t => t.KlasaID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .Index(t => t.NauczycielID);

            CreateTable(
                "dbo.Lekcja",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    PrzedmiotID = c.Int(),
                    KlasaID = c.Int(),
                    date = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klasa", t => t.KlasaID)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID)
                .Index(t => t.PrzedmiotID)
                .Index(t => t.KlasaID);

            CreateTable(
                "dbo.Nieobecnosc",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    UczenID = c.Int(),
                    LekcjaID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lekcja", t => t.LekcjaID)
                .ForeignKey("dbo.Uczen", t => t.UczenID)
                .Index(t => t.UczenID)
                .Index(t => t.LekcjaID);

            CreateTable(
                "dbo.Uczen",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    imie = c.String(),
                    nazwisko = c.String(),
                    login = c.String(),
                    haslo = c.String(),
                    KlasaID = c.Int(),
                    RodzicID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klasa", t => t.KlasaID)
                .ForeignKey("dbo.Rodzic", t => t.RodzicID)
                .Index(t => t.KlasaID)
                .Index(t => t.RodzicID);

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
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Uczen", t => t.UczenID)
                .Index(t => t.PrzedmiotID)
                .Index(t => t.NauczycielID)
                .Index(t => t.UczenID);

            CreateTable(
                "dbo.Nauczyciel",
                c => new
                {
                    NauczycielID = c.Int(nullable: false, identity: true),
                    imie = c.String(),
                    nazwisko = c.String(),
                    login = c.String(),
                    haslo = c.String(),
                    KlasaID = c.Int(),
                })
                .PrimaryKey(t => t.NauczycielID)
                .ForeignKey("dbo.Klasa", t => t.KlasaID)
                .Index(t => t.KlasaID);

            CreateTable(
                "dbo.PrzedmiotKlasaNauczyciel",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    PrzedmiotID = c.Int(nullable: false),
                    KlasaID = c.Int(nullable: false),
                    NauczycielID = c.Int(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Klasa", t => t.KlasaID, cascadeDelete: true)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID, cascadeDelete: true)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID, cascadeDelete: true)
                .Index(t => t.PrzedmiotID)
                .Index(t => t.KlasaID)
                .Index(t => t.NauczycielID);

            CreateTable(
                "dbo.Przedmiot",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    nazwa = c.String(),
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
                .ForeignKey("dbo.Klasa", t => t.KlasaID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID)
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
                .ForeignKey("dbo.Klasa", t => t.KlasaID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Przedmiot", t => t.PrzedmiotID)
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
                .ForeignKey("dbo.Test", t => t.TestID)
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
                .ForeignKey("dbo.Uczen", t => t.UczenID)
                .Index(t => t.UczenID)
                .Index(t => t.TestID);

            CreateTable(
                "dbo.Tresc_ksztalcenia",
                c => new
                {
                    PrzedmiotID = c.Int(nullable: false),
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
                .ForeignKey("dbo.Rodzic", t => t.RodzicID)
                .Index(t => t.NauczycielID)
                .Index(t => t.RodzicID);

            CreateTable(
                "dbo.Rodzic",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    imie = c.String(),
                    nazwisko = c.String(),
                    login = c.String(),
                    haslo = c.String(),
                })
                .PrimaryKey(t => t.ID);

            CreateTable(
                "dbo.Uwaga",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    NauczycielID = c.Int(nullable: false),
                    UczenID = c.Int(nullable: false),
                    naglowek = c.String(),
                    tresc = c.String(),
                    date = c.DateTime(nullable: false),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID, cascadeDelete: true)
                .ForeignKey("dbo.Uczen", t => t.UczenID, cascadeDelete: true)
                .Index(t => t.NauczycielID)
                .Index(t => t.UczenID);

            CreateTable(
                "dbo.Spoznienie",
                c => new
                {
                    ID = c.Int(nullable: false, identity: true),
                    UczenID = c.Int(),
                    LekcjaID = c.Int(),
                })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Lekcja", t => t.LekcjaID)
                .ForeignKey("dbo.Uczen", t => t.UczenID)
                .Index(t => t.UczenID)
                .Index(t => t.LekcjaID);

        }

        public override void Down()
        {
            DropForeignKey("dbo.Klasa", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Spoznienie", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Spoznienie", "LekcjaID", "dbo.Lekcja");
            DropForeignKey("dbo.Ocena", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Uwaga", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Uwaga", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Uczen", "RodzicID", "dbo.Rodzic");
            DropForeignKey("dbo.Ogloszenie_dla_rodzicow", "RodzicID", "dbo.Rodzic");
            DropForeignKey("dbo.Ogloszenie_dla_rodzicow", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Ogloszenie", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Ocena", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Tresc_ksztalcenia", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Testy_ucznia", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Testy_ucznia", "TestID", "dbo.Test");
            DropForeignKey("dbo.Pytanie", "TestID", "dbo.Test");
            DropForeignKey("dbo.Test", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Test", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Test", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Plik", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Plik", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Plik", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Ocena", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.PrzedmiotKlasaNauczyciel", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.PrzedmiotKlasaNauczyciel", "NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.PrzedmiotKlasaNauczyciel", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Nauczyciel", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Nieobecnosc", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Uczen", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Nieobecnosc", "LekcjaID", "dbo.Lekcja");
            DropForeignKey("dbo.Lekcja", "KlasaID", "dbo.Klasa");
            DropIndex("dbo.Spoznienie", new[] { "LekcjaID" });
            DropIndex("dbo.Spoznienie", new[] { "UczenID" });
            DropIndex("dbo.Uwaga", new[] { "UczenID" });
            DropIndex("dbo.Uwaga", new[] { "NauczycielID" });
            DropIndex("dbo.Ogloszenie_dla_rodzicow", new[] { "RodzicID" });
            DropIndex("dbo.Ogloszenie_dla_rodzicow", new[] { "NauczycielID" });
            DropIndex("dbo.Ogloszenie", new[] { "NauczycielID" });
            DropIndex("dbo.Tresc_ksztalcenia", new[] { "PrzedmiotID" });
            DropIndex("dbo.Testy_ucznia", new[] { "TestID" });
            DropIndex("dbo.Testy_ucznia", new[] { "UczenID" });
            DropIndex("dbo.Pytanie", new[] { "TestID" });
            DropIndex("dbo.Test", new[] { "NauczycielID" });
            DropIndex("dbo.Test", new[] { "KlasaID" });
            DropIndex("dbo.Test", new[] { "PrzedmiotID" });
            DropIndex("dbo.Plik", new[] { "NauczycielID" });
            DropIndex("dbo.Plik", new[] { "KlasaID" });
            DropIndex("dbo.Plik", new[] { "PrzedmiotID" });
            DropIndex("dbo.PrzedmiotKlasaNauczyciel", new[] { "NauczycielID" });
            DropIndex("dbo.PrzedmiotKlasaNauczyciel", new[] { "KlasaID" });
            DropIndex("dbo.PrzedmiotKlasaNauczyciel", new[] { "PrzedmiotID" });
            DropIndex("dbo.Nauczyciel", new[] { "KlasaID" });
            DropIndex("dbo.Ocena", new[] { "UczenID" });
            DropIndex("dbo.Ocena", new[] { "NauczycielID" });
            DropIndex("dbo.Ocena", new[] { "PrzedmiotID" });
            DropIndex("dbo.Uczen", new[] { "RodzicID" });
            DropIndex("dbo.Uczen", new[] { "KlasaID" });
            DropIndex("dbo.Nieobecnosc", new[] { "LekcjaID" });
            DropIndex("dbo.Nieobecnosc", new[] { "UczenID" });
            DropIndex("dbo.Lekcja", new[] { "KlasaID" });
            DropIndex("dbo.Lekcja", new[] { "PrzedmiotID" });
            DropIndex("dbo.Klasa", new[] { "NauczycielID" });
            DropTable("dbo.Spoznienie");
            DropTable("dbo.Uwaga");
            DropTable("dbo.Rodzic");
            DropTable("dbo.Ogloszenie_dla_rodzicow");
            DropTable("dbo.Ogloszenie");
            DropTable("dbo.Tresc_ksztalcenia");
            DropTable("dbo.Testy_ucznia");
            DropTable("dbo.Pytanie");
            DropTable("dbo.Test");
            DropTable("dbo.Plik");
            DropTable("dbo.Przedmiot");
            DropTable("dbo.PrzedmiotKlasaNauczyciel");
            DropTable("dbo.Nauczyciel");
            DropTable("dbo.Ocena");
            DropTable("dbo.Uczen");
            DropTable("dbo.Nieobecnosc");
            DropTable("dbo.Lekcja");
            DropTable("dbo.Klasa");
            DropTable("dbo.Administrator");
        }
    }
}