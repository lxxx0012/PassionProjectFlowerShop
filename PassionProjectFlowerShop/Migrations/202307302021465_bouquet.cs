namespace PassionProjectFlowerShop.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class bouquet : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Bouquets",
                c => new
                    {
                        BouquetId = c.Int(nullable: false, identity: true),
                        BouquetName = c.String(),
                        BouquetPrice = c.Int(nullable: false),
                        BouquetDescription = c.String(),
                        BouquetPic = c.String(),
                    })
                .PrimaryKey(t => t.BouquetId);
            
            CreateTable(
                "dbo.Occasions",
                c => new
                    {
                        OccasionId = c.Int(nullable: false, identity: true),
                        OccasionName = c.String(),
                        OccasionDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.OccasionId);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Occasions");
            DropTable("dbo.Bouquets");
        }
    }
}
