using BookStoreMVC.Application.DTOs.AuthorDTOs;
using BookStoreMVC.Application.DTOs.CategoryDTOs;
using BookStoreMVC.Domain.Entities;
using BookStoreMVC.Domain.Utilities.Concretes;
using BookStoreMVC.Domain.Utilities.Interfaces;
using BookStoreMVC.Infrastructure.Repositories.AuthorRepostories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Application.Services.AuthorServices;

public class AuthorService : IAuthorService
{
    private readonly IAuthorRepostory authorRepostory;

    public AuthorService(IAuthorRepostory authorRepostory)
    {
        this.authorRepostory = authorRepostory;
    }

    public async Task<IDataResult<AuthorDTO>> CreateAsync(AuthorCreateDTO authorCreateDTO)
    {
        if (await authorRepostory.AnyAsnc(x => x.Name.ToLower() == authorCreateDTO.Name.ToLower()))
        {
            return new ErrorDataResult<AuthorDTO>("Mevcut Yazar Sistemde Kayıtlı");
        }
        var newAuthor = authorCreateDTO.Adapt<Author>();
        await authorRepostory.AddAsync(newAuthor);
        await authorRepostory.SaveChangesAsync();
        var authorDTO = newAuthor.Adapt<AuthorDTO>();
        return new SuccessDataResult<AuthorDTO>(authorDTO, "Yazar ekleme başarılı.");
    }

    public async Task<IResult> DeleteAsync(Guid id)
    {
        var deletingAuthor = await authorRepostory.GetByIdAsync(id);
        if (deletingAuthor == null)
        {
            return new ErrorResult("Silinecek veri bulunamadı.");
        }
        await authorRepostory.DeleteAsync(deletingAuthor);
        await authorRepostory.SaveChangesAsync();
        return new SuccessResult("Yazar silme işlemi başarılı.");
    }

    public async Task<IDataResult<List<AuthorListDTO>>> GetAllAsync()
    {
        var authors = await authorRepostory.GetAllAsync();
        if (authors.Count() <= 0)
        {
            //Metodun dönüş tipi IDataResult olduğu için ErrorResult yerine ErrorDataResult dönüyoruz.
            return new ErrorDataResult<List<AuthorListDTO>>("Listelenecek Yazar Bulunamadı.");
        }
        var authorListDtos = authors.Adapt<List<AuthorListDTO>>();
        return new SuccessDataResult<List<AuthorListDTO>>(authorListDtos, "Yazar listeleme başarılı.");
    }

    public async Task<IDataResult<AuthorDTO>> GetByIdAsync(Guid id)
    {
        var author = await authorRepostory.GetByIdAsync(id);
        if (author == null)
        {
            new ErrorDataResult<AuthorDTO>("Gösterilecek Yazar Bulunamadı.");
        }
        var authorDto = author.Adapt<AuthorDTO>();
        return new SuccessDataResult<AuthorDTO>(authorDto, "Yazar gösterme başarılı.");
    }

    public async Task<IDataResult<AuthorDTO>> UpdateAsync(AuthorUpdateDTO authorUpdateDTO)
    {
        var updatingAuthor = await authorRepostory.GetByIdAsync(authorUpdateDTO.Id);
        if (updatingAuthor is null)
        {
            return new ErrorDataResult<AuthorDTO>("Güncellenecek Yazar Bulunamadı.");
        }


        //mevcut nesneye mapleneceği zaman parantez içinde yazmamız yeterli
        var updatedAuthor = authorUpdateDTO.Adapt(updatingAuthor);
        await authorRepostory.UpdateAsync(updatedAuthor);
        await authorRepostory.SaveChangesAsync();
        return new SuccessDataResult<AuthorDTO>(updatedAuthor.Adapt<AuthorDTO>(), "Kategori Güncelleme Başarılı.");
    }
}
