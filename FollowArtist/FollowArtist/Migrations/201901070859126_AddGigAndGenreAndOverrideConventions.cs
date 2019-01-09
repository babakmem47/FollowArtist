namespace FollowArtist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddGigAndGenreAndOverrideConventions : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Genres",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 40),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Gigs",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArtistId = c.String(nullable: false, maxLength: 128),
                        DateTime = c.DateTime(nullable: false),
                        Venue = c.String(nullable: false, maxLength: 200),
                        GenreId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.ArtistId)
                .ForeignKey("dbo.Genres", t => t.GenreId)
                .Index(t => t.ArtistId)
                .Index(t => t.GenreId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Gigs", "GenreId", "dbo.Genres");
            DropForeignKey("dbo.Gigs", "ArtistId", "dbo.AspNetUsers");
            DropIndex("dbo.Gigs", new[] { "GenreId" });
            DropIndex("dbo.Gigs", new[] { "ArtistId" });
            DropTable("dbo.Gigs");
            DropTable("dbo.Genres");
        }
    }
}
