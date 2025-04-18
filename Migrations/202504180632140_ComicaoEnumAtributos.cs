namespace Futebol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ComicaoEnumAtributos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ComicaoTecnicas", "Cargo", c => c.Int(nullable: false));
            AddColumn("dbo.ComicaoTecnicas", "DataDeNascimento", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.ComicaoTecnicas", "DataDeNascimento");
            DropColumn("dbo.ComicaoTecnicas", "Cargo");
        }
    }
}
