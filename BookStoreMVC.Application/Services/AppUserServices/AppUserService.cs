using BookStoreMVC.Application.DTOs.AdminDTOs;

namespace BookStoreMVC.Application.Services.AppUserServices;

public class AppUserService : IAppUserService
{
    private readonly IAccountService _accountService;
    private readonly IAppUserRepostory _appUserRepostory;

    public AppUserService(IAccountService accountService, IAppUserRepostory appUserRepostory)
    {
        _accountService = accountService;
        _appUserRepostory = appUserRepostory;
    }


    public async Task<IDataResult<AppUserDTO>> CreateAsync(AppUserCreateDTO appUserCreateDTO)
    {
        if( await _accountService.AnyAsync(x=> x.Email == appUserCreateDTO.Email) )
        {
            return new ErrorDataResult<AppUserDTO>("Email Kullanılıyor");
        }

        IdentityUser identityUser = new IdentityUser()
        {
            Email = appUserCreateDTO.Email,
            NormalizedEmail = appUserCreateDTO.Email.ToUpperInvariant(),
            UserName = appUserCreateDTO.Email,
            NormalizedUserName = appUserCreateDTO.Email.ToUpperInvariant(),
            EmailConfirmed=true
        };

        DataResult<AppUserDTO> result = new ErrorDataResult<AppUserDTO>();

        var strategy = await _appUserRepostory.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _appUserRepostory.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var identityResult = await _accountService.CreateUserAsync(identityUser, Domain.Enums.Roles.AppUser);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<AppUserDTO>(identityResult.ToString());
                    transactionScope.Rollback();
                    return;
                }
                var appUser = appUserCreateDTO.Adapt<AppUser>();
                appUser.IdentityId = identityUser.Id;
                await _appUserRepostory.AddAsync(appUser);
                await _appUserRepostory.SaveChangesAsync();
                result = new SuccessDataResult<AppUserDTO>("Kullanıcı Ekleme Başarılı");
                transactionScope.Commit();
            }
            catch (Exception ex)
            {

                result = new ErrorDataResult<AppUserDTO>("Ekleme Başarısız" + ex);
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
        var deletingAppUser = await _appUserRepostory.GetByIdAsync(id);
        if (deletingAppUser == null)
        {
            return new ErrorResult("Silinecek veri bulunamadı.");
        }
        await _accountService.DeleteUserAsync(deletingAppUser.IdentityId);
        await _appUserRepostory.DeleteAsync(deletingAppUser);
        await _appUserRepostory.SaveChangesAsync();
        return new SuccessResult("Kullanıcı silme işlemi başarılı.");
    }

    public async Task<IDataResult<List<AppUserListDTO>>> GetAllAsync()
    {
        var appUsers = await _appUserRepostory.GetAllAsync();
        var appUserDTOs = appUsers.Adapt<List<AppUserListDTO>>();
        if(appUsers.Count()>= 0)
        {
            return new SuccessDataResult<List<AppUserListDTO>>(appUserDTOs, "Kullanıcı listeleme başarılı");
        }
        return new ErrorDataResult<List<AppUserListDTO>>(appUserDTOs, "Görüntülenecek kullanıcı bulunamadı.");
    }

    public async Task<IDataResult<AppUserDTO>> GetByIdAsync(Guid id)
    {
        var appUser = await _appUserRepostory.GetByIdAsync(id);
        if (appUser == null)
        {
            new ErrorDataResult<AppUserDTO>("Gösterilecek Kullanıcı Bulunamadı.");
        }
        var appUserDto = appUser.Adapt<AppUserDTO>();
        return new SuccessDataResult<AppUserDTO>(appUserDto, "Kullanıcı gösterme başarılı.");
    }

    public async Task<IDataResult<AppUserDTO>> UpdateAsync(AppUserUpdateDTO appUserUpdateDTO)
    {
        var updatingAppUser = await _appUserRepostory.GetByIdAsync(appUserUpdateDTO.Id);
        if (updatingAppUser is null)
        {
            return new ErrorDataResult<AppUserDTO>("Güncellenecek Kullanıcı Bulunamadı.");
        }
        if (updatingAppUser.Email != appUserUpdateDTO.Email && await _accountService.AnyAsync(x => x.Email == appUserUpdateDTO.Email))
        {
            return new ErrorDataResult<AppUserDTO>("Güncellenecek Email zaten kullanılıyor!");
        }

        var identityUser = await _accountService.FindByIdAsync(updatingAppUser.IdentityId);
        identityUser.Email = appUserUpdateDTO.Email;
        identityUser.NormalizedEmail = appUserUpdateDTO.Email.ToUpperInvariant();
        identityUser.UserName = appUserUpdateDTO.Email;
        identityUser.NormalizedEmail = appUserUpdateDTO.Email.ToUpperInvariant();

        DataResult<AppUserDTO> result = new ErrorDataResult<AppUserDTO>();
        var strategy = await _appUserRepostory.CreateExecutionStrategy();
        await strategy.ExecuteAsync(async () =>
        {
            var transactionScope = await _appUserRepostory.BeginTransactionAsync().ConfigureAwait(false);
            try
            {
                var identityResult = await _accountService.UpdateUserAsync(identityUser);
                if (!identityResult.Succeeded)
                {
                    result = new ErrorDataResult<AppUserDTO>(identityResult.ToString());
                    transactionScope.Rollback();
                    return;
                }

                var updatedAppUser = appUserUpdateDTO.Adapt(updatingAppUser);

                await _appUserRepostory.UpdateAsync(updatedAppUser);
                await _appUserRepostory.SaveChangesAsync();
                result = new SuccessDataResult<AppUserDTO>("Kullanıcı Güncelleme Başarılı");
                transactionScope.Commit();
            }
            catch (Exception ex)
            {
                result = new ErrorDataResult<AppUserDTO>("Kullanıcı Güncelleme Başarısız" + ex.Message);
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
