namespace Schoolio.Controllers
{
    using System.Linq;
    using System.Web;
    using System.Web.Mvc;

    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.Owin;

    using Schoolio.Models.Enums;

    public class RestrictedController : Controller
    {
        private readonly ActorTypeEnum restriction;

        private ApplicationUserManager userManager;

        public RestrictedController(ActorTypeEnum restriction)
        {
            this.restriction = restriction;
        }

        protected ApplicationUserManager UserManager
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
        
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var user = this.UserManager.FindById(this.User.Identity.GetUserId());
            if (user.Types.All(x => x.Id != this.restriction))
            {
                filterContext.Result = new HttpUnauthorizedResult("Restricted!");
            }

            base.OnActionExecuting(filterContext);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
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