namespace BookStoreMVC.Infrastructure.Repositories.AdminRepostories;

public interface IAdminRepostory: IAsyncRepostory, IAsyncInsertable<Admin>, IAsyncFindableRepostory<Admin>, IAsyncQueryableRepostory<Admin>, IAsyncUpdatableRepostory<Admin>, IAsyncDeletableRepostory<Admin>, IAsyncTransactionRepostory
{
    Task<Admin?> GetByIdentityId(string identityId);
}
