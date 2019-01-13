namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ocea : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Ocena", "IdEdytujacego", c => c.Int(nullable: false));
            AddColumn("dbo.Ocena", "dataEdycji", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Ocena", "dataEdycji");
            DropColumn("dbo.Ocena", "IdEdytujacego");
        }
    }
}
