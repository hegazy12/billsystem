namespace sara_system.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ggd : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.approvers", "decision", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.approvers", "decision");
        }
    }
}
