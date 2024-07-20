namespace BookStoreMVC.Infrastructure.DataAccess.Interfaces
{
	public interface IAsyncOrderableRepostory<TEntity> where TEntity : BaseEntity
	{
		Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, TKey>> orderBy, bool orderByDesc, bool trancking = true);
		Task<IEnumerable<TEntity>> GetAllAsync<TKey>(Expression<Func<TEntity, bool>> expression, Expression<Func<TEntity, TKey>> orderBy, bool orderByDesc, bool tracking = true);
	}
}
