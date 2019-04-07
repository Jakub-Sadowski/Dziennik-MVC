namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class pytaniauczniow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Pytanie_ucznia",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NauczycielID = c.Int(),
                        UczenID = c.Int(),
                        Pytanie = c.String(),
                        Odpowiedz = c.String(),
                        Data_pytania = c.DateTime(nullable: false),
                        Data_odpowiedzi = c.DateTime(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Uczen", t => t.UczenID)
                .Index(t => t.NauczycielID)
                .Index(t => t.UczenID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Pytanie_ucznia", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Pytanie_ucznia", "NauczycielID", "dbo.Nauczyciel");
            DropIndex("dbo.Pytanie_ucznia", new[] { "UczenID" });
            DropIndex("dbo.Pytanie_ucznia", new[] { "NauczycielID" });
            DropTable("dbo.Pytanie_ucznia");
        }
    }
}
