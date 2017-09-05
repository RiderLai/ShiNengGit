namespace ShiNengShiHui.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesInitial3 : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Teachers", "Class_Id", "dbo.Classes");
            DropIndex("dbo.Teachers", new[] { "Class_Id" });
            RenameColumn(table: "dbo.testGrades", name: "Student_Id", newName: "StudentId");
            RenameColumn(table: "dbo.testStudents", name: "Class_Id", newName: "ClassId");
            RenameColumn(table: "dbo.testPrizes", name: "Student_Id", newName: "StudentId");
            RenameColumn(table: "dbo.testPrizes", name: "PrizeItem_Id", newName: "PrizeItemId");
            RenameColumn(table: "dbo.AbpUsers", name: "Teacher_Id", newName: "TeacherId");
            RenameColumn(table: "dbo.Teachers", name: "Class_Id", newName: "ClassId");
            RenameIndex(table: "dbo.testGrades", name: "IX_Student_Id", newName: "IX_StudentId");
            RenameIndex(table: "dbo.testStudents", name: "IX_Class_Id", newName: "IX_ClassId");
            RenameIndex(table: "dbo.testPrizes", name: "IX_Student_Id", newName: "IX_StudentId");
            RenameIndex(table: "dbo.testPrizes", name: "IX_PrizeItem_Id", newName: "IX_PrizeItemId");
            RenameIndex(table: "dbo.AbpUsers", name: "IX_Teacher_Id", newName: "IX_TeacherId");
            AlterColumn("dbo.Teachers", "ClassId", c => c.Int(nullable: false));
            CreateIndex("dbo.Teachers", "ClassId");
            AddForeignKey("dbo.Teachers", "ClassId", "dbo.Classes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Teachers", "ClassId", "dbo.Classes");
            DropIndex("dbo.Teachers", new[] { "ClassId" });
            AlterColumn("dbo.Teachers", "ClassId", c => c.Int());
            RenameIndex(table: "dbo.AbpUsers", name: "IX_TeacherId", newName: "IX_Teacher_Id");
            RenameIndex(table: "dbo.testPrizes", name: "IX_PrizeItemId", newName: "IX_PrizeItem_Id");
            RenameIndex(table: "dbo.testPrizes", name: "IX_StudentId", newName: "IX_Student_Id");
            RenameIndex(table: "dbo.testStudents", name: "IX_ClassId", newName: "IX_Class_Id");
            RenameIndex(table: "dbo.testGrades", name: "IX_StudentId", newName: "IX_Student_Id");
            RenameColumn(table: "dbo.Teachers", name: "ClassId", newName: "Class_Id");
            RenameColumn(table: "dbo.AbpUsers", name: "TeacherId", newName: "Teacher_Id");
            RenameColumn(table: "dbo.testPrizes", name: "PrizeItemId", newName: "PrizeItem_Id");
            RenameColumn(table: "dbo.testPrizes", name: "StudentId", newName: "Student_Id");
            RenameColumn(table: "dbo.testStudents", name: "ClassId", newName: "Class_Id");
            RenameColumn(table: "dbo.testGrades", name: "StudentId", newName: "Student_Id");
            CreateIndex("dbo.Teachers", "Class_Id");
            AddForeignKey("dbo.Teachers", "Class_Id", "dbo.Classes", "Id");
        }
    }
}
