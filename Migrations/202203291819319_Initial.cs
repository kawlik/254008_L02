namespace _254008_L02.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ModelRates", "ModelTable_ID", "dbo.ModelTables");
            DropIndex("dbo.ModelRates", new[] { "ModelTable_ID" });
            CreateTable(
                "dbo.Serials",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        data = c.String(),
                    })
                .PrimaryKey(t => t.ID);
            
            DropTable("dbo.ModelTables");
            DropTable("dbo.ModelRates");
        }
        
        public override void Down()
        {
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
                .PrimaryKey(t => t.ID);
            
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
            
            DropTable("dbo.Serials");
            CreateIndex("dbo.ModelRates", "ModelTable_ID");
            AddForeignKey("dbo.ModelRates", "ModelTable_ID", "dbo.ModelTables", "ID");
        }
    }
}
