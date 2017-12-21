namespace ShiNengShiHui.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesInitial8 : DbMigration
    {
        public override void Up()
        {
            DropTable("dbo.WeekGrades");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.WeekGrades",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        GradeDataJson = c.String(nullable: false),
                        DateJson = c.String(nullable: false),
                        SID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
    }
}
