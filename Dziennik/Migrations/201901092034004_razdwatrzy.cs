namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class razdwatrzy : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Ogloszenie_dla_rodzicow", "RodzicID", "dbo.Rodzic");
            RenameColumn(table: "dbo.Ogloszenie_dla_rodzicow", name: "RodzicID", newName: "Rodzic_ID");
            RenameColumn(table: "dbo.Ogloszenie_dla_rodzicow", name: "klasa_KlasaID", newName: "KlasaID");
            RenameIndex(table: "dbo.Ogloszenie_dla_rodzicow", name: "IX_klasa_KlasaID", newName: "IX_KlasaID");
            RenameIndex(table: "dbo.Ogloszenie_dla_rodzicow", name: "IX_RodzicID", newName: "IX_Rodzic_ID");
            AddForeignKey("dbo.Ogloszenie_dla_rodzicow", "Rodzic_ID", "dbo.Rodzic", "ID");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Ogloszenie_dla_rodzicow", "Rodzic_ID", "dbo.Rodzic");
            RenameIndex(table: "dbo.Ogloszenie_dla_rodzicow", name: "IX_Rodzic_ID", newName: "IX_RodzicID");
            RenameIndex(table: "dbo.Ogloszenie_dla_rodzicow", name: "IX_KlasaID", newName: "IX_klasa_KlasaID");
            RenameColumn(table: "dbo.Ogloszenie_dla_rodzicow", name: "KlasaID", newName: "klasa_KlasaID");
            RenameColumn(table: "dbo.Ogloszenie_dla_rodzicow", name: "Rodzic_ID", newName: "RodzicID");
            AddForeignKey("dbo.Ogloszenie_dla_rodzicow", "RodzicID", "dbo.Rodzic", "ID", cascadeDelete: true);
        }
    }
}
