namespace PassionProjectFlowerShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flower6 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Flowers", "BouquetId", "dbo.Bouquets");
            DropForeignKey("dbo.Flowers", "OccasionId", "dbo.Occasions");
            DropIndex("dbo.Flowers", new[] { "BouquetId" });
            DropIndex("dbo.Flowers", new[] { "OccasionId" });
            RenameColumn(table: "dbo.Flowers", name: "BouquetId", newName: "Bouquet_BouquetId");
            RenameColumn(table: "dbo.Flowers", name: "OccasionId", newName: "Occasion_OccasionId");
            AlterColumn("dbo.Flowers", "Bouquet_BouquetId", c => c.Int());
            AlterColumn("dbo.Flowers", "Occasion_OccasionId", c => c.Int());
            CreateIndex("dbo.Flowers", "Bouquet_BouquetId");
            CreateIndex("dbo.Flowers", "Occasion_OccasionId");
            AddForeignKey("dbo.Flowers", "Bouquet_BouquetId", "dbo.Bouquets", "BouquetId");
            AddForeignKey("dbo.Flowers", "Occasion_OccasionId", "dbo.Occasions", "OccasionId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flowers", "Occasion_OccasionId", "dbo.Occasions");
            DropForeignKey("dbo.Flowers", "Bouquet_BouquetId", "dbo.Bouquets");
            DropIndex("dbo.Flowers", new[] { "Occasion_OccasionId" });
            DropIndex("dbo.Flowers", new[] { "Bouquet_BouquetId" });
            AlterColumn("dbo.Flowers", "Occasion_OccasionId", c => c.Int(nullable: false));
            AlterColumn("dbo.Flowers", "Bouquet_BouquetId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Flowers", name: "Occasion_OccasionId", newName: "OccasionId");
            RenameColumn(table: "dbo.Flowers", name: "Bouquet_BouquetId", newName: "BouquetId");
            CreateIndex("dbo.Flowers", "OccasionId");
            CreateIndex("dbo.Flowers", "BouquetId");
            AddForeignKey("dbo.Flowers", "OccasionId", "dbo.Occasions", "OccasionId", cascadeDelete: true);
            AddForeignKey("dbo.Flowers", "BouquetId", "dbo.Bouquets", "BouquetId", cascadeDelete: true);
        }
    }
}
