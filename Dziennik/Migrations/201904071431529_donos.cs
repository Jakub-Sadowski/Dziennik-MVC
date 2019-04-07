namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class donos : DbMigration
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
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Donos", "UczenID", "dbo.Uczen");
            DropForeignKey("dbo.Donos", "NauczycielID", "dbo.Nauczyciel");
            DropIndex("dbo.Donos", new[] { "UczenID" });
            DropIndex("dbo.Donos", new[] { "NauczycielID" });
            DropTable("dbo.Donos");
        }
    }
}
