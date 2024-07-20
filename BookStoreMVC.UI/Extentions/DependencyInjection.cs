namespace BookStoreMVC.UI.Extentions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUIServices(this IServiceCollection services)
        {
            AddIdentityService(services);
            services.AddNotyf(config =>
            {
                config.HasRippleEffect = true;
                config.DurationInSeconds = 5;
                config.Position = NotyfPosition.BottomRight;
                config.IsDismissable = true;
            });
            return services;
        }

        private static IServiceCollection AddIdentityService(this IServiceCollection services)
        {
            services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppDbContext>();
            return services;
        }
    }
}
