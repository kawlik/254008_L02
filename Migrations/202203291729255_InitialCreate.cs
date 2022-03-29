namespace _254008_L02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ModelTables",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        code = c.String(),
                        currency = c.String(),
                        table = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.ModelRates",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        mid = c.Single(nullable: false),
                        no = c.String(),
                        effectiveDate = c.String(),
                        ModelTable_ID = c.Int(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.ModelTables", t => t.ModelTable_ID)
                .Index(t => t.ModelTable_ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ModelRates", "ModelTable_ID", "dbo.ModelTables");
            DropIndex("dbo.ModelRates", new[] { "ModelTable_ID" });
            DropTable("dbo.ModelRates");
            DropTable("dbo.ModelTables");
        }
    }
}
