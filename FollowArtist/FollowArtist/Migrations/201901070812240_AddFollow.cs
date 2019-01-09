namespace FollowArtist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollow : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Follows",
                c => new
                    {
                        FollowerId = c.String(nullable: false, maxLength: 128),
                        FolloweeId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.FollowerId, t.FolloweeId })
                .ForeignKey("dbo.AspNetUsers", t => t.FolloweeId)
                .ForeignKey("dbo.AspNetUsers", t => t.FollowerId)
                .Index(t => t.FollowerId)
                .Index(t => t.FolloweeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Follows", "FollowerId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Follows", "FolloweeId", "dbo.AspNetUsers");
            DropIndex("dbo.Follows", new[] { "FolloweeId" });
            DropIndex("dbo.Follows", new[] { "FollowerId" });
            DropTable("dbo.Follows");
        }
    }
}
