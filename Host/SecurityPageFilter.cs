using Common.Application;
using Common.Infrastructure;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Reflection;

namespace Host;

public class SecurityPageFilter(IAuthHelper authHelper) : IPageFilter
{
    public void OnPageHandlerExecuted(PageHandlerExecutedContext context) { }

    public void OnPageHandlerExecuting(PageHandlerExecutingContext context)
    {
        if (context?.HandlerMethod?.MethodInfo.GetCustomAttribute(typeof(NeedsPermissionAttribute)) is not NeedsPermissionAttribute handlerPermission) return;

        if (authHelper.GetPermissions().All(p => p != handlerPermission.Permission))
            context.HttpContext.Response.Redirect("/Account");
    }

    public void OnPageHandlerSelected(PageHandlerSelectedContext context) { }
}