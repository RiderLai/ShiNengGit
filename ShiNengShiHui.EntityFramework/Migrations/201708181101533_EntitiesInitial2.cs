namespace ShiNengShiHui.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesInitial2 : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.testStudents", "Group", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.testStudents", "Group");
        }
    }
}
