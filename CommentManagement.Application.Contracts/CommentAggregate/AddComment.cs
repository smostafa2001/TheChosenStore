namespace CommentManagement.Application.Contracts.CommentAggregate
{
    public class AddComment
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Website { get; set; }
        public string Message { get; set; }
        public long OwnerRecordId { get; set; }
        public byte Type { get; set; }
        public long ParentId { get; set; }
    }
}
