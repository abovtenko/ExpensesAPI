namespace ExpensesAPI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddRelationships : DbMigration
    {
        public override void Up()
        {
            RenameColumn(table: "dbo.Transactions", name: "User_UserID", newName: "UserID");
            RenameIndex(table: "dbo.Transactions", name: "IX_User_UserID", newName: "IX_UserID");
        }
        
        public override void Down()
        {
            RenameIndex(table: "dbo.Transactions", name: "IX_UserID", newName: "IX_User_UserID");
            RenameColumn(table: "dbo.Transactions", name: "UserID", newName: "User_UserID");
        }
    }
}
