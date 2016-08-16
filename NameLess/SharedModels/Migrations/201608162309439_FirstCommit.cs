namespace SharedModels.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class FirstCommit : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CamposPesquisas",
                c => new
                    {
                        CamposPesquisaId = c.Int(nullable: false, identity: true),
                        TagId = c.Int(nullable: false),
                        IdCampoTexto = c.String(),
                        IdCampoBotao = c.String(),
                        TipoCampo = c.String(),
                        UsuarioId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.CamposPesquisaId)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId)
                .Index(t => t.TagId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        TagId = c.Int(nullable: false, identity: true),
                        Tag = c.String(),
                        DescricaoTag = c.String(),
                        ObservacaoTag = c.String(),
                        UsuarioId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.TagId)
                .ForeignKey("dbo.AspNetUsers", t => t.UsuarioId)
                .Index(t => t.UsuarioId);
            
            CreateTable(
                "dbo.Pesquisas",
                c => new
                    {
                        PesquisaId = c.Int(nullable: false, identity: true),
                        TermoPesquisado = c.String(),
                        Localizacao = c.Geography(),
                        DataPesquisa = c.String(),
                        ClienteId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.PesquisaId)
                .ForeignKey("dbo.Clientes", t => t.ClienteId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.ClienteId)
                .Index(t => t.TagId);
            
            CreateTable(
                "dbo.Clientes",
                c => new
                    {
                        ClienteId = c.Int(nullable: false, identity: true),
                        NomeCliente = c.String(),
                        EnderecoCliente = c.String(),
                        CnpjCliente = c.String(),
                    })
                .PrimaryKey(t => t.ClienteId);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        ClienteId = c.Int(),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Clientes", t => t.ClienteId)
                .Index(t => t.ClienteId)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Pesquisas", "TagId", "dbo.Tags");
            DropForeignKey("dbo.Tags", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUsers", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.CamposPesquisas", "UsuarioId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Pesquisas", "ClienteId", "dbo.Clientes");
            DropForeignKey("dbo.CamposPesquisas", "TagId", "dbo.Tags");
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.AspNetUsers", new[] { "ClienteId" });
            DropIndex("dbo.Pesquisas", new[] { "TagId" });
            DropIndex("dbo.Pesquisas", new[] { "ClienteId" });
            DropIndex("dbo.Tags", new[] { "UsuarioId" });
            DropIndex("dbo.CamposPesquisas", new[] { "UsuarioId" });
            DropIndex("dbo.CamposPesquisas", new[] { "TagId" });
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Clientes");
            DropTable("dbo.Pesquisas");
            DropTable("dbo.Tags");
            DropTable("dbo.CamposPesquisas");
        }
    }
}
