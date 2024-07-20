namespace BookStoreMVC.Application.Services.AccountServices;

public interface IAccountService
{
    Task<bool>AnyAsync(Expression<Func<IdentityUser, bool>> expression);
    Task<IdentityUser?> FindByIdAsync(string identityId);
    Task<IdentityResult> CreateUserAsync(IdentityUser user, Roles role);
    Task<IdentityResult> DeleteUserAsync(string identityId);
    Task<Guid> GetUserIdAsync(string identityId, string role);
    Task<IdentityResult> UpdateUserAsync(IdentityUser user);
}
