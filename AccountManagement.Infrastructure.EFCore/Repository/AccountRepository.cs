using AccountManagement.Application.Contracts.AccountAggregate;
using AccountManagement.Domain.AccountAggregate;
using Common.Application;
using Common.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace AccountManagement.Infrastructure.EFCore.Repository;

public class AccountRepository(AccountDbContext context) : BaseRepository<long, Account>(context), IAccountRepository
{
    public List<AccountViewModel> Accounts => [.. context.Accounts.Select(a => new AccountViewModel
    {
        Id = a.Id,
        Fullname = a.Fullname,
    })];

    public Account Get(string username) => context.Accounts.FirstOrDefault(a => a.Username == username);
    public EditAccount GetDetails(long id) => context.Accounts.Select(a => new EditAccount
    {
        Id = a.Id,
        Fullname = a.Fullname,
        Username = a.Username,
        Mobile = a.Mobile,
        RoleId = a.RoleId
    }).FirstOrDefault(a => a.Id == id);
    public Account GetWithRole(string username) => context.Accounts.Include(a => a.Role).FirstOrDefault(a => a.Username == username);

    public List<AccountViewModel> Search(AccountSearchModel searchModel)
    {
        var query = context.Accounts.Include(a => a.Role).Select(a => new AccountViewModel
        {
            Id = a.Id,
            Fullname = a.Fullname,
            Username = a.Username,
            Mobile = a.Mobile,
            RoleId = a.RoleId,
            Role = a.Role.Name,
            ProfilePhoto = a.ProfilePhoto,
            CreationDate = a.CreationDate.ToFarsi()
        });

        if (!string.IsNullOrWhiteSpace(searchModel.Fullname))
            query = query.Where(a => a.Fullname.Contains(searchModel.Fullname));

        if (!string.IsNullOrWhiteSpace(searchModel.Username))
            query = query.Where(a => a.Username.Contains(searchModel.Username));

        if (!string.IsNullOrWhiteSpace(searchModel.Mobile))
            query = query.Where(a => a.Mobile.Contains(searchModel.Mobile));

        if (searchModel.RoleId > 0)
            query = query.Where(a => a.RoleId == searchModel.RoleId);

        return [.. query.OrderByDescending(a => a.Id)];
    }
}
