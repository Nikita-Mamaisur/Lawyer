namespace Lawyer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class IsAnswered : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Answers", "Confirmation", c => c.Boolean(nullable: false));
            AddColumn("dbo.Asks", "IsAnswered", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Asks", "IsAnswered");
            DropColumn("dbo.Answers", "Confirmation");
        }
    }
}
