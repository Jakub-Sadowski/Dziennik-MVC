namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaaaaa1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Lekcja", "dzien", c => c.Int());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Lekcja", "dzien", c => c.String());
        }
    }
}
