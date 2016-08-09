namespace NameLess.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteraçãodeNomedeForeingKey : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.AspNetUsers", name: "Cliente_ClienteId", newName: "ClienteId");
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_Cliente_ClienteId", newName: "IX_ClienteId");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.AspNetUsers", name: "IX_ClienteId", newName: "IX_Cliente_ClienteId");
            RenameColumn(table: "dbo.AspNetUsers", name: "ClienteId", newName: "Cliente_ClienteId");
        }
    }
}
