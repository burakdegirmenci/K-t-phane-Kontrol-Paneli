namespace BookStoreMVC.Infrastructure.Repositories.PublisherRepostories;

public class PublisherRepostory : EFBaseRepostory<Publisher> , IPublisherRepostory
{
    public PublisherRepostory(AppDbContext context) : base(context)
    {
        
    }
}
