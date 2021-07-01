using System.Web.Mvc;

namespace qssWeb.Controllers
{
    public class BaseController : Controller  
    {  
        protected override void OnActionExecuted(ActionExecutedContext filterContext)
        {  
            base.OnActionExecuted(filterContext);  
            if (Session["userId"] == null)  
            {  
                filterContext.Result = Redirect("/User/Login");
            }  
        }  
    }  
}