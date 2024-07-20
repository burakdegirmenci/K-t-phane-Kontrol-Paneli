using BookStoreMVC.Application.DTOs.CategoryDTOs;
using BookStoreMVC.Domain.Entities;
using BookStoreMVC.Domain.Enums;
using BookStoreMVC.Domain.Utilities.Concretes;
using BookStoreMVC.Domain.Utilities.Interfaces;
using BookStoreMVC.Infrastructure.Repositories.CategoryRepostories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStoreMVC.Application.Services.CategoryServices
{
    
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepostory _categoryRepostory;
        public CategoryService(ICategoryRepostory categoryRepostory)
        {
            _categoryRepostory = categoryRepostory;
        }
        public async Task<IDataResult<CategoryDTO>> CreateAsync(CategoryCreateDTO categoryCreateDTO)
        {
            if(await _categoryRepostory.AnyAsnc(x=> x.Name.ToLower() == categoryCreateDTO.Name.ToLower()))
            {
                return new ErrorDataResult<CategoryDTO>("Mevcut Kategori Sistemde Kayıtlı");
            }
            var newCategory = categoryCreateDTO.Adapt<Category>();
            await _categoryRepostory.AddAsync(newCategory);
            await _categoryRepostory.SaveChangesAsync();
            var categoryDTO = newCategory.Adapt<CategoryDTO>();
            return new SuccessDataResult<CategoryDTO>(categoryDTO, "Kategori ekleme başarılı.");
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingCategory = await _categoryRepostory.GetByIdAsync(id);
            if(deletingCategory == null)
            {
                return new ErrorResult("Silinecek veri bulunamadı.");
            }
            await _categoryRepostory.DeleteAsync(deletingCategory);
            await _categoryRepostory.SaveChangesAsync();
            return new SuccessResult("Kategori silme işlemi başarılı.");
        }

        public async Task<IDataResult<List<CategoryListDTO>>> GetAllAsync()
        {
            var categories = await _categoryRepostory.GetAllAsync();
			var categoryListDtos = categories.Adapt<List<CategoryListDTO>>();
			if (categories.Count()<=0)
            {
                //Metodun dönüş tipi IDataResult olduğu için ErrorResult yerine ErrorDataResult dönüyoruz.
                return new ErrorDataResult<List<CategoryListDTO>>(categoryListDtos,"Listelenecek Kategori Bulunamadı.");
            }
            
            return new SuccessDataResult<List<CategoryListDTO>>(categoryListDtos, "Kategori listeleme başarılı.");
        }

        public async Task<IDataResult<CategoryDTO>> GetByIdAsync(Guid id)
        {
            var category = await _categoryRepostory.GetByIdAsync(id);
            if (category == null)
            {
                new ErrorDataResult<CategoryDTO>("Gösterilecek Kategori Bulunamadı.");
            }
            var categoryDto = category.Adapt<CategoryDTO>();
            return new SuccessDataResult<CategoryDTO>(categoryDto, "Kategori gösterme başarılı.");
        }

        public async Task<IDataResult<CategoryDTO>> UpdateAsync(CategoryUpdateDTO categoryUpdateDTO)
        {
            var updatingCategory = await _categoryRepostory.GetByIdAsync(categoryUpdateDTO.Id);
            if(updatingCategory is null)
            {
                return new ErrorDataResult<CategoryDTO>("Güncellenecek Kategori Bulunamadı.");
            }

          


            //mevcut nesneye mapleneceği zaman parantez içinde yazmamız yeterli
            var updatedCategory = categoryUpdateDTO.Adapt(updatingCategory);
            if( (await _categoryRepostory.GetAllAsync()).Any(x=> x.Name == categoryUpdateDTO.Name ))
            {
                return new ErrorDataResult<CategoryDTO>(updatedCategory.Adapt<CategoryDTO>(), "Mevcut kategori zaten eklenmiş.");
            }
            await _categoryRepostory.UpdateAsync(updatedCategory);
            await _categoryRepostory.SaveChangesAsync();
            return new SuccessDataResult<CategoryDTO>(updatedCategory.Adapt<CategoryDTO>(), "Kategori Güncelleme Başarılı.");
        }
    }
}
