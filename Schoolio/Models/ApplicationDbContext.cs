namespace Schoolio.Models
{
    using System.Data.Entity;

    using Microsoft.AspNet.Identity.EntityFramework;

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public virtual DbSet<Claim> Claims { get; set; }

        public virtual DbSet<ClaimGroup> ClaimGroups { get; set; }

        public virtual DbSet<Class> Classes { get; set; }

        public virtual DbSet<ClassType> ClassTypes { get; set; }

        public virtual DbSet<Note> Notes { get; set; }

        public virtual DbSet<Parent> Parents { get; set; }

        public virtual DbSet<Schedule> Schedules { get; set; }

        public virtual DbSet<SchedulePosition> SchedulePositions { get; set; }

        public virtual DbSet<Student> Students { get; set; }

        public virtual DbSet<Subject> Subjects { get; set; }

        public virtual DbSet<SubjectType> SubjectTypes { get; set; }

        public virtual DbSet<Teacher> Teachers { get; set; }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>().HasOptional(x => x.AssignedTeacher).WithRequired(x => x.User);
            modelBuilder.Entity<ApplicationUser>().HasOptional(x => x.AssignedParent).WithRequired(x => x.User);
            modelBuilder.Entity<ApplicationUser>().HasOptional(x => x.AssignedStudent).WithRequired(x => x.User);
            modelBuilder.Entity<ApplicationUser>().HasOptional(x => x.Group).WithMany(x => x.Users);

            modelBuilder.Entity<ClaimGroup>().HasKey(x => x.Id);
            modelBuilder.Entity<ClaimGroup>().HasMany(x => x.Claims).WithMany(x => x.Groups);

            modelBuilder.Entity<Claim>().HasKey(x => x.Id);

            modelBuilder.Entity<Parent>().HasKey(x => x.Id);
            modelBuilder.Entity<Parent>().HasMany(x => x.Children).WithMany(x => x.Parents);

            modelBuilder.Entity<Student>().HasKey(x => x.Id);
            modelBuilder.Entity<Student>().HasOptional(x => x.Class).WithMany(x => x.Students);
            modelBuilder.Entity<Student>().HasMany(x => x.Notes).WithRequired(x => x.Student);

            modelBuilder.Entity<Class>().HasKey(x => x.Id);
            modelBuilder.Entity<Class>().HasOptional(x => x.Schedule).WithOptionalPrincipal(x => x.Class);
            modelBuilder.Entity<Class>().HasRequired(x => x.ClassType).WithMany(x => x.Classes);
            modelBuilder.Entity<Class>().HasOptional(x => x.Curator).WithOptionalDependent(x => x.CuratedClass);

            modelBuilder.Entity<Teacher>().HasKey(x => x.Id);
            modelBuilder.Entity<Teacher>().HasMany(x => x.TaughtSubjects).WithOptional(x => x.Teacher);
            modelBuilder.Entity<Teacher>().HasMany(x => x.TeachingAbilities).WithMany(x => x.Teachers);

            modelBuilder.Entity<Subject>().HasKey(x => x.Id);
            modelBuilder.Entity<Subject>().HasRequired(x => x.SubjectType).WithMany(x => x.Subjects);
            modelBuilder.Entity<Subject>().HasMany(x => x.SchedulePositions).WithRequired(x => x.Subject);
            modelBuilder.Entity<Subject>().HasMany(x => x.Notes).WithRequired(x => x.Subject);

            modelBuilder.Entity<Note>().HasKey(x => x.Id);

            modelBuilder.Entity<SchedulePosition>().HasKey(x => x.Id);
            modelBuilder.Entity<SchedulePosition>().HasRequired(x => x.Schedule).WithMany(x => x.Positions);

            modelBuilder.Entity<Schedule>().HasKey(x => x.Id);
            modelBuilder.Entity<Schedule>().HasRequired(x => x.ClassType).WithMany(x => x.Schedules);

            modelBuilder.Entity<ClassType>().HasKey(x => x.Id);
            modelBuilder.Entity<ClassType>().HasMany(x => x.SubjectTypes).WithOptional(x => x.ClassType);

            modelBuilder.Entity<SubjectType>().HasKey(x => x.Id);
        }
    }
}