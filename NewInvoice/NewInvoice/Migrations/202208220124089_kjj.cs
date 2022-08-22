namespace NewInvoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kjj : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.comments", name: "invoicenumber", newName: "invoice_invoicenumber");
            RenameIndex(table: "dbo.comments", name: "IX_invoicenumber", newName: "IX_invoice_invoicenumber");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.comments", name: "IX_invoice_invoicenumber", newName: "IX_invoicenumber");
            RenameColumn(table: "dbo.comments", name: "invoice_invoicenumber", newName: "invoicenumber");
        }
    }
}
