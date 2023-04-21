using Framework.Application;
using Microsoft.AspNetCore.Razor.TagHelpers;
using System.Linq;

namespace ServiceHost
{
    [HtmlTargetElement(Attributes = "permission")]
    public class PermissionTagHelper : TagHelper
    {
        private readonly IAuthHelper _authHelper;

        public PermissionTagHelper(IAuthHelper authHelper) => _authHelper = authHelper;

        public int Permission { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (!_authHelper.IsAuthenticated())
            {
                output.SuppressOutput();
                return;
            }
            var permissions = _authHelper.GetPermissions();
            if (permissions.All(p => p != Permission))
            {
                output.SuppressOutput();
                return;
            }
        }
    }
}
