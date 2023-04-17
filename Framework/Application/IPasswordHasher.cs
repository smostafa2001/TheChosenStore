namespace Framework.Application
{
    public interface IPasswordHasher
    {
        string Hash(string password);
        (bool verified, bool needsUpgrade) Check(string hash, string password);
    }
}
