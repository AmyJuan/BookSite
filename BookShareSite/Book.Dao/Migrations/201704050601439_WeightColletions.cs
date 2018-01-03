namespace Book.Dao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class WeightColletions : DbMigration
    {
        public override void Up()
        {
            //CreateTable(
            //    "dbo.Book",
            //    c => new
            //        {
            //            ID = c.String(nullable: false, maxLength: 128),
            //            Title = c.String(maxLength: 127),
            //            OriginTitle = c.String(maxLength: 127),
            //            Summary = c.String(),
            //            Image = c.String(maxLength: 127),
            //            Authors = c.String(),
            //            Translators = c.String(),
            //            Create = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            //CreateTable(
            //    "dbo.Collection",
            //    c => new
            //        {
            //            ID = c.String(nullable: false, maxLength: 128),
            //            BookId = c.String(nullable: false, maxLength: 128),
            //            UserId = c.String(nullable: false, maxLength: 128),
            //            Status = c.Int(nullable: false),
            //            Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
            //            Weight = c.Int(nullable: false),
            //        })
            //    .PrimaryKey(t => t.ID)
            //    .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
            //    .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
            //    .Index(t => t.BookId)
            //    .Index(t => t.UserId);
            
            //CreateTable(
            //    "dbo.User",
            //    c => new
            //        {
            //            ID = c.String(nullable: false, maxLength: 128),
            //            Uid = c.String(maxLength: 127),
            //            Name = c.String(maxLength: 127),
            //            Password = c.String(maxLength: 127),
            //            Alt = c.String(maxLength: 127),
            //            Image = c.String(maxLength: 127),
            //            Create = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
            //        })
            //    .PrimaryKey(t => t.ID);
            
            CreateTable(
                "dbo.Review",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        BookId = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                        Updated = c.DateTime(nullable: false, precision: 7, storeType: "datetime2"),
                        Content = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Book", t => t.BookId, cascadeDelete: true)
                .ForeignKey("dbo.User", t => t.UserId, cascadeDelete: true)
                .Index(t => t.BookId)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.Weight",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        WeightType = c.Int(nullable: false),
                        Value = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Review", "UserId", "dbo.User");
            DropForeignKey("dbo.Review", "BookId", "dbo.Book");
            //DropForeignKey("dbo.Collection", "UserId", "dbo.User");
            //DropForeignKey("dbo.Collection", "BookId", "dbo.Book");
            DropIndex("dbo.Review", new[] { "UserId" });
            DropIndex("dbo.Review", new[] { "BookId" });
            //DropIndex("dbo.Collection", new[] { "UserId" });
            //DropIndex("dbo.Collection", new[] { "BookId" });
            DropTable("dbo.Weight");
            DropTable("dbo.Review");
            //DropTable("dbo.User");
            //DropTable("dbo.Collection");
            //DropTable("dbo.Book");
        }
    }
}
