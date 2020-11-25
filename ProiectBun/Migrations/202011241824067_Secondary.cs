namespace ProiectBun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Secondary : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Reviews", "Product_Id", "dbo.Products");
            DropForeignKey("dbo.Products", "Category_Id", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "Category_Id" });
            DropIndex("dbo.Reviews", new[] { "Product_Id" });
            RenameColumn(table: "dbo.Products", name: "Category_Id", newName: "CategoryId");
            AlterColumn("dbo.Categories", "Name", c => c.String());
            AlterColumn("dbo.Products", "CategoryId", c => c.Int(nullable: false));
            CreateIndex("dbo.Products", "CategoryId");
            AddForeignKey("dbo.Products", "CategoryId", "dbo.Categories", "Id", cascadeDelete: true);
            DropTable("dbo.Reviews");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.Reviews",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(),
                        Rating = c.Int(nullable: false),
                        Product_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropForeignKey("dbo.Products", "CategoryId", "dbo.Categories");
            DropIndex("dbo.Products", new[] { "CategoryId" });
            AlterColumn("dbo.Products", "CategoryId", c => c.Int());
            AlterColumn("dbo.Categories", "Name", c => c.String(nullable: false));
            RenameColumn(table: "dbo.Products", name: "CategoryId", newName: "Category_Id");
            CreateIndex("dbo.Reviews", "Product_Id");
            CreateIndex("dbo.Products", "Category_Id");
            AddForeignKey("dbo.Products", "Category_Id", "dbo.Categories", "Id");
            AddForeignKey("dbo.Reviews", "Product_Id", "dbo.Products", "Id");
        }
    }
}
