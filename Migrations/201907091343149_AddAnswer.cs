namespace Lawyer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddAnswer : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Answers",
                c => new
                    {
                        Id = c.Guid(nullable: false, identity: true),
                        Text = c.String(nullable: false),
                        Date = c.DateTime(nullable: false, precision: 0, storeType: "datetime2"),
                        Ask_Id = c.Guid(nullable: false),
                        Author_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Asks", t => t.Ask_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.Author_Id)
                .Index(t => t.Ask_Id)
                .Index(t => t.Author_Id);
            
            AddColumn("dbo.Asks", "Slug", c => c.String(nullable: false, maxLength: 50, unicode: false));
            AddColumn("dbo.Asks", "Title", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Answers", "Author_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Answers", "Ask_Id", "dbo.Asks");
            DropIndex("dbo.Answers", new[] { "Author_Id" });
            DropIndex("dbo.Answers", new[] { "Ask_Id" });
            DropColumn("dbo.Asks", "Title");
            DropColumn("dbo.Asks", "Slug");
            DropTable("dbo.Answers");
        }
    }
}
