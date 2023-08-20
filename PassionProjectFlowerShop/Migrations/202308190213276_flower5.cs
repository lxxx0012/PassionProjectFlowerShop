namespace PassionProjectFlowerShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class flower5 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Flowers", "Bouquet_BouquetId", "dbo.Bouquets");
            DropIndex("dbo.Flowers", new[] { "Bouquet_BouquetId" });
            RenameColumn(table: "dbo.Flowers", name: "Bouquet_BouquetId", newName: "BouquetId");
            AlterColumn("dbo.Flowers", "BouquetId", c => c.Int(nullable: false));
            CreateIndex("dbo.Flowers", "BouquetId");
            AddForeignKey("dbo.Flowers", "BouquetId", "dbo.Bouquets", "BouquetId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Flowers", "BouquetId", "dbo.Bouquets");
            DropIndex("dbo.Flowers", new[] { "BouquetId" });
            AlterColumn("dbo.Flowers", "BouquetId", c => c.Int());
            RenameColumn(table: "dbo.Flowers", name: "BouquetId", newName: "Bouquet_BouquetId");
            CreateIndex("dbo.Flowers", "Bouquet_BouquetId");
            AddForeignKey("dbo.Flowers", "Bouquet_BouquetId", "dbo.Bouquets", "BouquetId");
        }
    }
}
