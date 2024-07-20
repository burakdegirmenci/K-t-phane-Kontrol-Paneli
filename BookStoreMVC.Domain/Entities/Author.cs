namespace BookStoreMVC.Domain.Entities;

public class Author: AuiditableEntity
{
    public Author()
    {
        Books = new HashSet<Book>();
    }
    public string Name { get; set; }
    public DateTime BirthDate { get; set; }

    // Nav Prop
    public virtual IEnumerable<Book> Books { get; set; }


}
