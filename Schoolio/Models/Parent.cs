namespace Schoolio.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class Parent
    {
        public virtual int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual string FirstName { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual string LastName { get; set; }

        public virtual ApplicationUser User { get; set; }

        public string Address { get; set; }

        public string PhoneNumber { get; set; }

        public virtual ICollection<Student> Children { get; set; }
    }
}