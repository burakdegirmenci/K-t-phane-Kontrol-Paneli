namespace BookStoreMVC.Infrastructure.Repositories.BookRepostories;

public class BookRepostory : EFBaseRepostory<Book> , IBookRepostory
{
    public BookRepostory(AppDbContext context) : base(context)  
    {
        
    }
}
