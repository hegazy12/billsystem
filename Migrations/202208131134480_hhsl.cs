namespace sara_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hhsl : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.approvers", name: "user_id", newName: "user_key_id");
            RenameIndex(table: "dbo.approvers", name: "IX_user_id", newName: "IX_user_key_id");
            AddColumn("dbo.approvers", "invoice_key", c => c.String());
            AddColumn("dbo.approvers", "invoice_invoicenumber", c => c.String(maxLength: 128));
            CreateIndex("dbo.approvers", "invoice_invoicenumber");
            AddForeignKey("dbo.approvers", "invoice_invoicenumber", "dbo.invoices", "invoicenumber");
            DropColumn("dbo.approvers", "invoice");
        }
        
        public override void Down()
        {
            AddColumn("dbo.approvers", "invoice", c => c.String());
            DropForeignKey("dbo.approvers", "invoice_invoicenumber", "dbo.invoices");
            DropIndex("dbo.approvers", new[] { "invoice_invoicenumber" });
            DropColumn("dbo.approvers", "invoice_invoicenumber");
            DropColumn("dbo.approvers", "invoice_key");
            RenameIndex(table: "dbo.approvers", name: "IX_user_key_id", newName: "IX_user_id");
            RenameColumn(table: "dbo.approvers", name: "user_key_id", newName: "user_id");
        }
    }
}
