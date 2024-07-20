namespace BookStoreMVC.Infrastructure.DataAccess.Interfaces
{
	public interface IAsyncTransactionRepostory
	{
		Task<IDbContextTransaction> BeginTransactionAsync (CancellationToken cancellationToken = default);
		Task<IExecutionStrategy> CreateExecutionStrategy();
	}
}
