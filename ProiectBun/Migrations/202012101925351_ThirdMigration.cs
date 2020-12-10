namespace ProiectBun.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ThirdMigration : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Files",
                c => new
                    {
                        FileId = c.Int(nullable: false, identity: false),
                        FilePath = c.String(),
                        FileName = c.String(),
                        Extension = c.String(),
                    })
                .PrimaryKey(t => t.FileId);
            
            AddColumn("dbo.Products", "FileId", c => c.Int(nullable: true));
            CreateIndex("dbo.Products", "FileId");
            AddForeignKey("dbo.Products", "FileId", "dbo.Files", "FileId", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Products", "FileId", "dbo.Files");
            DropIndex("dbo.Products", new[] { "FileId" });
            DropColumn("dbo.Products", "FileId");
            DropTable("dbo.Files");
        }
    }
}
