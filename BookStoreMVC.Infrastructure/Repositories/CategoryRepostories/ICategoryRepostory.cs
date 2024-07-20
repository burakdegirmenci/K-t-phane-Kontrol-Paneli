namespace BookStoreMVC.Infrastructure.Repositories.CategoryRepostories;

public interface ICategoryRepostory : IAsyncRepostory, IAsyncInsertable<Category>, IAsyncFindableRepostory<Category>, IAsyncQueryableRepostory<Category>,IAsyncUpdatableRepostory<Category>, IAsyncDeletableRepostory<Category>
{
}
