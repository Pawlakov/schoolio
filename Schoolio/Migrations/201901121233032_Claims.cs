namespace Schoolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Claims : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ClaimGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Claims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ClaimType = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ClaimGroupClaims",
                c => new
                    {
                        ClaimGroup_Id = c.Int(nullable: false),
                        Claim_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClaimGroup_Id, t.Claim_Id })
                .ForeignKey("dbo.ClaimGroups", t => t.ClaimGroup_Id, cascadeDelete: true)
                .ForeignKey("dbo.Claims", t => t.Claim_Id, cascadeDelete: true)
                .Index(t => t.ClaimGroup_Id)
                .Index(t => t.Claim_Id);
            
            AddColumn("dbo.AspNetUsers", "Group_Id", c => c.Int());
            CreateIndex("dbo.AspNetUsers", "Group_Id");
            AddForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.ClaimGroups", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.ClaimGroups");
            DropForeignKey("dbo.ClaimGroupClaims", "Claim_Id", "dbo.Claims");
            DropForeignKey("dbo.ClaimGroupClaims", "ClaimGroup_Id", "dbo.ClaimGroups");
            DropIndex("dbo.ClaimGroupClaims", new[] { "Claim_Id" });
            DropIndex("dbo.ClaimGroupClaims", new[] { "ClaimGroup_Id" });
            DropIndex("dbo.AspNetUsers", new[] { "Group_Id" });
            DropColumn("dbo.AspNetUsers", "Group_Id");
            DropTable("dbo.ClaimGroupClaims");
            DropTable("dbo.Claims");
            DropTable("dbo.ClaimGroups");
        }
    }
}
