namespace Schoolio.ViewModels.Teacher
{
    using System.Collections.Generic;

    public class SubjectViewModel
    {
        public int Id { get; set; }

        public int SubjectTypeId { get; set; }

        public IEnumerable<StudentListItemViewModel> Students { get; set; }
    }
}