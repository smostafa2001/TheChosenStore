using AccountManagement.Application.Contracts.RoleAggregate;
using AccountManagement.Domain.RoleAggregate;
using Framework.Application;
using Framework.Infrastructure;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Infrastructure.EFCore.Repository
{
    public class RoleRepository : BaseRepository<long, Role>, IRoleRepository
    {
        private readonly AccountDbContext _context;

        public RoleRepository(AccountDbContext context) : base(context) => _context = context;

        public EditRole GetDetails(long id) => _context.Roles.Select(r => new EditRole
        {
            Id = r.Id,
            Name = r.Name
        }).FirstOrDefault(r => r.Id == id);

        public List<RoleViewModel> GetRoles() => _context.Roles.Select(r => new RoleViewModel
        {
            Id = r.Id,
            Name = r.Name,
            CreationDate = r.CreationDate.ToFarsi()
        }).ToList();
    }
}
