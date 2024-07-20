namespace BookStoreMVC.Domain.Core.BaseEntitiyConfigurations
{
	public class AudiTableEntityConfiguration<TEntity> : BaseEntitiyConfiguration<TEntity> where TEntity : AuiditableEntity
	{
		public override void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.Property(x => x.DeletedBy).IsRequired(false);
			builder.Property(x => x.DeletedDate).IsRequired(false);
			base.Configure(builder);
		}
	}
}
