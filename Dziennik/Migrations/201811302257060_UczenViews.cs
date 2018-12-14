namespace Dziennik.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UczenViews : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.Uczen", "rodzicImie");
            DropColumn("dbo.Uczen", "rodzicNazwisko");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Uczen", "rodzicNazwisko", c => c.String());
            AddColumn("dbo.Uczen", "rodzicImie", c => c.String());
        }
    }
}
