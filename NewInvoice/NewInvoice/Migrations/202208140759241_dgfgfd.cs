namespace NewInvoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class dgfgfd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.purchaseorders", "date", c => c.String());
            DropColumn("dbo.purchaseorders", "created");
        }
        
        public override void Down()
        {
            AddColumn("dbo.purchaseorders", "created", c => c.String());
            DropColumn("dbo.purchaseorders", "date");
        }
    }
}
