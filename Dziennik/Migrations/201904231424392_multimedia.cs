namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class multimedia : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Multimedia",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        Path = c.String(),
                        Type = c.Int(nullable: false),
                        Pytanie_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Pytanie", t => t.Pytanie_ID)
                .Index(t => t.Pytanie_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Multimedia", "Pytanie_ID", "dbo.Pytanie");
            DropIndex("dbo.Multimedia", new[] { "Pytanie_ID" });
            DropTable("dbo.Multimedia");
        }
    }
}
