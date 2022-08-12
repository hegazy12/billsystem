namespace sara_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hhj : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.invoices", "creator_id", c => c.Int());
            CreateIndex("dbo.invoices", "creator_id");
            AddForeignKey("dbo.invoices", "creator_id", "dbo.users", "id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.invoices", "creator_id", "dbo.users");
            DropIndex("dbo.invoices", new[] { "creator_id" });
            DropColumn("dbo.invoices", "creator_id");
        }
    }
}
