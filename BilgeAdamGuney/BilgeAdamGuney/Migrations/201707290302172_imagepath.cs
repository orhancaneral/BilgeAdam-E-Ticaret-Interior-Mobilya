namespace BilgeAdamGuney.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class imagepath : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Products", "ImagePath", c => c.String(maxLength: 4000));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Products", "ImagePath");
        }
    }
}
