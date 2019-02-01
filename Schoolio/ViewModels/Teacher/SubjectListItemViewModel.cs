namespace Schoolio.ViewModels.Teacher
{
    using Schoolio.Models;

    public class SubjectListItemViewModel
    {
        public SubjectListItemViewModel(Subject model)
        {
            this.Id = model.Id;
            this.Name = model.Name;
            this.TypeId = model.SubjectType.Id;
            this.TypeName = model.SubjectType.Name;
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public int TypeId { get; set; }

        public string TypeName { get; set; }
    }
}