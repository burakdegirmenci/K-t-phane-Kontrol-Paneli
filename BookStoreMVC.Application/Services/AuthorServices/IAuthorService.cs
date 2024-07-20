using BookStoreMVC.Application.DTOs.AuthorDTOs;
using BookStoreMVC.Application.DTOs.CategoryDTOs;
using BookStoreMVC.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Application.Services.AuthorServices;

public interface IAuthorService
{
    Task<IDataResult<AuthorDTO>> CreateAsync(AuthorCreateDTO authorCreateDTO);
    Task<IDataResult<List<AuthorListDTO>>> GetAllAsync();
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<AuthorDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<AuthorDTO>> UpdateAsync(AuthorUpdateDTO authorUpdateDTO);
}
