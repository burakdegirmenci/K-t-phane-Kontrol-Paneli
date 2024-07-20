namespace BookStoreMVC.UI.Areas.Admin.Models.BookVMs
{
    public class UpdateAvailabilityModel
    {
        public Guid BookId { get; set; }
        public bool IsBookAvailable { get; set; }
    }
}
