namespace BookStoreMVC.Domain.Core.BaseEntitiyConfigurations
{
	public class BaseUserConfiguration<TEntity> : AudiTableEntityConfiguration<TEntity> where TEntity : BaseUser
	{
		public override void Configure(EntityTypeBuilder<TEntity> builder)
		{
			builder.Property(x=> x.FirstName).IsRequired().HasMaxLength(128);
			builder.Property(x => x.LastName).IsRequired().HasMaxLength(128);
			builder.Property(x => x.Email).IsRequired();
			base.Configure(builder);
		}
	}
}
