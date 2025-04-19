namespace Futebol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EstatisticasPartidaAtualizada : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.EstatisticasPartidas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        PartidaID = c.Int(nullable: false),
                        TimeID = c.Int(nullable: false),
                        JogadorID = c.Int(nullable: false),
                        SequenciaGol = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Jogadors", t => t.JogadorID, cascadeDelete: true)
                .ForeignKey("dbo.Partidas", t => t.PartidaID)
                .ForeignKey("dbo.Times", t => t.TimeID)
                .Index(t => t.PartidaID)
                .Index(t => t.TimeID)
                .Index(t => t.JogadorID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EstatisticasPartidas", "TimeID", "dbo.Times");
            DropForeignKey("dbo.EstatisticasPartidas", "PartidaID", "dbo.Partidas");
            DropForeignKey("dbo.EstatisticasPartidas", "JogadorID", "dbo.Jogadors");
            DropIndex("dbo.EstatisticasPartidas", new[] { "JogadorID" });
            DropIndex("dbo.EstatisticasPartidas", new[] { "TimeID" });
            DropIndex("dbo.EstatisticasPartidas", new[] { "PartidaID" });
            DropTable("dbo.EstatisticasPartidas");
        }
    }
}
