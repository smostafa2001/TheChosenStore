using Framework.Application;
using Framework.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Linq;
using System.Reflection;

namespace ServiceHost
{
    public class SecurityPageFilter : IPageFilter
    {
        private readonly IAuthHelper _authHelper;

        public SecurityPageFilter(IAuthHelper authHelper) => _authHelper = authHelper;

        public void OnPageHandlerExecuted(PageHandlerExecutedContext context) { }
        public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {
            if (context.HandlerMethod.MethodInfo.GetCustomAttribute(typeof(NeedsPermissionAttribute)) is not NeedsPermissionAttribute handlerPermission)
                return;

            if (_authHelper.GetPermissions().All(p => p != handlerPermission.Permission))
                context.HttpContext.Response.Redirect("/Account");
        }
        public void OnPageHandlerSelected(PageHandlerSelectedContext context) { }
    }
}
