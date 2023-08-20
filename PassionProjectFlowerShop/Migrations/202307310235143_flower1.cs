namespace PassionProjectFlowerShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flower1 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.FlowerBouquets",
                c => new
                    {
                        Flower_FlowerId = c.Int(nullable: false),
                        Bouquet_BouquetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Flower_FlowerId, t.Bouquet_BouquetId })
                .ForeignKey("dbo.Flowers", t => t.Flower_FlowerId, cascadeDelete: true)
                .ForeignKey("dbo.Bouquets", t => t.Bouquet_BouquetId, cascadeDelete: true)
                .Index(t => t.Flower_FlowerId)
                .Index(t => t.Bouquet_BouquetId);
            
            CreateTable(
                "dbo.OccasionBouquets",
                c => new
                    {
                        Occasion_OccasionId = c.Int(nullable: false),
                        Bouquet_BouquetId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Occasion_OccasionId, t.Bouquet_BouquetId })
                .ForeignKey("dbo.Occasions", t => t.Occasion_OccasionId, cascadeDelete: true)
                .ForeignKey("dbo.Bouquets", t => t.Bouquet_BouquetId, cascadeDelete: true)
                .Index(t => t.Occasion_OccasionId)
                .Index(t => t.Bouquet_BouquetId);
            
            CreateTable(
                "dbo.OccasionFlowers",
                c => new
                    {
                        Occasion_OccasionId = c.Int(nullable: false),
                        Flower_FlowerId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Occasion_OccasionId, t.Flower_FlowerId })
                .ForeignKey("dbo.Occasions", t => t.Occasion_OccasionId, cascadeDelete: true)
                .ForeignKey("dbo.Flowers", t => t.Flower_FlowerId, cascadeDelete: true)
                .Index(t => t.Occasion_OccasionId)
                .Index(t => t.Flower_FlowerId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.OccasionFlowers", "Flower_FlowerId", "dbo.Flowers");
            DropForeignKey("dbo.OccasionFlowers", "Occasion_OccasionId", "dbo.Occasions");
            DropForeignKey("dbo.OccasionBouquets", "Bouquet_BouquetId", "dbo.Bouquets");
            DropForeignKey("dbo.OccasionBouquets", "Occasion_OccasionId", "dbo.Occasions");
            DropForeignKey("dbo.FlowerBouquets", "Bouquet_BouquetId", "dbo.Bouquets");
            DropForeignKey("dbo.FlowerBouquets", "Flower_FlowerId", "dbo.Flowers");
            DropIndex("dbo.OccasionFlowers", new[] { "Flower_FlowerId" });
            DropIndex("dbo.OccasionFlowers", new[] { "Occasion_OccasionId" });
            DropIndex("dbo.OccasionBouquets", new[] { "Bouquet_BouquetId" });
            DropIndex("dbo.OccasionBouquets", new[] { "Occasion_OccasionId" });
            DropIndex("dbo.FlowerBouquets", new[] { "Bouquet_BouquetId" });
            DropIndex("dbo.FlowerBouquets", new[] { "Flower_FlowerId" });
            DropTable("dbo.OccasionFlowers");
            DropTable("dbo.OccasionBouquets");
            DropTable("dbo.FlowerBouquets");
        }
    }
}
