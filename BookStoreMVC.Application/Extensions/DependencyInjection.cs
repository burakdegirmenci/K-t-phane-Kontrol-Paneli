namespace BookStoreMVC.Application.Extensions
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IAuthorService, AuthorService>();
            services.AddScoped<IPublisherServices, PublisherService>();
            services.AddScoped<IBookService, BookService>();
            services.AddScoped<IAccountService, AccountService>();
            services.AddScoped<IAppUserService, AppUserService>();
            services.AddScoped<IAdminService, AdminService>();
            ConfigureMapster();
            return services;
        }

        public static void ConfigureMapster()
        {
            TypeAdapterConfig<Book, BookListDTO>.NewConfig()
                .Map(dest => dest.CategoryName, src => src.Category.Name)
                .Map(dest => dest.PublisherName, src => src.Publisher.Name)
                .Map(dest => dest.AuthorName, src => src.Author.Name);

        }
    }
}
