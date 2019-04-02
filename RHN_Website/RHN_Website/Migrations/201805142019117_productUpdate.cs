namespace RHN_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "StockPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Product", "SellingPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Sale", "TotalSellingPrice", c => c.Double(nullable: false));
            DropColumn("dbo.Product", "Price");
            DropColumn("dbo.Sale", "TotalPrice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Sale", "TotalPrice", c => c.Double(nullable: false));
            AddColumn("dbo.Product", "Price", c => c.Double(nullable: false));
            DropColumn("dbo.Sale", "TotalSellingPrice");
            DropColumn("dbo.Product", "SellingPrice");
            DropColumn("dbo.Product", "StockPrice");
        }
    }
}
