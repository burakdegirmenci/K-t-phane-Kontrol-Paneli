using BookStoreMVC.Application.DTOs.AppUserDTOs;
using BookStoreMVC.Application.DTOs.AuthorDTOs;
using BookStoreMVC.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Application.Services.AppUserServices;

public interface IAppUserService
{
    Task<IDataResult<List<AppUserListDTO>>> GetAllAsync();
    Task<IDataResult<AppUserDTO>> CreateAsync(AppUserCreateDTO appUserCreateDTO);
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<AppUserDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<AppUserDTO>> UpdateAsync(AppUserUpdateDTO appUserUpdateDTO);
}
