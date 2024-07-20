namespace BookStoreMVC.Infrastructure.Repositories.AuthorRepostories;

public class AuthorRepostory : EFBaseRepostory<Author>, IAuthorRepostory
{
    public AuthorRepostory(AppDbContext context) : base(context)
    {
        
    }
}
