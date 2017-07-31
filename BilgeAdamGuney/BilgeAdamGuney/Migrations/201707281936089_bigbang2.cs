namespace BilgeAdamGuney.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bigbang2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "MemberId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "MemberId");
            AddForeignKey("dbo.Orders", "MemberId", "dbo.Members", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "MemberId", "dbo.Members");
            DropIndex("dbo.Orders", new[] { "MemberId" });
            DropColumn("dbo.Orders", "MemberId");
        }
    }
}
