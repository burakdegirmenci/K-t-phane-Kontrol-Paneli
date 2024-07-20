namespace BookStoreMVC.Domain.Entities
{
    public class Category : AuiditableEntity
    {
        public Category()
        {
            Books = new HashSet<Book>();
        }
        public string Name { get; set; }


        //nav prop
        public virtual IEnumerable<Book> Books { get; set; }
    }
}
