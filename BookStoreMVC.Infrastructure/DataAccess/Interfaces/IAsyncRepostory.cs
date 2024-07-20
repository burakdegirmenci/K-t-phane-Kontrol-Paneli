namespace BookStoreMVC.Infrastructure.DataAccess.Interfaces
{
	public interface IAsyncRepostory
	{
		Task<int> SaveChangesAsync();
	}
}
