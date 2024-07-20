namespace BookStoreMVC.Domain.Core.BaseEntities
{
	public class BaseEntity : IUpdatebleEntity
	{
		public string? UpdateedBy { get; set ; }
		public DateTime? UpdatedDate { get; set; }
		public string CreatedBy { get; set; }
		public DateTime CreatedDate { get; set; }
		public Guid Id { get; set; }
		public Status Status { get; set; }
	}
}
