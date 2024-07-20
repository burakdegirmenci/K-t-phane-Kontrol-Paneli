namespace BookStoreMVC.Infrastructure.Configurations;

public class BookConfiguration : AudiTableEntityConfiguration<Book>
{
    public override void Configure(EntityTypeBuilder<Book> builder)
    {
        builder.Property(x=> x.Name).IsRequired().HasMaxLength(128);
        builder.Property(x => x.DateOfPublish).IsRequired();
        builder.Property(x => x.IsAvailable).IsRequired();
        base.Configure(builder);
    }
}
