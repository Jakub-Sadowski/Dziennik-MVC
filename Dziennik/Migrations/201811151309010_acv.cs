namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class acv : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Uczen", "RodzicID", "dbo.Rodzic");
            DropIndex("dbo.Uczen", new[] { "RodzicID" });
            AlterColumn("dbo.Uczen", "RodzicID", c => c.Int(nullable: false));
            CreateIndex("dbo.Uczen", "RodzicID");
            AddForeignKey("dbo.Uczen", "RodzicID", "dbo.Rodzic", "ID", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Uczen", "RodzicID", "dbo.Rodzic");
            DropIndex("dbo.Uczen", new[] { "RodzicID" });
            AlterColumn("dbo.Uczen", "RodzicID", c => c.Int());
            CreateIndex("dbo.Uczen", "RodzicID");
            AddForeignKey("dbo.Uczen", "RodzicID", "dbo.Rodzic", "ID");
        }
    }
}
