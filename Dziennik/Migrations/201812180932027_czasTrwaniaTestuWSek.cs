namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class czasTrwaniaTestuWSek : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Test", "czasTrwania", c => c.Int(nullable: false));
            DropColumn("dbo.Test", "czas_trwania");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Test", "czas_trwania", c => c.DateTime(nullable: false));
            DropColumn("dbo.Test", "czasTrwania");
        }
    }
}
