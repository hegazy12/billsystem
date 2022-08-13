namespace sara_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fffl : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.invoices", "state_id", "dbo.states");
            DropIndex("dbo.invoices", new[] { "state_id" });
            AddColumn("dbo.invoices", "state", c => c.String());
            AddColumn("dbo.invoices", "creator_key", c => c.Int(nullable: false));
            DropColumn("dbo.invoices", "state_id");
            DropTable("dbo.states");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.states",
                c => new
                    {
                        id = c.Int(nullable: false, identity: true),
                        statename = c.String(),
                    })
                .PrimaryKey(t => t.id);
            
            AddColumn("dbo.invoices", "state_id", c => c.Int());
            DropColumn("dbo.invoices", "creator_key");
            DropColumn("dbo.invoices", "state");
            CreateIndex("dbo.invoices", "state_id");
            AddForeignKey("dbo.invoices", "state_id", "dbo.states", "id");
        }
    }
}
