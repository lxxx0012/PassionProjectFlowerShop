namespace PassionProjectFlowerShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flower8 : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Bouquets", "BouquetPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
            AlterColumn("dbo.Flowers", "FlowerPrice", c => c.Decimal(nullable: false, precision: 18, scale: 2));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.Flowers", "FlowerPrice", c => c.Int(nullable: false));
            AlterColumn("dbo.Bouquets", "BouquetPrice", c => c.Int(nullable: false));
        }
    }
}
