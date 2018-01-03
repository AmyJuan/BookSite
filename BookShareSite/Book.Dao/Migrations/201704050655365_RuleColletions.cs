namespace Book.Dao.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RuleColletions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Rule",
                c => new
                    {
                        ID = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(),
                        MinSup = c.Double(nullable: false),
                        MinConf = c.Double(nullable: false),
                        Delta = c.Double(nullable: false),
                    })
                .PrimaryKey(t => t.ID);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Rule");
        }
    }
}
