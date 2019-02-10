namespace Schoolio.Models
{
    using System.Collections.Generic;

    using Schoolio.Models.Enums;

    public class ActorType
    {
        public virtual ActorTypeEnum Id { get; set; }

        public virtual ICollection<ApplicationUser> Users { get; set; }
    }
}