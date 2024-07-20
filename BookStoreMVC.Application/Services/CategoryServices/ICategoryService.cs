using BookStoreMVC.Application.DTOs.CategoryDTOs;
using BookStoreMVC.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Application.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<IDataResult<CategoryDTO>> CreateAsync (CategoryCreateDTO categoryCreateDTO);
        Task<IDataResult<List<CategoryListDTO>>> GetAllAsync();
        Task<IResult> DeleteAsync(Guid id);
        Task<IDataResult<CategoryDTO>> GetByIdAsync (Guid id);
        Task<IDataResult<CategoryDTO>> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO);
    }
}
