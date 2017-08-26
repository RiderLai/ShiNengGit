namespace ShiNengShiHui.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesInitial6 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Classes", "Display", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Classes", "Display");
        }
    }
}
