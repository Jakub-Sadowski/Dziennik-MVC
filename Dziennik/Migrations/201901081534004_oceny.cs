namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oceny : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ocena", new[] { "PrzedmiotID" });
            DropIndex("dbo.Ocena", new[] { "NauczycielID" });
            AlterColumn("dbo.Ocena", "PrzedmiotID", c => c.Int(nullable: false));
            AlterColumn("dbo.Ocena", "NauczycielID", c => c.Int(nullable: false));
            CreateIndex("dbo.Ocena", "PrzedmiotID");
            CreateIndex("dbo.Ocena", "NauczycielID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ocena", new[] { "NauczycielID" });
            DropIndex("dbo.Ocena", new[] { "PrzedmiotID" });
            AlterColumn("dbo.Ocena", "NauczycielID", c => c.Int());
            AlterColumn("dbo.Ocena", "PrzedmiotID", c => c.Int());
            CreateIndex("dbo.Ocena", "NauczycielID");
            CreateIndex("dbo.Ocena", "PrzedmiotID");
        }
    }
}
