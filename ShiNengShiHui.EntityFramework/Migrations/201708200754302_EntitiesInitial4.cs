namespace ShiNengShiHui.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesInitial4 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Functions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        PID = c.Int(nullable: false),
                        Name = c.String(),
                        Order = c.Int(nullable: false),
                        Action = c.String(),
                        Controller = c.String(),
                        ICon = c.String(),
                        RoleId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Functions");
        }
    }
}
