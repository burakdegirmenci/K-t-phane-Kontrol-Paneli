namespace BookStoreMVC.Infrastructure.DataAccess.Interfaces
{
	public interface IAsyncInsertable<TEntity> : IAsyncRepostory where TEntity : BaseEntity
	{
		Task<TEntity> AddAsync(TEntity entity);
		Task AddRangeAsync(IEnumerable<TEntity> entities);
	}
}
