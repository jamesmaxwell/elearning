using System.Web;
using System.Web.Mvc;
using ServiceStack.Logging;

namespace ELearning.Common
{
    public class NotifyErrorAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (filterContext.ExceptionHandled)
                return;

            var log = LogManager.LogFactory.GetLogger("Common");
            log.Error(filterContext.Exception.Message, filterContext.Exception);

            if (filterContext.HttpContext.Request.IsAjaxRequest())
            {
                var exp = filterContext.Exception;
                var errorRes = new ErrorResponse { ErrorCode = exp.GetType().Name, Message = exp.Message, StackTrace = exp.StackTrace};

                var json = new JsonResult();
                json.Data = errorRes;
                json.JsonRequestBehavior = JsonRequestBehavior.AllowGet;
                filterContext.Result = json;
                filterContext.HttpContext.Response.StatusCode = 500;

                filterContext.ExceptionHandled = true;
            }
        }
    }
}