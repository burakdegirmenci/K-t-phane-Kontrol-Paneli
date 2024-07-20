namespace BookStoreMVC.Infrastructure.DataAccess.Interfaces
{
	public interface IAsyncFindableRepostory<TEntity> where TEntity : BaseEntity
	{
		Task<bool> AnyAsnc (Expression<Func<TEntity, bool>> expression =null);
		Task<TEntity?> GetByIdAsync(Guid id, bool trancking = true);
		Task<TEntity?> GetAsync(Expression<Func<TEntity, bool>> expression, bool tracking = true);
	}
}
