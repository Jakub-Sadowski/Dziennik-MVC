namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class aaa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Ocena", "ocena", c => c.Double(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Ocena", "ocena", c => c.Int(nullable: false));
        }
    }
}
