namespace Schoolio.Models
{
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    using Schoolio.Models.Enums;

    public class Claim
    {
        public virtual int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual string Name { get; set; }

        public virtual ClaimType ClaimType { get; set; }

        public virtual ICollection<ClaimGroup> Groups { get; set; }
    }
}