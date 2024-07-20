namespace BookStoreMVC.Infrastructure.Configurations
{
    public class AppUserConfiguration : BaseUserConfiguration<AppUser>
    {
        public override void Configure(EntityTypeBuilder<AppUser> builder)
        {
            base.Configure(builder);
        }
    }
}
