namespace ShiNengShiHui.Migrations
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Infrastructure.Annotations;
    using System.Data.Entity.Migrations;
    
    public partial class EntitiesInitial5 : DbMigration
    {
        public override void Up()
        {
            AlterTableAnnotations(
                "dbo.testPrizes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        DateJosn = c.String(nullable: false),
                        PrizeItemId = c.Guid(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Prize_SoftDelete",
                        new AnnotationValues(oldValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition", newValue: null)
                    },
                });
            
            DropColumn("dbo.testPrizes", "IsDeleted");
            DropColumn("dbo.testPrizes", "DeleterUserId");
            DropColumn("dbo.testPrizes", "DeletionTime");
            DropColumn("dbo.testPrizes", "LastModificationTime");
            DropColumn("dbo.testPrizes", "LastModifierUserId");
            DropColumn("dbo.testPrizes", "CreationTime");
            DropColumn("dbo.testPrizes", "CreatorUserId");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Prizes", "CreatorUserId", c => c.Long());
            AddColumn("dbo.Prizes", "CreationTime", c => c.DateTime(nullable: false));
            AddColumn("dbo.Prizes", "LastModifierUserId", c => c.Long());
            AddColumn("dbo.Prizes", "LastModificationTime", c => c.DateTime());
            AddColumn("dbo.Prizes", "DeletionTime", c => c.DateTime());
            AddColumn("dbo.Prizes", "DeleterUserId", c => c.Long());
            AddColumn("dbo.Prizes", "IsDeleted", c => c.Boolean(nullable: false));
            AlterTableAnnotations(
                "dbo.Prizes",
                c => new
                    {
                        Id = c.Long(nullable: false, identity: true),
                        StudentId = c.Int(nullable: false),
                        DateJosn = c.String(nullable: false),
                        PrizeItemId = c.Guid(nullable: false),
                    },
                annotations: new Dictionary<string, AnnotationValues>
                {
                    { 
                        "DynamicFilter_Prize_SoftDelete",
                        new AnnotationValues(oldValue: null, newValue: "EntityFramework.DynamicFilters.DynamicFilterDefinition")
                    },
                });
            
        }
    }
}
