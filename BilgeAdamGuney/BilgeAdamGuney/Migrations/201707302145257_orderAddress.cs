namespace BilgeAdamGuney.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class orderAddress : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Orders", "OrderAddressId", c => c.Int(nullable: false));
            CreateIndex("dbo.Orders", "OrderAddressId");
            AddForeignKey("dbo.Orders", "OrderAddressId", "dbo.Addresses", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Orders", "OrderAddressId", "dbo.Addresses");
            DropIndex("dbo.Orders", new[] { "OrderAddressId" });
            DropColumn("dbo.Orders", "OrderAddressId");
        }
    }
}
