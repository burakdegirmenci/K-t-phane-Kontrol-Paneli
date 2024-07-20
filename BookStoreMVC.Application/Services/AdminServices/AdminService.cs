namespace BookStoreMVC.Application.Services.AdminServices;

public class AdminService : IAdminService
{
    private readonly IAdminRepostory _adminRepostory;
    private readonly IAccountService _accountService;

    public AdminService(IAdminRepostory adminRepostory, IAccountService accountService)
    {
        _adminRepostory = adminRepostory;
        _accountService = accountService;
    }


    public async Task<IDataResult<AdminDTO>> CreateAsync(AdminCreateDTO adminCreateDTO)
    {
        if (await _accountService.AnyAsync(x => x.Email == adminCreateDTO.Email))
        {
            return new ErrorDataResult<AdminDTO>("Email Kullanılıyor");
        }

        IdentityUser identityUser = new IdentityUser()
        {
            Email = adminCreateDTO.Email,
            NormalizedEmail = adminCreateDTO.Email.ToUpperInvariant(),
            UserName = adminCreateDTO.Email,
            NormalizedUserName = adminCreateDTO.Email.ToUpperInvariant(),
            EmailConfirmed = true
        };

        DataResult<AdminDTO> result = new ErrorDataResult<AdminDTO>();

        var strategy = await _adminRepostory.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _adminRepostory.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var identityResult = await _accountService.CreateUserAsync(identityUser, Domain.Enums.Roles.Admin);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<AdminDTO>(identityResult.ToString());
                    transactionScope.Rollback();
                    return;
                }
                var appUser = adminCreateDTO.Adapt<Admin>();
                appUser.IdentityId = identityUser.Id;
                await _adminRepostory.AddAsync(appUser);
                await _adminRepostory.SaveChangesAsync();
                result = new SuccessDataResult<AdminDTO>("Admin Ekleme Başarılı");
                transactionScope.Commit();
            }
            catch (Exception ex)
            {

                result = new ErrorDataResult<AdminDTO>("Admin Ekleme Başarısız" + ex);
                transactionScope.Rollback();
            }
            finally
            {
                transactionScope.Dispose();
            }
        });
        return result;
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var deletingAdmin = await _adminRepostory.GetByIdAsync(id);
        
        if (deletingAdmin == null)
        {
            return new ErrorResult("Silinecek veri bulunamadı.");
        }
        await _adminRepostory.DeleteAsync(deletingAdmin);
        await _accountService.DeleteUserAsync(deletingAdmin.IdentityId);
        await _adminRepostory.SaveChangesAsync();
        return new SuccessResult("Admin silme işlemi başarılı.");
    }

    public async Task<IDataResult<List<AdminListDTO>>> GetAllAsync()
    {
        var admins = await _adminRepostory.GetAllAsync();
        var adminDTOs = admins.Adapt<List<AdminListDTO>>();
        if (admins.Count() >= 0)
        {
            return new SuccessDataResult<List<AdminListDTO>>(adminDTOs, "Admin listeleme başarılı");
        }
        return new ErrorDataResult<List<AdminListDTO>>(adminDTOs, "Görüntülenecek admin bulunamadı.");
    }

    public async Task<IDataResult<AdminDTO>> GetByIdAsync(Guid id)
    {
        var admin = await _adminRepostory.GetByIdAsync(id);
        if (admin == null)
        {
            new ErrorDataResult<AdminDTO>("Gösterilecek Admin Bulunamadı.");
        }
        var adminDto = admin.Adapt<AdminDTO>();
        return new SuccessDataResult<AdminDTO>(adminDto, "Admin gösterme başarılı.");
    }

    public async Task<IDataResult<AdminDTO>> UpdateAsync(AdminUpdateDTO adminUpdateDTO)
    {
        var updatingAdmin = await _adminRepostory.GetByIdAsync(adminUpdateDTO.Id);
        if (updatingAdmin is null)
        {
            return new ErrorDataResult<AdminDTO>("Güncellenecek Admin Bulunamadı.");
        }
        if (updatingAdmin.Email != adminUpdateDTO.Email && await _accountService.AnyAsync(x => x.Email == adminUpdateDTO.Email))
        {
            return new ErrorDataResult<AdminDTO>("Güncellenecek Email zaten kullanılıyor!");
        }


        var identityUser = await _accountService.FindByIdAsync(updatingAdmin.IdentityId);
        identityUser.Email = adminUpdateDTO.Email;
        identityUser.NormalizedEmail = adminUpdateDTO.Email.ToUpperInvariant();
        identityUser.UserName = adminUpdateDTO.Email;
        identityUser.NormalizedEmail = adminUpdateDTO.Email.ToUpperInvariant();

        DataResult<AdminDTO> result = new ErrorDataResult<AdminDTO>();
        var strategy = await _adminRepostory.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _adminRepostory.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var identityResult = await _accountService.UpdateUserAsync(identityUser);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<AdminDTO>(identityResult.ToString());
                    transactionScope.Rollback();
                    return;
                }

                var updatedAdmin = adminUpdateDTO.Adapt(updatingAdmin);

                await _adminRepostory.UpdateAsync(updatedAdmin);
                await _adminRepostory.SaveChangesAsync();
                result = new SuccessDataResult<AdminDTO>("Admin Güncelleme Başarılı");
                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<AdminDTO>("Admin Güncelleme Başarısız" + ex.Message);
                transactionScope.Rollback();
            }
            finally
            {
                transactionScope.Dispose();
            }
        });
        return result;
    }
}
