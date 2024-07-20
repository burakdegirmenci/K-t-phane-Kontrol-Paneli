namespace BookStoreMVC.Infrastructure.Repositories.AppUserRepostories;

public interface IAppUserRepostory : IAsyncRepostory, IAsyncInsertable<AppUser>, IAsyncFindableRepostory<AppUser>, IAsyncQueryableRepostory<AppUser>, IAsyncUpdatableRepostory<AppUser>, IAsyncDeletableRepostory<AppUser>, IAsyncTransactionRepostory
{
    Task<AppUser?> GetByIdentityId(string identityId);
}
