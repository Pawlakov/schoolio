namespace Schoolio.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public class ClaimGroup
    {
        public virtual int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual string Name { get; set; }

        public virtual ICollection<Claim> Claims { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}