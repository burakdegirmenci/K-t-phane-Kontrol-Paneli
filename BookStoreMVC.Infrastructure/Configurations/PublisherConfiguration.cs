namespace BookStoreMVC.Infrastructure.Configurations;

public class PublisherConfiguration : AudiTableEntityConfiguration<Publisher>
{
	public override void Configure(EntityTypeBuilder<Publisher> builder)
	{
		builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
		builder.Property(x=> x.Address).IsRequired().HasMaxLength(256);
		base.Configure(builder);
	}
}
