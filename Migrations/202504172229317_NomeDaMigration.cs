namespace Futebol.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class NomeDaMigration : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Jogadors", "CamisaNumero", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Jogadors", "CamisaNumero");
        }
    }
}
