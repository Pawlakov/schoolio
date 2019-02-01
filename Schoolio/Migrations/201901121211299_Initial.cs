namespace Schoolio.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Classes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ClassType_Id = c.Int(nullable: false),
                        Curator_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassTypes", t => t.ClassType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Teachers", t => t.Curator_Id)
                .Index(t => t.ClassType_Id)
                .Index(t => t.Curator_Id);
            
            CreateTable(
                "dbo.ClassTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.Schedules",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ClassType_Id = c.Int(nullable: false),
                        Class_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassTypes", t => t.ClassType_Id, cascadeDelete: true)
                .ForeignKey("dbo.Classes", t => t.Class_Id)
                .Index(t => t.ClassType_Id)
                .Index(t => t.Class_Id);
            
            CreateTable(
                "dbo.SchedulePositions",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Time = c.String(nullable: false),
                        Duration = c.Int(nullable: false),
                        Room = c.Int(nullable: false),
                        Schedule_Id = c.Int(nullable: false),
                        Subject_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Schedules", t => t.Schedule_Id, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .Index(t => t.Schedule_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.Subjects",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        Teacher_Id = c.Int(),
                        SubjectType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id)
                .ForeignKey("dbo.SubjectTypes", t => t.SubjectType_Id, cascadeDelete: true)
                .Index(t => t.Teacher_Id)
                .Index(t => t.SubjectType_Id);
            
            CreateTable(
                "dbo.Notes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Date = c.DateTime(nullable: false),
                        Comment = c.String(),
                        Value = c.Single(nullable: false),
                        Student_Id = c.Int(nullable: false),
                        Subject_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .ForeignKey("dbo.Subjects", t => t.Subject_Id, cascadeDelete: true)
                .Index(t => t.Student_Id)
                .Index(t => t.Subject_Id);
            
            CreateTable(
                "dbo.Students",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Class_Id = c.Int(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Classes", t => t.Class_Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.Class_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.Parents",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        Address = c.String(),
                        PhoneNumber = c.String(),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.AspNetUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Email = c.String(maxLength: 256),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.UserName, unique: true, name: "UserNameIndex");
            
            CreateTable(
                "dbo.Teachers",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        FirstName = c.String(nullable: false),
                        LastName = c.String(nullable: false),
                        User_Id = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.User_Id)
                .Index(t => t.User_Id);
            
            CreateTable(
                "dbo.SubjectTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false),
                        ClassType_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.ClassTypes", t => t.ClassType_Id)
                .Index(t => t.ClassType_Id);
            
            CreateTable(
                "dbo.AspNetUserClaims",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        UserId = c.String(nullable: false, maxLength: 128),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserLogins",
                c => new
                    {
                        LoginProvider = c.String(nullable: false, maxLength: 128),
                        ProviderKey = c.String(nullable: false, maxLength: 128),
                        UserId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.LoginProvider, t.ProviderKey, t.UserId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .Index(t => t.UserId);
            
            CreateTable(
                "dbo.AspNetUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AspNetUsers", t => t.UserId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetRoles", t => t.RoleId, cascadeDelete: true)
                .Index(t => t.UserId)
                .Index(t => t.RoleId);
            
            CreateTable(
                "dbo.AspNetRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(nullable: false, maxLength: 256),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "RoleNameIndex");
            
            CreateTable(
                "dbo.ParentStudents",
                c => new
                    {
                        Parent_Id = c.Int(nullable: false),
                        Student_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Parent_Id, t.Student_Id })
                .ForeignKey("dbo.Parents", t => t.Parent_Id, cascadeDelete: true)
                .ForeignKey("dbo.Students", t => t.Student_Id, cascadeDelete: true)
                .Index(t => t.Parent_Id)
                .Index(t => t.Student_Id);
            
            CreateTable(
                "dbo.TeacherSubjectTypes",
                c => new
                    {
                        Teacher_Id = c.Int(nullable: false),
                        SubjectType_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Teacher_Id, t.SubjectType_Id })
                .ForeignKey("dbo.Teachers", t => t.Teacher_Id, cascadeDelete: true)
                .ForeignKey("dbo.SubjectTypes", t => t.SubjectType_Id, cascadeDelete: true)
                .Index(t => t.Teacher_Id)
                .Index(t => t.SubjectType_Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AspNetUserRoles", "RoleId", "dbo.AspNetRoles");
            DropForeignKey("dbo.Schedules", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.Classes", "Curator_Id", "dbo.Teachers");
            DropForeignKey("dbo.Classes", "ClassType_Id", "dbo.ClassTypes");
            DropForeignKey("dbo.SubjectTypes", "ClassType_Id", "dbo.ClassTypes");
            DropForeignKey("dbo.Subjects", "SubjectType_Id", "dbo.SubjectTypes");
            DropForeignKey("dbo.SchedulePositions", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.Notes", "Subject_Id", "dbo.Subjects");
            DropForeignKey("dbo.AspNetUserRoles", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserLogins", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AspNetUserClaims", "UserId", "dbo.AspNetUsers");
            DropForeignKey("dbo.Teachers", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.TeacherSubjectTypes", "SubjectType_Id", "dbo.SubjectTypes");
            DropForeignKey("dbo.TeacherSubjectTypes", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Subjects", "Teacher_Id", "dbo.Teachers");
            DropForeignKey("dbo.Students", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.Parents", "User_Id", "dbo.AspNetUsers");
            DropForeignKey("dbo.ParentStudents", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.ParentStudents", "Parent_Id", "dbo.Parents");
            DropForeignKey("dbo.Notes", "Student_Id", "dbo.Students");
            DropForeignKey("dbo.Students", "Class_Id", "dbo.Classes");
            DropForeignKey("dbo.SchedulePositions", "Schedule_Id", "dbo.Schedules");
            DropForeignKey("dbo.Schedules", "ClassType_Id", "dbo.ClassTypes");
            DropIndex("dbo.TeacherSubjectTypes", new[] { "SubjectType_Id" });
            DropIndex("dbo.TeacherSubjectTypes", new[] { "Teacher_Id" });
            DropIndex("dbo.ParentStudents", new[] { "Student_Id" });
            DropIndex("dbo.ParentStudents", new[] { "Parent_Id" });
            DropIndex("dbo.AspNetRoles", "RoleNameIndex");
            DropIndex("dbo.AspNetUserRoles", new[] { "RoleId" });
            DropIndex("dbo.AspNetUserRoles", new[] { "UserId" });
            DropIndex("dbo.AspNetUserLogins", new[] { "UserId" });
            DropIndex("dbo.AspNetUserClaims", new[] { "UserId" });
            DropIndex("dbo.SubjectTypes", new[] { "ClassType_Id" });
            DropIndex("dbo.Teachers", new[] { "User_Id" });
            DropIndex("dbo.AspNetUsers", "UserNameIndex");
            DropIndex("dbo.Parents", new[] { "User_Id" });
            DropIndex("dbo.Students", new[] { "User_Id" });
            DropIndex("dbo.Students", new[] { "Class_Id" });
            DropIndex("dbo.Notes", new[] { "Subject_Id" });
            DropIndex("dbo.Notes", new[] { "Student_Id" });
            DropIndex("dbo.Subjects", new[] { "SubjectType_Id" });
            DropIndex("dbo.Subjects", new[] { "Teacher_Id" });
            DropIndex("dbo.SchedulePositions", new[] { "Subject_Id" });
            DropIndex("dbo.SchedulePositions", new[] { "Schedule_Id" });
            DropIndex("dbo.Schedules", new[] { "Class_Id" });
            DropIndex("dbo.Schedules", new[] { "ClassType_Id" });
            DropIndex("dbo.Classes", new[] { "Curator_Id" });
            DropIndex("dbo.Classes", new[] { "ClassType_Id" });
            DropTable("dbo.TeacherSubjectTypes");
            DropTable("dbo.ParentStudents");
            DropTable("dbo.AspNetRoles");
            DropTable("dbo.AspNetUserRoles");
            DropTable("dbo.AspNetUserLogins");
            DropTable("dbo.AspNetUserClaims");
            DropTable("dbo.SubjectTypes");
            DropTable("dbo.Teachers");
            DropTable("dbo.AspNetUsers");
            DropTable("dbo.Parents");
            DropTable("dbo.Students");
            DropTable("dbo.Notes");
            DropTable("dbo.Subjects");
            DropTable("dbo.SchedulePositions");
            DropTable("dbo.Schedules");
            DropTable("dbo.ClassTypes");
            DropTable("dbo.Classes");
        }
    }
}
