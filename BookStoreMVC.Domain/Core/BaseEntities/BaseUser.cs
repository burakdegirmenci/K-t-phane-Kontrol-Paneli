namespace BookStoreMVC.Domain.Core.BaseEntities
{
	public class BaseUser : AuiditableEntity
	{
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string IdentityId { get; set; }
    }
}
