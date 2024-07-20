namespace BookStoreMVC.Infrastructure.DataAccess.Interfaces
{
	public interface IAsyncUpdatableRepostory<TEntity> : IAsyncRepostory where TEntity : BaseEntity
	{
		Task<TEntity> UpdateAsync(TEntity entity);
	}
}
