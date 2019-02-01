namespace Schoolio.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ClassType
    {
        public virtual int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual string Name { get; set; }

        public virtual ICollection<Class> Classes { get; set; }

        public virtual ICollection<Schedule> Schedules { get; set; }

        public virtual ICollection<SubjectType> SubjectTypes { get; set; }
    }
}