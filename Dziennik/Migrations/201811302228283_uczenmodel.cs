namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uczenmodel : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Uczen", "rodzicImie", c => c.String());
            AddColumn("dbo.Uczen", "rodzicNazwisko", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Uczen", "rodzicNazwisko");
            DropColumn("dbo.Uczen", "rodzicImie");
        }
    }
}
