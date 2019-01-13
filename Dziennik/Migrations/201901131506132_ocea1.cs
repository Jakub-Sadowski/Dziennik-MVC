namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ocea1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ocena", "IdEdytujacego", c => c.Int());
            AlterColumn("dbo.Ocena", "dataEdycji", c => c.DateTime());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ocena", "dataEdycji", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Ocena", "IdEdytujacego", c => c.Int(nullable: false));
        }
    }
}
