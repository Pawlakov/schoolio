namespace Schoolio.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Student
    {
        public virtual int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual string LastName { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual Class Class { get; set; }

        public virtual ICollection<Parent> Parents { get; set; }

        public virtual ICollection<Note> Notes { get; set; }
    }
}