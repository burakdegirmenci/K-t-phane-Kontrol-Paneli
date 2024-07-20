using BookStoreMVC.Application.DTOs.BookDTOs;
using BookStoreMVC.Application.DTOs.CategoryDTOs;
using BookStoreMVC.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Application.Services.BookServices;

public interface IBookService
{
    Task<IDataResult<BookDTO>> CreateAsync(BookCreateDTO bookCreateDTO);
    Task<IDataResult<List<BookListDTO>>> GetAllAsync();
    Task<IDataResult<List<BookListDTO>>> GetAllBooksForUserAsync();
    Task<IResult> DeleteAsync(Guid id);
    Task<IDataResult<BookDTO>> GetByIdAsync(Guid id);
    Task<IDataResult<BookDTO>> UpdateAsync(BookUpdateDTO bookUpdateDTO);
    Task UpdateAvailability(Guid id, bool isAvailable);
}
