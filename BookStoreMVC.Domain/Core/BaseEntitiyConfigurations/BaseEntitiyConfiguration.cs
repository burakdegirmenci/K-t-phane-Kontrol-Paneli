namespace BookStoreMVC.Domain.Core.BaseEntitiyConfigurations
{
	public class BaseEntitiyConfiguration<TEntity> : IEntityTypeConfiguration<TEntity> where TEntity : BaseEntity
	{
		public virtual void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.HasKey(x => x.Id);
			builder.Property(x=>x.Id).ValueGeneratedOnAdd();
			builder.Property(x => x.CreatedBy).IsRequired();
			builder.Property(x => x.CreatedDate).IsRequired();
			builder.Property(x => x.Status).IsRequired();
			builder.Property(x => x.UpdateedBy).IsRequired(false);
			builder.Property(x => x.UpdatedDate).IsRequired(false);


		}
	}
}
