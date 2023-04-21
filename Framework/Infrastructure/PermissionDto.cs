namespace Framework.Infrastructure
{
    public class PermissionDto
    {
        public PermissionDto() { }
        public PermissionDto(int code, string name)
        {
            Code = code;
            Name = name;
        }

        public int Code { get; set; }
        public string Name { get; set; }
    }
}
