namespace Schoolio.Models
{
    using System;

    public class Note
    {
        public virtual int Id { get; set; }

        public virtual DateTime Date { get; set; }

        public virtual string Comment { get; set; }

        public virtual float Value { get; set; }

        public virtual Subject Subject { get; set; }

        public virtual Student Student { get; set; }
    }
}