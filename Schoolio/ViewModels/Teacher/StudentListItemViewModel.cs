namespace Schoolio.ViewModels.Teacher
{
    using Schoolio.Models;

    public class StudentListItemViewModel
    {
        public StudentListItemViewModel(Student model)
        {
            this.Id = model.Id;
            this.Name = $"{model.LastName} {model.FirstName}";
        }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}