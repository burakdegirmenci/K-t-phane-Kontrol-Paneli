namespace BookStoreMVC.Application.Services.AccountServices
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IAdminRepostory _adminRepostory;
        private readonly IAppUserRepostory _appUserRepostory;

        public AccountService(UserManager<IdentityUser> userManager, IAdminRepostory adminRepostory, IAppUserRepostory appUserRepostory)
        {
            _userManager = userManager;
            _adminRepostory = adminRepostory;
            _appUserRepostory = appUserRepostory;
        }

        public async Task<bool> AnyAsync(Expression<Func<IdentityUser, bool>> expression)
        {
            return await _userManager.Users.AnyAsync(expression);
        }

        public async Task<IdentityResult> CreateUserAsync(IdentityUser user, Roles role)
        {
            var result = await _userManager.CreateAsync(user, "Password.1");
            if(!result.Succeeded)
            {
                return result;
            }
            return await _userManager.AddToRoleAsync(user, role.ToString());
        }

        public async Task<IdentityResult> DeleteUserAsync(string identityId)
        {
            var user = await _userManager.FindByIdAsync(identityId);
            if(user is null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code="Kullanıcı bulunamadı.",
                    Description="Kullanıcı bulunamadı."
                });
            }
            return await _userManager.DeleteAsync(user);
        }
        public async Task<IdentityResult> UpdateUserAsync(IdentityUser user)
        {
            
            if (user is null)
            {
                return IdentityResult.Failed(new IdentityError()
                {
                    Code = "Kullanıcı bulunamadı.",
                    Description = "Kullanıcı bulunamadı."
                });
            }
            return await _userManager.UpdateAsync(user);
        }

        public async Task<IdentityUser?> FindByIdAsync(string identityId)
        {
            return await _userManager.FindByIdAsync(identityId);
        }

        public async Task<Guid> GetUserIdAsync(string identityId, string role)
        {
            BaseUser? user = role switch
            {
                "Admin" => await _adminRepostory.GetByIdentityId(identityId),
                "AppUser" => await _appUserRepostory.GetByIdentityId(identityId),
                _ => null
            };
            return user is null ? Guid.NewGuid() : user.Id;
        }
    }
}
