using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Infrastructure
{
    public class NeedsPermissionAttribute:Attribute
    {
        public int Permission { get; set; }

        public NeedsPermissionAttribute(int permission) => Permission = permission;
    }
}
