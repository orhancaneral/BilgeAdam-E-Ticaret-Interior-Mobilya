namespace BilgeAdamGuney.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class productType2 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.ProductTypes", "Name", c => c.String(nullable: false, maxLength: 100));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.ProductTypes", "Name", c => c.String());
        }
    }
}
