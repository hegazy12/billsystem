namespace NewInvoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class invoice : DbMigration
    {
        public override void Up()
        {
            DropColumn("dbo.approvers", "invoice_key");
            DropColumn("dbo.approvers", "user_key");
            DropColumn("dbo.invoices", "creator_key");
        }
        
        public override void Down()
        {
            AddColumn("dbo.invoices", "creator_key", c => c.Int(nullable: false));
            AddColumn("dbo.approvers", "user_key", c => c.Int(nullable: false));
            AddColumn("dbo.approvers", "invoice_key", c => c.String());
        }
    }
}
