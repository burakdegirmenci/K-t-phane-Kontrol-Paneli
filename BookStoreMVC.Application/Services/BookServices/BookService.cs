using BookStoreMVC.Application.DTOs.BookDTOs;
using BookStoreMVC.Application.DTOs.CategoryDTOs;
using BookStoreMVC.Domain.Entities;
using BookStoreMVC.Domain.Utilities.Concretes;
using BookStoreMVC.Domain.Utilities.Interfaces;
using BookStoreMVC.Infrastructure.Repositories.BookRepostories;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace BookStoreMVC.Application.Services.BookServices
{
    internal class BookService : IBookService
    {
        private readonly IBookRepostory bookRepostory;
        

        public BookService(IBookRepostory bookRepostory)
        {
            this.bookRepostory = bookRepostory;
            
        }

        public async Task<IDataResult<BookDTO>> CreateAsync(BookCreateDTO bookCreateDTO)
        {
            var newBook = bookCreateDTO.Adapt<Book>();
            await bookRepostory.AddAsync(newBook);
            await bookRepostory.SaveChangesAsync();
            return new SuccessDataResult<BookDTO>(newBook.Adapt<BookDTO>(), "Kitap Ekleme Başarılı");
        }

        public async Task<IResult> DeleteAsync(Guid id)
        {
            var deletingBook = await bookRepostory.GetByIdAsync(id);
            if (deletingBook == null)
            {
                return new ErrorResult("Silinecek veri bulunamadı.");
            }
            await bookRepostory.DeleteAsync(deletingBook);
            await bookRepostory.SaveChangesAsync();
            return new SuccessResult("Kitap silme işlemi başarılı.");
        }

        public async Task<IDataResult<List<BookListDTO>>> GetAllAsync()
        {
            var books = await bookRepostory.GetAllAsync();
            if(books.Count()<=0)
            {
                return new ErrorDataResult<List<BookListDTO>>(books.Adapt<List<BookListDTO>>(), "Kitap Listeleme Başarılı");
            }
            return new SuccessDataResult<List<BookListDTO>>(books.Adapt<List<BookListDTO>>(), "Kitap Listeleme Başarılı");
        }

        public async Task<IDataResult<List<BookListDTO>>> GetAllBooksForUserAsync()
        {
            var books = await bookRepostory.GetAllAsync(x=> x.IsAvailable==true);
            if (books.Count() <= 0)
            {
                return new ErrorDataResult<List<BookListDTO>>(books.Adapt<List<BookListDTO>>(), "Kitap Listeleme Başarılı");
            }
            return new SuccessDataResult<List<BookListDTO>>(books.Adapt<List<BookListDTO>>(), "Kitap Listeleme Başarılı");
        }

        public async Task<IDataResult<BookDTO>> GetByIdAsync(Guid id)
        {
            var book = await bookRepostory.GetByIdAsync(id);
            if (book == null)
            {
                new ErrorDataResult<CategoryDTO>("Gösterilecek Kitap Bulunamadı.");
            }
            var bookDto = book.Adapt<BookDTO>();
            return new SuccessDataResult<BookDTO>(bookDto, "Kitap gösterme başarılı.");
        }

        public async Task<IDataResult<BookDTO>> UpdateAsync(BookUpdateDTO bookUpdateDTO)
        {
            var updatingBook = await bookRepostory.GetByIdAsync(bookUpdateDTO.Id);
            if (updatingBook is null)
            {
                return new ErrorDataResult<BookDTO>("Güncellenecek Kitap Bulunamadı.");
            }

            //mevcut nesneye mapleneceği zaman parantez içinde yazmamız yeterli
            var updatedBook = bookUpdateDTO.Adapt(updatingBook);
            //if ((await bookRepostory.GetAllAsync()).Any(x => x.Name == bookUpdateDTO.Name && x.PublisherId == bookUpdateDTO.PublisherId && x.CategoryId == bookUpdateDTO.CategoryId && x.AuthorId == bookUpdateDTO.AuthorId))
            //{
            //    return new ErrorDataResult<BookDTO>(updatedBook.Adapt<BookDTO>(), "Mevcut kitap zaten eklenmiş.");
            //}
            await bookRepostory.UpdateAsync(updatedBook);
            await bookRepostory.SaveChangesAsync();
            return new SuccessDataResult<BookDTO>(updatedBook.Adapt<BookDTO>(), "Kitap Güncelleme Başarılı.");
        }

        public async Task UpdateAvailability(Guid id, bool isAvailable)
        {
            var book = await bookRepostory.GetByIdAsync(id);
            book.IsAvailable=isAvailable;
            await bookRepostory.UpdateAsync(book);
            await bookRepostory.SaveChangesAsync();
        }


    }
}
