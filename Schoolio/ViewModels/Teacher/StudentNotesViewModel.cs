namespace Schoolio.ViewModels.Teacher
{
    using System.Collections.Generic;

    public class StudentNotesViewModel
    {
        public int StudentId { get; set; }

        public string StudentName { get; set; }

        public int SubjectId { get; set; }

        public string SubjectName { get; set; }

        public float Average { get; set; }

        public IEnumerable<NoteListItemViewModel> Notes { get; set; }
    }
}