namespace BookStoreMVC.Infrastructure.Repositories.CategoryRepostories;

public class CategoryRepostory:EFBaseRepostory<Category>, ICategoryRepostory
{
    public CategoryRepostory(AppDbContext context) : base(context)
    {
        
    }
}
