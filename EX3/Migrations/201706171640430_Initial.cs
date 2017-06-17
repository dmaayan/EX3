namespace EX3.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Statistics",
                c => new
                    {
                        UserName = c.String(nullable: false, maxLength: 128),
                        Wins = c.Int(nullable: false),
                        Losses = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.UserName);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        userName = c.String(nullable: false, maxLength: 128),
                        password = c.String(nullable: false),
                        email = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.userName)
                .ForeignKey("dbo.Statistics", t => t.userName)
                .Index(t => t.userName);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Users", "userName", "dbo.Statistics");
            DropIndex("dbo.Users", new[] { "userName" });
            DropTable("dbo.Users");
            DropTable("dbo.Statistics");
        }
    }
}
