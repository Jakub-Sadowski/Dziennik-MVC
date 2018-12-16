namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ads1 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Test", "czas_trwania", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Test", "czas_trwania", c => c.Int(nullable: false));
        }
    }
}
