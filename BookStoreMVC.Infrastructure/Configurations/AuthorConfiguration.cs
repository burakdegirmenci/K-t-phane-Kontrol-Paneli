namespace BookStoreMVC.Infrastructure.Configurations;

public class AuthorConfiguration : AudiTableEntityConfiguration<Author>
{
    public override void Configure(EntityTypeBuilder<Author> builder)
    {
        builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
        builder.Property(x=> x.BirthDate).IsRequired();
        base.Configure(builder);
    }
}
