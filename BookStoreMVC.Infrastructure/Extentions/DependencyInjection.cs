namespace BookStoreMVC.Infrastructure.Extentions
{
	public static class DependencyInjection
	{
		public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
		{
			services.AddDbContext<AppDbContext>(options =>
			{
				options.UseLazyLoadingProxies();
				options.UseSqlServer(configuration.GetConnectionString(AppDbContext.DevConnectionString));
			});

			services.AddScoped<ICategoryRepostory, CategoryRepostory>();
			services.AddScoped<IAuthorRepostory, AuthorRepostory>();
			services.AddScoped<IPublisherRepostory, PublisherRepostory>();
			services.AddScoped<IBookRepostory, BookRepostory>();
			services.AddScoped<IAdminRepostory, AdminRepostory>();
			services.AddScoped<IAppUserRepostory, AppUserRepostory>();

			//AdminSeed.SeedAsync(configuration).GetAwaiter().GetResult(); //seed datayı çalışıtmak için yapıyoruz

			//Üstteki kısım migration yaparken YORUMA ALINACAK!!!!!

			return services;

		}

	}
}
