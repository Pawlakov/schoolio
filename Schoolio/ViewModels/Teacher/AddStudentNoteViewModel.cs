namespace Schoolio.ViewModels.Teacher
{
    using System.ComponentModel.DataAnnotations;

    public class AddStudentNoteViewModel
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        [Range(1.0, 6.0, ErrorMessage = "Note is out of range.")]
        public float Value { get; set; }

        public string Comment { get; set; }
    }
}