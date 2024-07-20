using BookStoreMVC.Application.DTOs.AdminDTOs;
using BookStoreMVC.Application.DTOs.AuthorDTOs;
using BookStoreMVC.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Application.Services.AdminServices;

public interface IAdminService
{
    Task<IDataResult<AdminDTO>> CreateAsync(AdminCreateDTO adminCreateDTO);
    Task<IDataResult<List<AdminListDTO>>> GetAllAsync();
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<AdminDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<AdminDTO>> UpdateAsync(AdminUpdateDTO adminUpdateDTO);
}
