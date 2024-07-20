using BookStoreMVC.Application.DTOs.CategoryDTOs;
using BookStoreMVC.Application.DTOs.PublisherDTOs;
using BookStoreMVC.Domain.Entities;
using BookStoreMVC.Domain.Utilities.Concretes;
using BookStoreMVC.Domain.Utilities.Interfaces;
using BookStoreMVC.Infrastructure.Repositories.PublisherRepostories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Application.Services.PublisherServices;

public class PublisherService : IPublisherServices
{
	private readonly IPublisherRepostory publisherRepostory;

	public PublisherService(IPublisherRepostory publisherRepostory)
	{
		this.publisherRepostory = publisherRepostory;
	}

	public async Task<IDataResult<PublisherDTO>> CreateAsync(PublisherCreateDTO publisherCreateDTO)
	{
		if (await publisherRepostory.AnyAsnc(x => x.Name.ToLower() == publisherCreateDTO.Name.ToLower()))
		{
			return new ErrorDataResult<PublisherDTO>("Mevcut Yayınevi Sistemde Kayıtlı");
		}
		var newPublisher = publisherCreateDTO.Adapt<Publisher>();
		await publisherRepostory.AddAsync(newPublisher);
		await publisherRepostory.SaveChangesAsync();
		var publisherDTO = newPublisher.Adapt<PublisherDTO>();
		return new SuccessDataResult<PublisherDTO>(publisherDTO, "Yayınevi ekleme başarılı.");
	}

	public async Task<IResult> DeleteAsync(Guid id)
	{
		var deletingPublisher = await publisherRepostory.GetByIdAsync(id);
		if (deletingPublisher == null)
		{
			return new ErrorResult("Silinecek veri bulunamadı.");
		}
		await publisherRepostory.DeleteAsync(deletingPublisher);
		await publisherRepostory.SaveChangesAsync();
		return new SuccessResult("Yayınevi silme işlemi başarılı.");
	}

	public async Task<IDataResult<List<PublisherListDTO>>> GetAllAsync()
	{
		var publishers = await publisherRepostory.GetAllAsync();
        var publisherListDtos = publishers.Adapt<List<PublisherListDTO>>();
        if (publishers.Count() <= 0)
		{
			//Metodun dönüş tipi IDataResult olduğu için ErrorResult yerine ErrorDataResult dönüyoruz.
			return new ErrorDataResult<List<PublisherListDTO>>(publisherListDtos,"Listelenecek Yayınevi Bulunamadı.");
		}
		
		return new SuccessDataResult<List<PublisherListDTO>>(publisherListDtos, "Yayınevi listeleme başarılı.");
	}

	public async Task<IDataResult<PublisherDTO>> GetByIdAsync(Guid id)
	{
		var publisher = await publisherRepostory.GetByIdAsync(id);
		if (publisher == null)
		{
			new ErrorDataResult<PublisherDTO>("Gösterilecek Kategori Bulunamadı.");
		}
		var publisherDto = publisher.Adapt<PublisherDTO>();
		return new SuccessDataResult<PublisherDTO>(publisherDto, "Yayınevi gösterme başarılı.");
	}

	public async Task<IDataResult<PublisherDTO>> UpdateAsync(PublisherUpdateDTO publisherUpdateDTO)
	{
		var updatingPublisher = await publisherRepostory.GetByIdAsync(publisherUpdateDTO.Id);
		if (updatingPublisher is null)
		{
			return new ErrorDataResult<PublisherDTO>("Güncellenecek Yayınevi Bulunamadı.");
		}


		//mevcut nesneye mapleneceği zaman parantez içinde yazmamız yeterli
		var updatedPublisher = publisherUpdateDTO.Adapt(updatingPublisher);
		await publisherRepostory.UpdateAsync(updatedPublisher);
		await publisherRepostory.SaveChangesAsync();
		return new SuccessDataResult<PublisherDTO>(updatedPublisher.Adapt<PublisherDTO>(), "Yayınevi Güncelleme Başarılı.");
	}
}
