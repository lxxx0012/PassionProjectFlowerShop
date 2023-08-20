namespace PassionProjectFlowerShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flowerbouquetoccasion : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.FlowerBouquets", "Flower_FlowerId", "dbo.Flowers");
            DropForeignKey("dbo.FlowerBouquets", "Bouquet_BouquetId", "dbo.Bouquets");
            DropForeignKey("dbo.OccasionFlowers", "Occasion_OccasionId", "dbo.Occasions");
            DropForeignKey("dbo.OccasionFlowers", "Flower_FlowerId", "dbo.Flowers");
            DropIndex("dbo.FlowerBouquets", new[] { "Flower_FlowerId" });
            DropIndex("dbo.FlowerBouquets", new[] { "Bouquet_BouquetId" });
            DropIndex("dbo.OccasionFlowers", new[] { "Occasion_OccasionId" });
            DropIndex("dbo.OccasionFlowers", new[] { "Flower_FlowerId" });
            AddColumn("dbo.Flowers", "BouquetId", c => c.Int(nullable: false));
            AddColumn("dbo.Flowers", "OccasionId", c => c.Int(nullable: false));
            CreateIndex("dbo.Flowers", "BouquetId");
            CreateIndex("dbo.Flowers", "OccasionId");
            AddForeignKey("dbo.Flowers", "BouquetId", "dbo.Bouquets", "BouquetId", cascadeDelete: true);
            AddForeignKey("dbo.Flowers", "OccasionId", "dbo.Occasions", "OccasionId", cascadeDelete: true);
            DropTable("dbo.FlowerBouquets");
            DropTable("dbo.OccasionFlowers");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.OccasionFlowers",
                c => new
                    {
                        Occasion_OccasionId = c.Int(nullable: false),
                        Flower_FlowerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Occasion_OccasionId, t.Flower_FlowerId });
            
            CreateTable(
                "dbo.FlowerBouquets",
                c => new
                    {
                        Flower_FlowerId = c.Int(nullable: false),
                        Bouquet_BouquetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Flower_FlowerId, t.Bouquet_BouquetId });
            
            DropForeignKey("dbo.Flowers", "OccasionId", "dbo.Occasions");
            DropForeignKey("dbo.Flowers", "BouquetId", "dbo.Bouquets");
            DropIndex("dbo.Flowers", new[] { "OccasionId" });
            DropIndex("dbo.Flowers", new[] { "BouquetId" });
            DropColumn("dbo.Flowers", "OccasionId");
            DropColumn("dbo.Flowers", "BouquetId");
            CreateIndex("dbo.OccasionFlowers", "Flower_FlowerId");
            CreateIndex("dbo.OccasionFlowers", "Occasion_OccasionId");
            CreateIndex("dbo.FlowerBouquets", "Bouquet_BouquetId");
            CreateIndex("dbo.FlowerBouquets", "Flower_FlowerId");
            AddForeignKey("dbo.OccasionFlowers", "Flower_FlowerId", "dbo.Flowers", "FlowerId", cascadeDelete: true);
            AddForeignKey("dbo.OccasionFlowers", "Occasion_OccasionId", "dbo.Occasions", "OccasionId", cascadeDelete: true);
            AddForeignKey("dbo.FlowerBouquets", "Bouquet_BouquetId", "dbo.Bouquets", "BouquetId", cascadeDelete: true);
            AddForeignKey("dbo.FlowerBouquets", "Flower_FlowerId", "dbo.Flowers", "FlowerId", cascadeDelete: true);
        }
    }
}
