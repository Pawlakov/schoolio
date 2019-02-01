namespace Schoolio.ViewModels.Teacher
{
    using System.Collections.Generic;

    public class IndexViewModel
    {
        public IEnumerable<SubjectListItemViewModel> Subjects { get; set; }
    }
}