namespace BookStoreMVC.Domain.Core.BaseEntities
{
	public class AuiditableEntity : BaseEntity, IDeletebleEntity
	{
		public string? DeletedBy { get; set; }
		public DateTime? DeletedDate { get; set; }
	}
}
