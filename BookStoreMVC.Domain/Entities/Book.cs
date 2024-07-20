namespace BookStoreMVC.Domain.Entities;

public class Book : AuiditableEntity
{
    public string Name { get; set; }
    public DateTime DateOfPublish { get; set; }
    public bool IsAvailable { get; set; }


    //Nav Prop
    public Guid CategoryId { get; set; }
    public virtual Category Category { get; set; }
    public Guid AuthorId { get; set; }
    public virtual Author Author { get; set; }
    public Guid PublisherId { get; set; }
    public virtual Publisher Publisher { get; set; }
}
