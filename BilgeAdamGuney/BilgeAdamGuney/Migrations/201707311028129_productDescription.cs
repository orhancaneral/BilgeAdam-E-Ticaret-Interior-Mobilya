namespace BilgeAdamGuney.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productDescription : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "Description", c => c.String(nullable: false, maxLength: 1500));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "Description");
        }
    }
}
