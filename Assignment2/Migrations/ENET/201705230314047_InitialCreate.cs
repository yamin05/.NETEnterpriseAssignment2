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
                        Intervention_InterventionId = c.Int(),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.Interventions", t => t.Intervention_InterventionId)
                .Index(t => t.Intervention_InterventionId);
            
            CreateTable(
                "dbo.Interventions",
                c => new
                    {
                        InterventionId = c.Int(nullable: false, identity: true),
                        CreatedByUserId = c.String(maxLength: 128),
                        ApprovedByUserId = c.String(maxLength: 128),
                        InterventionTypeId = c.Int(nullable: false),
                        InterventionCost = c.Decimal(nullable: false, precision: 18, scale: 2),
                        InterventionHours = c.Decimal(nullable: false, precision: 18, scale: 2),
                        CreateDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        Condition = c.Int(),
                        ModifyDate = c.DateTime(),
                        ClientId_ClientId = c.Int(),
                        User_UserId = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.InterventionId)
                .ForeignKey("dbo.Users", t => t.ApprovedByUserId)
                .ForeignKey("dbo.Clients", t => t.ClientId_ClientId)
                .ForeignKey("dbo.Users", t => t.CreatedByUserId)
                .ForeignKey("dbo.InterventionTypes", t => t.InterventionTypeId, cascadeDelete: true)
                .ForeignKey("dbo.Users", t => t.User_UserId)
                .Index(t => t.CreatedByUserId)
                .Index(t => t.ApprovedByUserId)
                .Index(t => t.InterventionTypeId)
                .Index(t => t.ClientId_ClientId)
                .Index(t => t.User_UserId);
            
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
            DropForeignKey("dbo.Clients", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Interventions", "User_UserId", "dbo.Users");
            DropForeignKey("dbo.Users", "Intervention_InterventionId", "dbo.Interventions");
            DropForeignKey("dbo.Interventions", "InterventionTypeId", "dbo.InterventionTypes");
            DropForeignKey("dbo.Interventions", "CreatedByUserId", "dbo.Users");
            DropForeignKey("dbo.Interventions", "ClientId_ClientId", "dbo.Clients");
            DropForeignKey("dbo.Interventions", "ApprovedByUserId", "dbo.Users");
            DropIndex("dbo.Interventions", new[] { "User_UserId" });
            DropIndex("dbo.Interventions", new[] { "ClientId_ClientId" });
            DropIndex("dbo.Interventions", new[] { "InterventionTypeId" });
            DropIndex("dbo.Interventions", new[] { "ApprovedByUserId" });
            DropIndex("dbo.Interventions", new[] { "CreatedByUserId" });
            DropIndex("dbo.Users", new[] { "Intervention_InterventionId" });
            DropIndex("dbo.Clients", new[] { "CreatedByUserId" });
            DropTable("dbo.InterventionTypes");
            DropTable("dbo.Interventions");
            DropTable("dbo.Users");
            DropTable("dbo.Clients");
        }
    }
}
