namespace BookStoreMVC.Infrastructure.Repositories.BookRepostories;

public interface IBookRepostory: IAsyncRepostory, IAsyncInsertable<Book>, IAsyncFindableRepostory<Book>, IAsyncQueryableRepostory<Book>, IAsyncUpdatableRepostory<Book>, IAsyncDeletableRepostory<Book>
{
}
