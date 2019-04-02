namespace RHN_Website.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productUpdate2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Product", "DateAdded", c => c.DateTime(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Product", "DateAdded");
        }
    }
}
