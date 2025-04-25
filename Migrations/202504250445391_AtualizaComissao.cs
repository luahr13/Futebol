namespace Futebol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtualizaComissao : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.ComicaoTecnicas", newName: "ComissaoTecnicas");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.ComissaoTecnicas", newName: "ComicaoTecnicas");
        }
    }
}
