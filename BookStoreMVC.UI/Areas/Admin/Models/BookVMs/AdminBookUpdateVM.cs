using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStoreMVC.UI.Areas.Admin.Models.BookVMs
{
    public class AdminBookUpdateVM
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime DateOfPublish { get; set; }
        public bool IsAvailable { get; set; }
        public Guid CategoryId { get; set; }
        public SelectList? Categories { get; set; }
        public Guid AuthorId { get; set; }
        public SelectList? Authors { get; set; }
        public Guid PublisherId { get; set; }
        public SelectList? Publishers { get; set; }
    }
}
