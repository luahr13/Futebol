namespace Futebol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class PerssonalizacaoFront : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Partidas", "NomeDaPartida", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Partidas", "NomeDaPartida");
        }
    }
}
