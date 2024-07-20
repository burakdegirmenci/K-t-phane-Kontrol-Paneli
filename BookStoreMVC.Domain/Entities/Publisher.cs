namespace BookStoreMVC.Domain.Entities;

public class Publisher : AuiditableEntity
{
    public Publisher()
    {
        Books = new HashSet<Book>();
    }
    public string Name { get; set; }
    public string Address { get; set; }

    //nav prop
    public virtual IEnumerable<Book> Books { get; set; }
}
