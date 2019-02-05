namespace Schoolio.Models
{
    using System.Collections.Generic;

    public class Class
    {
        public virtual int Id { get; set; }

        public virtual Schedule Schedule { get; set; }

        public virtual ClassType ClassType { get; set; }

        public virtual Teacher Curator { get; set; }

        public virtual ICollection<Student> Students { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}