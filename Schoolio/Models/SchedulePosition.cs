namespace Schoolio.Models
{
    using System.ComponentModel.DataAnnotations;

    public class SchedulePosition
    {
        public virtual int Id { get; set; }

        [Required(AllowEmptyStrings = false)]
        public virtual string Time { get; set; }

        public virtual int Duration { get; set; }

        public virtual int Room { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Schedule Schedule { get; set; }
    }
}