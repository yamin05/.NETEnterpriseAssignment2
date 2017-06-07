namespace Assignment2.Migrations.ENET
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class InitialCreate : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Clients",
                c => new
                    {
                        ClientId = c.Int(nullable: false, identity: true),
                        ClientName = c.String(),
                        ClientLocation = c.String(),
                        ClientDistrict = c.String(),
                        CreatedByUserId = c.String(maxLength: 128),
                        CreateDate = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .Index(t => t.CreatedByUserId);
            
            CreateTable(
                "dbo.Users",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        MaximumHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        MaximumCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        District = c.String(),
                    })
                .PrimaryKey(t => t.UserId);
            
            CreateTable(
                "dbo.Interventions",
                c => new
                    {
                        InterventionId = c.Int(nullable: false, identity: true),
                        CreatedByUserId = c.String(maxLength: 128),
                        ApprovedByUserId = c.String(maxLength: 128),
                        LastUpdatedByUserId = c.String(maxLength: 128),
                        InterventionTypeId = c.Int(nullable: false),
                        ClientId = c.Int(nullable: false),
                        InterventionCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InterventionHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateDate = c.DateTime(nullable: false),
                        Status = c.String(),
                        Condition = c.Int(),
                        ModifyDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.InterventionId)
                .ForeignKey("dbo.Users", t => t.ApprovedByUserId)
                .ForeignKey("dbo.Clients", t => t.ClientId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.InterventionTypes", t => t.InterventionTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.LastUpdatedByUserId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ApprovedByUserId)
                .Index(t => t.LastUpdatedByUserId)
                .Index(t => t.InterventionTypeId)
                .Index(t => t.ClientId);
            
            CreateTable(
                "dbo.InterventionTypes",
                c => new
                    {
                        InterventionTypeId = c.Int(nullable: false, identity: true),
                        InterventionTypeName = c.String(),
                        InterventionTypeHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InterventionTypeCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                    })
                .PrimaryKey(t => t.InterventionTypeId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Interventions", "LastUpdatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Interventions", "InterventionTypeId", "dbo.InterventionTypes");
            DropForeignKey("dbo.Interventions", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Interventions", "ClientId", "dbo.Clients");
            DropForeignKey("dbo.Interventions", "ApprovedByUserId", "dbo.Users");
            DropForeignKey("dbo.Clients", "CreatedByUserId", "dbo.Users");
            DropIndex("dbo.Interventions", new[] { "ClientId" });
            DropIndex("dbo.Interventions", new[] { "InterventionTypeId" });
            DropIndex("dbo.Interventions", new[] { "LastUpdatedByUserId" });
            DropIndex("dbo.Interventions", new[] { "ApprovedByUserId" });
            DropIndex("dbo.Interventions", new[] { "CreatedByUserId" });
            DropIndex("dbo.Clients", new[] { "CreatedByUserId" });
            DropTable("dbo.InterventionTypes");
            DropTable("dbo.Interventions");
            DropTable("dbo.Users");
            DropTable("dbo.Clients");
        }
    }
}
