namespace Schoolio.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class SubjectType
    {
        public virtual int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual string Name { get; set; }

        public virtual ClassType ClassType { get; set; }

        public virtual ICollection<Teacher> Teachers { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }
    }
}