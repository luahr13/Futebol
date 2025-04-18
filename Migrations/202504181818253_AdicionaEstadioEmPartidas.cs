namespace Futebol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionaEstadioEmPartidas : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Partidas", "Estadio", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Partidas", "Estadio");
        }
    }
}
