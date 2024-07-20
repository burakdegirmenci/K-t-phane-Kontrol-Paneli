namespace BookStoreMVC.Infrastructure.DataAccess.Interfaces
{
	public interface IAsyncQueryableRepostory<TEntity> where TEntity : BaseEntity
	{
		Task<IEnumerable<TEntity>> GetAllAsync(bool tracking = true);
		Task<IEnumerable<TEntity>> GetAllAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true);
	}
}
