namespace BangazonAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedStatusDataType : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Chores", "status", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Chores", "status", c => c.String(nullable: false));
        }
    }
}
