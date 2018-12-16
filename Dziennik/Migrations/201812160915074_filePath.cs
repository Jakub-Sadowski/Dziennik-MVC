namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class filePath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Plik", "FilePath", c => c.String());
            AddColumn("dbo.Plik", "DataDodania", c => c.DateTime(nullable: false));
            DropColumn("dbo.Plik", "data");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Plik", "data", c => c.DateTime(nullable: false));
            DropColumn("dbo.Plik", "DataDodania");
            DropColumn("dbo.Plik", "FilePath");
        }
    }
}
