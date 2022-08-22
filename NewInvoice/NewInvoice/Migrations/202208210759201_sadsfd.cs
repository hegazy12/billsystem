namespace NewInvoice.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class sadsfd : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.comments",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        content = c.String(),
                        invoicenumber = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.id)
                .ForeignKey("dbo.invoices", t => t.invoicenumber)
                .Index(t => t.invoicenumber);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.comments", "invoicenumber", "dbo.invoices");
            DropIndex("dbo.comments", new[] { "invoicenumber" });
            DropTable("dbo.comments");
        }
    }
}
