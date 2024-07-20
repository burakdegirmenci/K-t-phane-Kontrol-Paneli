using BookStoreMVC.Domain.Entities;
using BookStoreMVC.Infrastructure.AppContext;
using BookStoreMVC.Infrastructure.DataAccess.EntityFreamwork;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Infrastructure.Repositories.AppUserRepostories;

public class AppUserRepostory : EFBaseRepostory<AppUser>, IAppUserRepostory
{
    public AppUserRepostory(AppDbContext context) : base(context)
    {

    }

    public Task<AppUser?> GetByIdentityId(string identityId)
    {
        return _table.FirstOrDefaultAsync(x => x.IdentityId == identityId);
    }
}
