namespace RHN_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class storeUpdate : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Store", "QuantityNeeded", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Store", "QuantityNeeded");
        }
    }
}
