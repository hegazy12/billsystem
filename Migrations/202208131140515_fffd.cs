namespace sara_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class fffd : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.approvers", name: "user_key_id", newName: "user_id");
            RenameIndex(table: "dbo.approvers", name: "IX_user_key_id", newName: "IX_user_id");
            DropColumn("dbo.approvers", "invoice_key");
            DropColumn("dbo.approvers", "user");
        }
        
        public override void Down()
        {
            AddColumn("dbo.approvers", "user", c => c.Int(nullable: false));
            AddColumn("dbo.approvers", "invoice_key", c => c.String());
            RenameIndex(table: "dbo.approvers", name: "IX_user_id", newName: "IX_user_key_id");
            RenameColumn(table: "dbo.approvers", name: "user_id", newName: "user_key_id");
        }
    }
}
