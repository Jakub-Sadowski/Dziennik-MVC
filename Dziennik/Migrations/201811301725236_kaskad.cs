namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kaskad : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Lekcja", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Plik", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Test", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Uczen", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Ocena", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Ocena", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Plik", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Test", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Pytanie", "TestID", "dbo.Test");
            DropForeignKey("dbo.Testy_ucznia", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Nieobecnosc", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Spoznienie", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Uwaga", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Ogloszenie_dla_rodzicow", "RodzicID", "dbo.Rodzic");
            DropIndex("dbo.Uwaga", new[] { "NauczycielID" });
            DropIndex("dbo.Uwaga", new[] { "UczenID" });
            AlterColumn("dbo.Uwaga", "NauczycielID", c => c.Int());
            AlterColumn("dbo.Uwaga", "UczenID", c => c.Int());
            CreateIndex("dbo.Uwaga", "NauczycielID");
            CreateIndex("dbo.Uwaga", "UczenID");
            AddForeignKey("dbo.Lekcja", "KlasaID", "dbo.Klasa", "KlasaID", cascadeDelete: true);
            AddForeignKey("dbo.Plik", "KlasaID", "dbo.Klasa", "KlasaID", cascadeDelete: true);
            AddForeignKey("dbo.Test", "KlasaID", "dbo.Klasa", "KlasaID", cascadeDelete: true);
            AddForeignKey("dbo.Uczen", "KlasaID", "dbo.Klasa", "KlasaID", cascadeDelete: true);
            AddForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Ocena", "PrzedmiotID", "dbo.Przedmiot", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Ocena", "UczenID", "dbo.Uczen", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Plik", "PrzedmiotID", "dbo.Przedmiot", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Test", "PrzedmiotID", "dbo.Przedmiot", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Pytanie", "TestID", "dbo.Test", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Testy_ucznia", "UczenID", "dbo.Uczen", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Nieobecnosc", "UczenID", "dbo.Uczen", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Spoznienie", "UczenID", "dbo.Uczen", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Uwaga", "UczenID", "dbo.Uczen", "ID", cascadeDelete: true);
            AddForeignKey("dbo.Ogloszenie_dla_rodzicow", "RodzicID", "dbo.Rodzic", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ogloszenie_dla_rodzicow", "RodzicID", "dbo.Rodzic");
            DropForeignKey("dbo.Uwaga", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Spoznienie", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Nieobecnosc", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Testy_ucznia", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Pytanie", "TestID", "dbo.Test");
            DropForeignKey("dbo.Test", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Plik", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Ocena", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Ocena", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot");
            DropForeignKey("dbo.Uczen", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Test", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Plik", "KlasaID", "dbo.Klasa");
            DropForeignKey("dbo.Lekcja", "KlasaID", "dbo.Klasa");
            DropIndex("dbo.Uwaga", new[] { "UczenID" });
            DropIndex("dbo.Uwaga", new[] { "NauczycielID" });
            AlterColumn("dbo.Uwaga", "UczenID", c => c.Int(nullable: false));
            AlterColumn("dbo.Uwaga", "NauczycielID", c => c.Int(nullable: false));
            CreateIndex("dbo.Uwaga", "UczenID");
            CreateIndex("dbo.Uwaga", "NauczycielID");
            AddForeignKey("dbo.Ogloszenie_dla_rodzicow", "RodzicID", "dbo.Rodzic", "ID");
            AddForeignKey("dbo.Uwaga", "UczenID", "dbo.Uczen", "ID");
            AddForeignKey("dbo.Spoznienie", "UczenID", "dbo.Uczen", "ID");
            AddForeignKey("dbo.Nieobecnosc", "UczenID", "dbo.Uczen", "ID");
            AddForeignKey("dbo.Testy_ucznia", "UczenID", "dbo.Uczen", "ID");
            AddForeignKey("dbo.Pytanie", "TestID", "dbo.Test", "ID");
            AddForeignKey("dbo.Test", "PrzedmiotID", "dbo.Przedmiot", "ID");
            AddForeignKey("dbo.Plik", "PrzedmiotID", "dbo.Przedmiot", "ID");
            AddForeignKey("dbo.Ocena", "UczenID", "dbo.Uczen", "ID");
            AddForeignKey("dbo.Ocena", "PrzedmiotID", "dbo.Przedmiot", "ID");
            AddForeignKey("dbo.Lekcja", "PrzedmiotID", "dbo.Przedmiot", "ID");
            AddForeignKey("dbo.Uczen", "KlasaID", "dbo.Klasa", "KlasaID");
            AddForeignKey("dbo.Test", "KlasaID", "dbo.Klasa", "KlasaID");
            AddForeignKey("dbo.Plik", "KlasaID", "dbo.Klasa", "KlasaID");
            AddForeignKey("dbo.Lekcja", "KlasaID", "dbo.Klasa", "KlasaID");
        }
    }
}
