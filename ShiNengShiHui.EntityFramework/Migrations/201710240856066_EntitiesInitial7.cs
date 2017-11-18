namespace ShiNengShiHui.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesInitial7 : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.GroupWeekGrades",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        DateJson = c.String(nullable: false),
                        Group = c.Int(nullable: false),
                        IsWell = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
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
        
        public override void Down()
        {
            DropTable("dbo.WeekGrades");
            DropTable("dbo.GroupWeekGrades");
        }
    }
}
