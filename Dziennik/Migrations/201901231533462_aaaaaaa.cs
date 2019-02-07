namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaaaaaa : DbMigration
    {
        public override void Up()
        {
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
                        data_odpowiedz = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Nauczyciel", t => t.NauczycielID)
                .ForeignKey("dbo.Rodzic", t => t.RodzicID)
                .Index(t => t.NauczycielID)
                .Index(t => t.RodzicID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Zapytanie", "RodzicID", "dbo.Rodzic");
            DropForeignKey("dbo.Zapytanie", "NauczycielID", "dbo.Nauczyciel");
            DropIndex("dbo.Zapytanie", new[] { "RodzicID" });
            DropIndex("dbo.Zapytanie", new[] { "NauczycielID" });
            DropTable("dbo.Zapytanie");
        }
    }
}
