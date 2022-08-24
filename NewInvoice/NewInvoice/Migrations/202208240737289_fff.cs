namespace NewInvoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fff : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.approvers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        decision = c.String(),
                        invoice_invoicenumber = c.String(maxLength: 128),
                        user_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.invoices", t => t.invoice_invoicenumber)
                .ForeignKey("dbo.users", t => t.user_id)
                .Index(t => t.invoice_invoicenumber)
                .Index(t => t.user_id);
            
            CreateTable(
                "dbo.invoices",
                c => new
                    {
                        invoicenumber = c.String(nullable: false, maxLength: 128),
                        value = c.Single(nullable: false),
                        description = c.String(),
                        state = c.String(),
                        delete_state = c.Int(nullable: false),
                        creator_id = c.Int(),
                        currency_symbol = c.String(maxLength: 128),
                        projectnumber_projectnumber = c.String(maxLength: 128),
                        purchaseorder_ordernumber = c.String(maxLength: 128),
                        vendor_vendornumber = c.Int(),
                    })
                .PrimaryKey(t => t.invoicenumber)
                .ForeignKey("dbo.users", t => t.creator_id)
                .ForeignKey("dbo.currencies", t => t.currency_symbol)
                .ForeignKey("dbo.projects", t => t.projectnumber_projectnumber)
                .ForeignKey("dbo.purchaseorders", t => t.purchaseorder_ordernumber)
                .ForeignKey("dbo.vendors", t => t.vendor_vendornumber)
                .Index(t => t.creator_id)
                .Index(t => t.currency_symbol)
                .Index(t => t.projectnumber_projectnumber)
                .Index(t => t.purchaseorder_ordernumber)
                .Index(t => t.vendor_vendornumber);
            
            CreateTable(
                "dbo.comments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        content = c.String(),
                        invoice_invoicenumber = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.invoices", t => t.invoice_invoicenumber)
                .Index(t => t.invoice_invoicenumber);
            
            CreateTable(
                "dbo.users",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        name = c.String(),
                        email = c.String(),
                        password = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            CreateTable(
                "dbo.currencies",
                c => new
                    {
                        symbol = c.String(nullable: false, maxLength: 128),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.symbol);
            
            CreateTable(
                "dbo.docs",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        path = c.String(),
                        invoice_invoicenumber = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.invoices", t => t.invoice_invoicenumber )
                .Index(t => t.invoice_invoicenumber);
            
            CreateTable(
                "dbo.projects",
                c => new
                    {
                        projectnumber = c.String(nullable: false, maxLength: 128),
                        projectname = c.String(),
                        projectmang_id = c.Int(),
                    })
                .PrimaryKey(t => t.projectnumber)
                .ForeignKey("dbo.users", t => t.projectmang_id)
                .Index(t => t.projectmang_id);
            
            CreateTable(
                "dbo.purchaseorders",
                c => new
                    {
                        ordernumber = c.String(nullable: false, maxLength: 128),
                        date = c.String(),
                    })
                .PrimaryKey(t => t.ordernumber);
            
            CreateTable(
                "dbo.vendors",
                c => new
                    {
                        vendornumber = c.Int(nullable: false, identity: true),
                        name = c.String(),
                    })
                .PrimaryKey(t => t.vendornumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.approvers", "user_id", "dbo.users");
            DropForeignKey("dbo.invoices", "vendor_vendornumber", "dbo.vendors");
            DropForeignKey("dbo.invoices", "purchaseorder_ordernumber", "dbo.purchaseorders");
            DropForeignKey("dbo.invoices", "projectnumber_projectnumber", "dbo.projects");
            DropForeignKey("dbo.projects", "projectmang_id", "dbo.users");
            DropForeignKey("dbo.docs", "invoice_invoicenumber", "dbo.invoices");
            DropForeignKey("dbo.invoices", "currency_symbol", "dbo.currencies");
            DropForeignKey("dbo.invoices", "creator_id", "dbo.users");
            DropForeignKey("dbo.comments", "invoice_invoicenumber", "dbo.invoices");
            DropForeignKey("dbo.approvers", "invoice_invoicenumber", "dbo.invoices");
            DropIndex("dbo.projects", new[] { "projectmang_id" });
            DropIndex("dbo.docs", new[] { "invoice_invoicenumber" });
            DropIndex("dbo.comments", new[] { "invoice_invoicenumber" });
            DropIndex("dbo.invoices", new[] { "vendor_vendornumber" });
            DropIndex("dbo.invoices", new[] { "purchaseorder_ordernumber" });
            DropIndex("dbo.invoices", new[] { "projectnumber_projectnumber" });
            DropIndex("dbo.invoices", new[] { "currency_symbol" });
            DropIndex("dbo.invoices", new[] { "creator_id" });
            DropIndex("dbo.approvers", new[] { "user_id" });
            DropIndex("dbo.approvers", new[] { "invoice_invoicenumber" });
            DropTable("dbo.vendors");
            DropTable("dbo.purchaseorders");
            DropTable("dbo.projects");
            DropTable("dbo.docs");
            DropTable("dbo.currencies");
            DropTable("dbo.users");
            DropTable("dbo.comments");
            DropTable("dbo.invoices");
            DropTable("dbo.approvers");
        }
    }
}
