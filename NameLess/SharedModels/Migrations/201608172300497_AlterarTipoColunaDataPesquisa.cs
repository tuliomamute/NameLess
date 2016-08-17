namespace SharedModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlterarTipoColunaDataPesquisa : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Pesquisas", "DataPesquisa", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Pesquisas", "DataPesquisa", c => c.String());
        }
    }
}
