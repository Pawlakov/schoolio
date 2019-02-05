namespace Schoolio.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Net;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Schoolio.Models;
    using Schoolio.ViewModels.Teacher;

    public class TeacherController : Controller
    {
        private ApplicationDbContext context = new ApplicationDbContext();

        private ApplicationUserManager userManager;

        private ApplicationUserManager UserManager
        {
            get
            {
                if (this.userManager == null)
                {
                    this.userManager = this.HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
                }

                return this.userManager;
            }
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
                Students = students.ToList().Select(x => new StudentListItemViewModel(x))
            };
            return this.View(viewModel);
        }

        public ActionResult SubjectType(int? id)
        {
            return this.View();
        }

        public ActionResult StudentNotes(int? studentId, int? subjectId)
        {
            return this.View();
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

                if (this.userManager != null)
                {
                    this.userManager.Dispose();
                    this.userManager = null;
                }
            }

            base.Dispose(disposing);
        }
    }
}