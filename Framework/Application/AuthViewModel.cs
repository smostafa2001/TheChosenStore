using System.Collections.Generic;

namespace Framework.Application
{
    public class AuthViewModel
    {
        public long Id { get; set; }
        public long RoleId { get; set; }
        public string Role { get; set; }
        public string Fullname { get; set; }
        public string Username { get; set; }
        public string ProfilePhoto { get; set; }
        public List<int> Permissions { get; set; }
    }
}