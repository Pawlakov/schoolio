namespace Schoolio.ViewModels.Teacher
{
    using System.Collections.Generic;

    public class SubjectViewModel
    {
        public int SubjectId { get; set; }

        public int SubjectTypeId { get; set; }

        public string SubjectName { get; set; }

        public string ClassName { get; set; }

        public IEnumerable<string> Hours { get; set; }

        public IEnumerable<StudentListItemViewModel> Students { get; set; }
    }
}