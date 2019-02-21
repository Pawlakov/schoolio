namespace Schoolio.Migrations
{
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Diagnostics;
    using System.Linq;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    using Schoolio.Models;
    using Schoolio.Models.Enums;

    internal sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            this.AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var store = new UserStore<ApplicationUser>(context);
            var manager = new UserManager<ApplicationUser>(store);
            var students = new[] { "student1", "student2", "student3" };

            if (!context.Users.Any(u => u.UserName == "teacher"))
            {
                var user = new ApplicationUser
                {
                    UserName = "teacher",
                    Email = "teacher@ee.pl",
                    EmailConfirmed = true
                };
                manager.Create(user, "testtest");
            }

            foreach (var userName in students)
            {
                if (!context.Users.Any(u => u.UserName == userName))
                {
                    var user = new ApplicationUser
                    {
                        UserName = userName,
                        Email = $"{userName}@ee.pl",
                        EmailConfirmed = true
                    };
                    manager.Create(user, "testtest");
                }
            }

            var teacher = new Teacher { FirstName = "Nauczyciel", LastName = "Nauczyciel", User = context.Users.First(x => x.UserName == "teacher") };
            var classType = new ClassType { Name = "Klasa Typowa" };
            var subjectType1 = new SubjectType { ClassType = classType, Name = "Typ Przedmiotu 1", Teachers = new List<Teacher> { teacher } };
            var subjectType2 = new SubjectType { ClassType = classType, Name = "Typ Przedmiotu 2", Teachers = new List<Teacher> { teacher } };
            var @class = new Class { ClassType = classType };
            var subject1 = new Subject { SubjectType = subjectType1, Name = "Przedmiot 1", Class = @class, Teacher = teacher };
            var subject2 = new Subject { SubjectType = subjectType2, Name = "Przedmiot 2", Class = @class, Teacher = teacher };
            var student1 = new Student
            {
                Class = @class,
                FirstName = "Uczeñ",
                LastName = "Jeden",
                User = context.Users.First(x => x.UserName == "student1")
            };
            var student2 = new Student
            {
                Class = @class,
                FirstName = "Uczeñ",
                LastName = "Dwa",
                User = context.Users.First(x => x.UserName == "student2")
            };
            var student3 = new Student
            {
                Class = @class,
                FirstName = "Uczeñ",
                LastName = "Trzy",
                User = context.Users.First(x => x.UserName == "student3")
            };
            var actorType = new ActorType { Id = ActorTypeEnum.Teacher, Users = new List<ApplicationUser> { teacher.User } };

            context.Teachers.AddOrUpdate(teacher);
            context.ClassTypes.AddOrUpdate(classType);
            context.SubjectTypes.AddOrUpdate(subjectType1);
            context.SubjectTypes.AddOrUpdate(subjectType2);
            context.Classes.AddOrUpdate(@class);
            context.Subjects.AddOrUpdate(subject1);
            context.Subjects.AddOrUpdate(subject2);
            context.Students.AddOrUpdate(student1);
            context.Students.AddOrUpdate(student2);
            context.Students.AddOrUpdate(student3);
            context.ActorTypes.AddOrUpdate(actorType);

            context.SaveChanges();
        }
    }
}
