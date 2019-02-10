namespace Schoolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangedRestricitons : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.ClaimGroupClaims", "ClaimGroup_Id", "dbo.ClaimGroups");
            DropForeignKey("dbo.ClaimGroupClaims", "Claim_Id", "dbo.Claims");
            DropForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.ClaimGroups");
            DropIndex("dbo.AspNetUsers", new[] { "Group_Id" });
            DropIndex("dbo.ClaimGroupClaims", new[] { "ClaimGroup_Id" });
            DropIndex("dbo.ClaimGroupClaims", new[] { "Claim_Id" });
            CreateTable(
                "dbo.ActorTypes",
                c => new
                    {
                        Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ApplicationUserActorTypes",
                c => new
                    {
                        ApplicationUser_Id = c.String(nullable: false, maxLength: 128),
                        ActorType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ApplicationUser_Id, t.ActorType_Id })
                .ForeignKey("dbo.AspNetUsers", t => t.ApplicationUser_Id, cascadeDelete: true)
                .ForeignKey("dbo.ActorTypes", t => t.ActorType_Id, cascadeDelete: true)
                .Index(t => t.ApplicationUser_Id)
                .Index(t => t.ActorType_Id);
            
            DropColumn("dbo.AspNetUsers", "Group_Id");
            DropTable("dbo.ClaimGroups");
            DropTable("dbo.Claims");
            DropTable("dbo.ClaimGroupClaims");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.ClaimGroupClaims",
                c => new
                    {
                        ClaimGroup_Id = c.Int(nullable: false),
                        Claim_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ClaimGroup_Id, t.Claim_Id });
            
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
                "dbo.ClaimGroups",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.AspNetUsers", "Group_Id", c => c.Int());
            DropForeignKey("dbo.ApplicationUserActorTypes", "ActorType_Id", "dbo.ActorTypes");
            DropForeignKey("dbo.ApplicationUserActorTypes", "ApplicationUser_Id", "dbo.AspNetUsers");
            DropIndex("dbo.ApplicationUserActorTypes", new[] { "ActorType_Id" });
            DropIndex("dbo.ApplicationUserActorTypes", new[] { "ApplicationUser_Id" });
            DropTable("dbo.ApplicationUserActorTypes");
            DropTable("dbo.ActorTypes");
            CreateIndex("dbo.ClaimGroupClaims", "Claim_Id");
            CreateIndex("dbo.ClaimGroupClaims", "ClaimGroup_Id");
            CreateIndex("dbo.AspNetUsers", "Group_Id");
            AddForeignKey("dbo.AspNetUsers", "Group_Id", "dbo.ClaimGroups", "Id");
            AddForeignKey("dbo.ClaimGroupClaims", "Claim_Id", "dbo.Claims", "Id", cascadeDelete: true);
            AddForeignKey("dbo.ClaimGroupClaims", "ClaimGroup_Id", "dbo.ClaimGroups", "Id", cascadeDelete: true);
        }
    }
}
