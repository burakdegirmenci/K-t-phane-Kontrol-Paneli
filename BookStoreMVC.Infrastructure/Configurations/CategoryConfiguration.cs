namespace BookStoreMVC.Infrastructure.Configurations
{
    public class CategoryConfiguration : AudiTableEntityConfiguration<Category>
    {
        public override void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.Property(x => x.Name).IsRequired().HasMaxLength(128);
            base.Configure(builder);
        }
    }
}
