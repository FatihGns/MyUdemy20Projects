namespace Project4_EntityFramewoerCodeFirstMovie.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class UpdateMoviesTable : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Categories", "CategoryName", c => c.String());
            DropColumn("dbo.Categories", "CategoeyName");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Categories", "CategoeyName", c => c.String());
            DropColumn("dbo.Categories", "CategoryName");
        }
    }
}
