namespace FollowArtist.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddFollow1 : DbMigration
    {
        public override void Up()
        {
            RenameTable(name: "dbo.Follows", newName: "Followings");
        }
        
        public override void Down()
        {
            RenameTable(name: "dbo.Followings", newName: "Follows");
        }
    }
}
