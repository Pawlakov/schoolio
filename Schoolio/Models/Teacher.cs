namespace Schoolio.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Teacher
    {
        public virtual int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual string LastName { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Subject> TaughtSubjects { get; set; }

        public virtual ICollection<SubjectType> TeachingAbilities { get; set; }

        public virtual Class CuratedClass { get; set; }
    }
}