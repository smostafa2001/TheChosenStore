using Common.Application;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Host;

[HtmlTargetElement(Attributes = "permission")]
public class PermissionTagHelper(IAuthHelper authHelper) : TagHelper
{
    public int Permission { get; set; }

    public override void Process(TagHelperContext context, TagHelperOutput output)
    {
        if (!authHelper.IsAuthenticated())
        {
            output.SuppressOutput();
            return;
        }

        var permissions = authHelper.GetPermissions();
        if (permissions.All(p => p != Permission))
        {
            output.SuppressOutput();
            return;
        }
    }
}
