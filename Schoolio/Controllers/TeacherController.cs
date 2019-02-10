namespace Schoolio.Controllers
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;

    using Schoolio.Models;
    using Schoolio.Models.Enums;
    using Schoolio.ViewModels.Teacher;

    public class TeacherController : RestrictedController
    {
        private ApplicationDbContext context = new ApplicationDbContext();
        
        public TeacherController() : base(ActorTypeEnum.Teacher)
        {
        }
        
        public ActionResult Index()
        {
            var user = this.UserManager.FindById(this.User.Identity.GetUserId());
            var teacher = user.AssignedTeacher;
            var subjects = teacher.TaughtSubjects;

            var viewModel = new IndexViewModel
            {
                Subjects = subjects.ToList().Select(x => new SubjectListItemViewModel(x))
            };
            return this.View(viewModel);
        }

        public ActionResult Subject(int? id)
        {
            if (!id.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var subject = this.context.Subjects.Find(id.Value);
            if (subject == null)
            {
                return this.HttpNotFound();
            }

            var @class = subject.Class;
            var classType = @class?.ClassType;
            var students = @class?.Students?.ToList() ?? new List<Student>();

            var viewModel = new SubjectViewModel()
            {
                SubjectId = subject.Id,
                SubjectTypeId = subject.SubjectType.Id,
                SubjectName = subject.Name,
                ClassName = classType?.Name ?? "None",
                Hours = subject.SchedulePositions != null && subject.SchedulePositions.Any() ? subject.SchedulePositions.Select(x => $"{x.Time} ({x.Duration}min)") : new[] { "None" },
                Students = students.ToList().Select(x => new StudentListItemViewModel(x)).OrderBy(x => x.Name)
            };
            return this.View(viewModel);
        }

        public ActionResult SubjectType(int? id)
        {
            return this.View();
        }

        public ActionResult StudentNotes(int? studentId, int? subjectId)
        {
            if (!subjectId.HasValue || !studentId.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var subject = this.context.Subjects.Find(subjectId.Value);
            var student = this.context.Students.Find(studentId.Value);
            if (subject == null || student == null)
            {
                return this.HttpNotFound();
            }

            var notes = student.Notes.Where(x => x.Subject == subject).ToList();
            var viewModel = new StudentNotesViewModel
            {
                StudentId = student.Id,
                StudentName = $"{student.LastName} {student.FirstName}",
                SubjectId = subject.Id,
                SubjectName = subject.Name,
                Notes = notes.Select(x => new NoteListItemViewModel
                {
                    NoteId = x.Id,
                    Value = x.Value,
                    Comment = x.Comment,
                    Date = x.Date
                }),
                Average = notes.Any() ? notes.Average(x => x.Value) : 0.0f
            };

            return this.View(viewModel);
        }
        
        [HttpGet]
        public ActionResult AddStudentNote(int? studentId, int? subjectId)
        {
            if (!subjectId.HasValue || !studentId.HasValue)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            var subject = this.context.Subjects.Find(subjectId.Value);
            var student = this.context.Students.Find(studentId.Value);
            if (subject == null || student == null)
            {
                return this.HttpNotFound();
            }
            
            var viewModel = new AddStudentNoteViewModel
            {
                StudentId = student.Id,
                StudentName = $"{student.LastName} {student.FirstName}",
                SubjectId = subject.Id,
                SubjectName = subject.Name
            };

            return this.View(viewModel);
        }

        [HttpPost]
        public ActionResult AddStudentNote(AddStudentNoteViewModel viewModel)
        {
            var subject = this.context.Subjects.Find(viewModel.SubjectId);
            var student = this.context.Students.Find(viewModel.StudentId);
            if (subject == null || student == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            if (!this.ModelState.IsValid)
            {
                viewModel.StudentName = $"{student.LastName} {student.FirstName}";
                viewModel.SubjectName = subject.Name;
                return this.View(viewModel);
            }

            var note = this.context.Notes.Create();
            note.Value = viewModel.Value;
            note.Comment = viewModel.Comment;
            note.Date = DateTime.Now;
            note.Student = student;
            note.Subject = subject;
            this.context.Notes.Add(note);
            this.context.SaveChanges();
            return this.RedirectToAction("Subject", "Teacher", new { id = subject.Id });
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.context != null)
                {
                    this.context.Dispose();
                    this.context = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}