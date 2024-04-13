namespace Common.Application;

public interface IAuthHelper
{
    void SignIn(AuthViewModel account);
    bool IsAuthenticated();
    void SignOut();
    string CurrentAccountRole();
    AuthViewModel CurrentAccount { get; }
    long CurrentAccountId { get; }
    List<int> GetPermissions();
}
