namespace PassionProjectFlowerShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flower2 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Flowers", "BouquetId", "dbo.Bouquets");
            DropIndex("dbo.Flowers", new[] { "BouquetId" });
            RenameColumn(table: "dbo.Flowers", name: "BouquetId", newName: "Bouquet_BouquetId");
            AlterColumn("dbo.Flowers", "Bouquet_BouquetId", c => c.Int());
            CreateIndex("dbo.Flowers", "Bouquet_BouquetId");
            AddForeignKey("dbo.Flowers", "Bouquet_BouquetId", "dbo.Bouquets", "BouquetId");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flowers", "Bouquet_BouquetId", "dbo.Bouquets");
            DropIndex("dbo.Flowers", new[] { "Bouquet_BouquetId" });
            AlterColumn("dbo.Flowers", "Bouquet_BouquetId", c => c.Int(nullable: false));
            RenameColumn(table: "dbo.Flowers", name: "Bouquet_BouquetId", newName: "BouquetId");
            CreateIndex("dbo.Flowers", "BouquetId");
            AddForeignKey("dbo.Flowers", "BouquetId", "dbo.Bouquets", "BouquetId", cascadeDelete: true);
        }
    }
}
