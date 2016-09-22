namespace TcWebHooks.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class branchesassimplestring : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.BranchModels", "Device_Id", "dbo.Devices");
            DropIndex("dbo.BranchModels", new[] { "Device_Id" });
            AddColumn("dbo.Devices", "SubscribedBranches", c => c.String());
            DropTable("dbo.BranchModels");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.BranchModels",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        BranchName = c.String(),
                        Device_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            DropColumn("dbo.Devices", "SubscribedBranches");
            CreateIndex("dbo.BranchModels", "Device_Id");
            AddForeignKey("dbo.BranchModels", "Device_Id", "dbo.Devices", "Id");
        }
    }
}
