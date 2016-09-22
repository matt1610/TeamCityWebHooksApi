namespace TcWebHooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class branchesmodelagain : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.BranchModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BranchName = c.String(),
                        Device_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Devices", t => t.Device_Id)
                .Index(t => t.Device_Id);
            
            DropColumn("dbo.Devices", "SubscribedBranches");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Devices", "SubscribedBranches", c => c.String());
            DropForeignKey("dbo.BranchModels", "Device_Id", "dbo.Devices");
            DropIndex("dbo.BranchModels", new[] { "Device_Id" });
            DropTable("dbo.BranchModels");
        }
    }
}
