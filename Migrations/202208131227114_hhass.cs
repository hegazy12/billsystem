namespace sara_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class hhass : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.approvers", "invoice_key", c => c.String());
            AddColumn("dbo.approvers", "user_key", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.approvers", "user_key");
            DropColumn("dbo.approvers", "invoice_key");
        }
    }
}
