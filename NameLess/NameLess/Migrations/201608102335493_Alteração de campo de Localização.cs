namespace NameLess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteraçãodecampodeLocalização : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pesquisas", "Localizacao", c => c.Geography());
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pesquisas", "Localizacao", c => c.String());
        }
    }
}
