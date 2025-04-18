namespace Futebol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtributosJogador : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jogadors", "DataDeNascimento", c => c.DateTime(nullable: false));
            AddColumn("dbo.Jogadors", "Nacionalidade", c => c.String());
            AddColumn("dbo.Jogadors", "Posicao", c => c.Int(nullable: false));
            AddColumn("dbo.Jogadors", "Altura", c => c.Single(nullable: false));
            AddColumn("dbo.Jogadors", "Peso", c => c.Single(nullable: false));
            AddColumn("dbo.Jogadors", "PeDominante", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jogadors", "PeDominante");
            DropColumn("dbo.Jogadors", "Peso");
            DropColumn("dbo.Jogadors", "Altura");
            DropColumn("dbo.Jogadors", "Posicao");
            DropColumn("dbo.Jogadors", "Nacionalidade");
            DropColumn("dbo.Jogadors", "DataDeNascimento");
        }
    }
}
