namespace BookStoreMVC.Infrastructure.DataAccess.Interfaces
{
	public interface IAsyncDeletableRepostory<TEntity> : IAsyncRepostory where TEntity : BaseEntity
	{
		Task DeleteAsync (TEntity entity);
		Task DeleteRangeAsync (IEnumerable<TEntity> entities);
	}
}
