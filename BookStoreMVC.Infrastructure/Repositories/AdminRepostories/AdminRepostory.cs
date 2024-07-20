namespace BookStoreMVC.Infrastructure.Repositories.AdminRepostories;

public class AdminRepostory : EFBaseRepostory<Admin>, IAdminRepostory
{
    public AdminRepostory(AppDbContext context) : base(context)
    {

    }

    public Task<Admin?> GetByIdentityId(string identityId)
    {
        return _table.FirstOrDefaultAsync(x=> x.IdentityId == identityId);
    }
}
