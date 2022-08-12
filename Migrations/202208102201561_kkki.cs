namespace sara_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class kkki : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.users", "invoice_invoicenumber", "dbo.invoices");
            DropIndex("dbo.users", new[] { "invoice_invoicenumber" });
            CreateTable(
                "dbo.approvers",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        invoice = c.String(),
                        user = c.Int(nullable: false),
                        user_id = c.Int(),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.users", t => t.user_id)
                .Index(t => t.user_id);
            
            DropColumn("dbo.users", "invoice_invoicenumber");
        }
        
        public override void Down()
        {
            AddColumn("dbo.users", "invoice_invoicenumber", c => c.String(maxLength: 128));
            DropForeignKey("dbo.approvers", "user_id", "dbo.users");
            DropIndex("dbo.approvers", new[] { "user_id" });
            DropTable("dbo.approvers");
            CreateIndex("dbo.users", "invoice_invoicenumber");
            AddForeignKey("dbo.users", "invoice_invoicenumber", "dbo.invoices", "invoicenumber");
        }
    }
}
