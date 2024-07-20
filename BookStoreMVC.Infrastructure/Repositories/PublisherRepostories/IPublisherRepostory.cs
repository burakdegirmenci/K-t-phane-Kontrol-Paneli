namespace BookStoreMVC.Infrastructure.Repositories.PublisherRepostories;

public interface IPublisherRepostory: IAsyncRepostory, IAsyncInsertable<Publisher>, IAsyncFindableRepostory<Publisher>, IAsyncQueryableRepostory<Publisher>, IAsyncUpdatableRepostory<Publisher>, IAsyncDeletableRepostory<Publisher>
{
}
