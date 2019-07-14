namespace Lawyer.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddUsers : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.Asks", new[] { "User_Id" });
            RenameColumn(table: "dbo.Asks", name: "User_Id", newName: "Author_Id");
            AlterColumn("dbo.Asks", "Author_Id", c => c.String(nullable: false, maxLength: 128));
            AlterColumn("dbo.AspNetUsers", "RegisterDate", c => c.DateTime(nullable: false, storeType: "date"));
            CreateIndex("dbo.Asks", "Author_Id");
        }
        
        public override void Down()
        {
            DropIndex("dbo.Asks", new[] { "Author_Id" });
            AlterColumn("dbo.AspNetUsers", "RegisterDate", c => c.DateTime(nullable: false));
            AlterColumn("dbo.Asks", "Author_Id", c => c.String(maxLength: 128));
            RenameColumn(table: "dbo.Asks", name: "Author_Id", newName: "User_Id");
            CreateIndex("dbo.Asks", "User_Id");
        }
    }
}
