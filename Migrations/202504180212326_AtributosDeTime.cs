namespace Futebol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AtributosDeTime : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Times", "Estado", c => c.String());
            AddColumn("dbo.Times", "Cidade", c => c.String());
            AddColumn("dbo.Times", "AnoFundacao", c => c.Int(nullable: false));
            AddColumn("dbo.Times", "Estadio", c => c.String());
            AddColumn("dbo.Times", "CapacidadeEstadio", c => c.Int(nullable: false));
            AddColumn("dbo.Times", "CoresUniforme", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Times", "CoresUniforme");
            DropColumn("dbo.Times", "CapacidadeEstadio");
            DropColumn("dbo.Times", "Estadio");
            DropColumn("dbo.Times", "AnoFundacao");
            DropColumn("dbo.Times", "Cidade");
            DropColumn("dbo.Times", "Estado");
        }
    }
}
