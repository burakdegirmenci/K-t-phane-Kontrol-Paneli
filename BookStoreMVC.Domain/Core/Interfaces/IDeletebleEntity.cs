namespace BookStoreMVC.Domain.Core.Interfaces
{
	public interface IDeletebleEntity 
	{
        public string? DeletedBy { get; set; }
        public DateTime? DeletedDate { get; set; }
    }
}
