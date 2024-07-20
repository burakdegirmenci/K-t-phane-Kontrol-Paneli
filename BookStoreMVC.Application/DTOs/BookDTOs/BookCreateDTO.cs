namespace BookStoreMVC.Application.DTOs.BookDTOs;

public class BookCreateDTO
{
    public string Name { get; set; }
    public DateTime DateOfPublish { get; set; }
    public bool IsAvailable { get; set; }
    public Guid CategoryId { get; set; }
    public Guid AuthorId { get; set; }
    public Guid PublisherId { get; set; }

}
