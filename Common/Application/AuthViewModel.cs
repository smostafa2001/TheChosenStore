namespace Common.Application;

public class AuthViewModel
{
    public long Id { get; set; }
    public long RoleId { get; set; }
    public string Role { get; set; } = string.Empty;
    public string Fullname { get; set; } = string.Empty;
    public string Username { get; set; } = string.Empty;
    public string ProfilePhoto { get; set; } = string.Empty;
    public List<int> Permissions { get; set; } = [];
}