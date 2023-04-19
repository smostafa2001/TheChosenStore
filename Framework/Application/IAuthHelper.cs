using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Framework.Application
{
    public interface IAuthHelper
    {
        void SignIn(AuthViewModel account);
        bool IsAuthenticated();
        void SignOut();
    }
}
