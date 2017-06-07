namespace Assignment2.Migrations.ENET
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Comment_Intervention : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Interventions", "Comments", c => c.String());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Interventions", "Comments");
        }
    }
}
