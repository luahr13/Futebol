namespace Futebol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class partidasDbContext : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Partidas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        TimeCasaID = c.Int(nullable: false),
                        TimeVisitanteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Times", t => t.TimeCasaID)
                .ForeignKey("dbo.Times", t => t.TimeVisitanteID)
                .Index(t => t.TimeCasaID)
                .Index(t => t.TimeVisitanteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Partidas", "TimeVisitanteID", "dbo.Times");
            DropForeignKey("dbo.Partidas", "TimeCasaID", "dbo.Times");
            DropIndex("dbo.Partidas", new[] { "TimeVisitanteID" });
            DropIndex("dbo.Partidas", new[] { "TimeCasaID" });
            DropTable("dbo.Partidas");
        }
    }
}
