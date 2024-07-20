using BookStoreMVC.Application.DTOs.CategoryDTOs;
using BookStoreMVC.Application.DTOs.PublisherDTOs;
using BookStoreMVC.Domain.Utilities.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Application.Services.PublisherServices;

public interface IPublisherServices
{
	Task<IDataResult<PublisherDTO>> CreateAsync(PublisherCreateDTO publisherCreateDTO);
	Task<IDataResult<List<PublisherListDTO>>> GetAllAsync();
	Task<IResult> DeleteAsync(Guid id);
	Task<IDataResult<PublisherDTO>> GetByIdAsync(Guid id);
	Task<IDataResult<PublisherDTO>> UpdateAsync(PublisherUpdateDTO publisherUpdateDTO);
}
