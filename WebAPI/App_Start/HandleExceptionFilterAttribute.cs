using BusinessLayer.Exceptions;
using System.Net;
using System.Net.Http;
using System.Web.Http.Filters;

namespace WebAPI.App_Start
{
    public class HandleExceptionFilterAttribute : ExceptionFilterAttribute
    {
        public override void OnException(HttpActionExecutedContext context)
        {
            if (context.Exception is InvalidInputException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.BadRequest);
            }
            else if (context.Exception is InvalidCredentialsException)
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            }
            else
            {
                context.Response = new HttpResponseMessage(HttpStatusCode.InternalServerError);
            }
        }
    }
}
