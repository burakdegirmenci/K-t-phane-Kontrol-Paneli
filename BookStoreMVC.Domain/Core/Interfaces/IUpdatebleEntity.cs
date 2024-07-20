namespace BookStoreMVC.Domain.Core.Interfaces
{
	public interface IUpdatebleEntity : ICreatebleEntity
	{
        public string? UpdateedBy { get; set; }
        public DateTime? UpdatedDate { get; set; }
    }
}
