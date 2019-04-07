namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaaaaaaaa : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Wiadomosc", "Nauczyciel_NauczycielID", "dbo.Nauczyciel");
            DropForeignKey("dbo.Wiadomosc", "Rodzic_ID", "dbo.Rodzic");
            DropForeignKey("dbo.Wiadomosc", "Uczen_ID", "dbo.Uczen");
            DropIndex("dbo.Wiadomosc", new[] { "Nauczyciel_NauczycielID" });
            DropIndex("dbo.Wiadomosc", new[] { "Rodzic_ID" });
            DropIndex("dbo.Wiadomosc", new[] { "Uczen_ID" });
            CreateTable(
                "dbo.Zapytanie",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NauczycielID = c.Int(),
                        RodzicID = c.Int(),
                        pytanie = c.String(),
                        odpowiedz = c.String(),
                        data_pytania = c.DateTime(nullable: false),
                        data_odpowiedz = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Rodzic", t => t.RodzicID)
                .Index(t => t.NauczycielID)
                .Index(t => t.RodzicID);
            
            AlterColumn("dbo.Nauczyciel", "Imie", c => c.String());
            AlterColumn("dbo.Nauczyciel", "Nazwisko", c => c.String());
            AlterColumn("dbo.Nauczyciel", "Login", c => c.String());
            AlterColumn("dbo.Nauczyciel", "Haslo", c => c.String());
            DropTable("dbo.Wiadomosc");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Wiadomosc",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        odbiorca = c.String(nullable: false),
                        nadawca = c.String(nullable: false),
                        naglowek = c.String(),
                        tresc = c.String(nullable: false),
                        data = c.DateTime(nullable: false),
                        Nauczyciel_NauczycielID = c.Int(),
                        Rodzic_ID = c.Int(),
                        Uczen_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropForeignKey("dbo.Zapytanie", "RodzicID", "dbo.Rodzic");
            DropForeignKey("dbo.Zapytanie", "NauczycielID", "dbo.Nauczyciel");
            DropIndex("dbo.Zapytanie", new[] { "RodzicID" });
            DropIndex("dbo.Zapytanie", new[] { "NauczycielID" });
            AlterColumn("dbo.Nauczyciel", "Haslo", c => c.String(nullable: false));
            AlterColumn("dbo.Nauczyciel", "Login", c => c.String(nullable: false));
            AlterColumn("dbo.Nauczyciel", "Nazwisko", c => c.String(nullable: false));
            AlterColumn("dbo.Nauczyciel", "Imie", c => c.String(nullable: false));
            DropTable("dbo.Zapytanie");
            CreateIndex("dbo.Wiadomosc", "Uczen_ID");
            CreateIndex("dbo.Wiadomosc", "Rodzic_ID");
            CreateIndex("dbo.Wiadomosc", "Nauczyciel_NauczycielID");
            AddForeignKey("dbo.Wiadomosc", "Uczen_ID", "dbo.Uczen", "ID");
            AddForeignKey("dbo.Wiadomosc", "Rodzic_ID", "dbo.Rodzic", "ID");
            AddForeignKey("dbo.Wiadomosc", "Nauczyciel_NauczycielID", "dbo.Nauczyciel", "NauczycielID");
        }
    }
}
