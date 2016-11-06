namespace SharedModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CriacaoSiglaEstado : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Pesquisas", "SiglaEstado", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Pesquisas", "SiglaEstado");
        }
    }
}
