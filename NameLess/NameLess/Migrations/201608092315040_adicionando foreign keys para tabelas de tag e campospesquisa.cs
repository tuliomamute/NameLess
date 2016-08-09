namespace NameLess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class adicionandoforeignkeysparatabelasdetagecampospesquisa : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.CamposPesquisas", "UsuarioId", c => c.String(maxLength: 128));
            AddColumn("dbo.Tags", "UsuarioId", c => c.String(maxLength: 128));
            CreateIndex("dbo.CamposPesquisas", "UsuarioId");
            CreateIndex("dbo.Tags", "UsuarioId");
            AddForeignKey("dbo.CamposPesquisas", "UsuarioId", "dbo.AspNetUsers", "Id");
            AddForeignKey("dbo.Tags", "UsuarioId", "dbo.AspNetUsers", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Tags", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CamposPesquisas", "UsuarioId", "dbo.AspNetUsers");
            DropIndex("dbo.Tags", new[] { "UsuarioId" });
            DropIndex("dbo.CamposPesquisas", new[] { "UsuarioId" });
            DropColumn("dbo.Tags", "UsuarioId");
            DropColumn("dbo.CamposPesquisas", "UsuarioId");
        }
    }
}
