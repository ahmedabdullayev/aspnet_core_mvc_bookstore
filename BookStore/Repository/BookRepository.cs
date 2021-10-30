using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository
    {
        private readonly BookStoreContext _context;

        public BookRepository(BookStoreContext context)
        {
            _context = context;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                CreatedOn = DateTime.UtcNow,
                Description = model.Description,
                Title = model.Title,
                LanguageId = model.LanguageId ?? 0,
                TotalPages = model.TotalPages ?? 0,
                UpdatedOn = DateTime.UtcNow,
            };
           await _context.Books.AddAsync(newBook);
           await _context.SaveChangesAsync();

            return newBook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allBooks = await _context.Books.ToListAsync();
            if (allBooks?.Any() == true)
            {
                foreach (var book in allBooks)
                {
                    books.Add(new BookModel()
                    {
                        Author = book.Author,
                        Category = book.Category,
                        Description = book.Description,
                        Id = book.Id,
                        LanguageId = book.LanguageId,
                       // Language = book.Language.Name,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                    });
                }
            }
            return books;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            // var book = await _context.Books.FindAsync(id);
            // if (book != null)
            // {
            //     var bookModel = new BookModel
            //     {
            //         Id = book.Id,
            //         Title = book.Title,
            //         Author = book.Author,
            //         Description = book.Description,
            //         Category = book.Category,
            //         LanguageId = book.LanguageId,
            //         Language = book.Language.Name,
            //         TotalPages = book.TotalPages,
            //
            //     };
            //     return bookModel;

            return await _context.Books.Where(x => x.Id == id)
                .Select(book => new BookModel()
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Category = book.Category,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    TotalPages = book.TotalPages,
                }).FirstOrDefaultAsync();
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return null;
        }

    }
}