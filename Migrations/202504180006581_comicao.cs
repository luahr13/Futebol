namespace Futebol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class comicao : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ComicaoTecnicas",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        NomeDaComicao = c.String(),
                        TimeID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Times", t => t.TimeID, cascadeDelete: true)
                .Index(t => t.TimeID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ComicaoTecnicas", "TimeID", "dbo.Times");
            DropIndex("dbo.ComicaoTecnicas", new[] { "TimeID" });
            DropTable("dbo.ComicaoTecnicas");
        }
    }
}
