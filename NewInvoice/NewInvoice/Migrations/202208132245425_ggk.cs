namespace NewInvoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ggk : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.invoices", "delete_state", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.invoices", "delete_state");
        }
    }
}
