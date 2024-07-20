namespace BookStoreMVC.Infrastructure.Repositories.AuthorRepostories;

public interface IAuthorRepostory : IAsyncRepostory, IAsyncInsertable<Author>, IAsyncFindableRepostory<Author>, IAsyncQueryableRepostory<Author>, IAsyncUpdatableRepostory<Author>, IAsyncDeletableRepostory<Author>
{
}
