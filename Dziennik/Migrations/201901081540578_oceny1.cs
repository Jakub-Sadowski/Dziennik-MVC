namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class oceny1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Ocena", new[] { "UczenID" });
            AlterColumn("dbo.Ocena", "UczenID", c => c.Int(nullable: false));
            CreateIndex("dbo.Ocena", "UczenID");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Ocena", new[] { "UczenID" });
            AlterColumn("dbo.Ocena", "UczenID", c => c.Int());
            CreateIndex("dbo.Ocena", "UczenID");
        }
    }
}
