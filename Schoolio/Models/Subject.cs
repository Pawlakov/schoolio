namespace Schoolio.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Subject
    {
        public virtual int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual string Name { get; set; }
        
        public virtual Teacher Teacher { get; set; }

        public virtual SubjectType SubjectType { get; set; }

        public virtual Class Class { get; set; }

        public virtual ICollection<SchedulePosition> SchedulePositions { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}