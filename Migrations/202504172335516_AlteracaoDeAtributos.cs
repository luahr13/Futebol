namespace Futebol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AlteracaoDeAtributos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jogadors", "NomeDoJogador", c => c.String());
            AddColumn("dbo.Times", "NomeDoTime", c => c.String());
            DropColumn("dbo.Jogadors", "Nome");
            DropColumn("dbo.Times", "Nome");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Times", "Nome", c => c.String());
            AddColumn("dbo.Jogadors", "Nome", c => c.String());
            DropColumn("dbo.Times", "NomeDoTime");
            DropColumn("dbo.Jogadors", "NomeDoJogador");
        }
    }
}
