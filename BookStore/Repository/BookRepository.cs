using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using BookStore.Data;
using BookStore.Models;
using BookStore.Services;
using Microsoft.EntityFrameworkCore;

namespace BookStore.Repository
{
    public class BookRepository : IBookRepository
    {
        private readonly BookStoreContext _context;
        private readonly IUserService _userService;

        public BookRepository(BookStoreContext context, IUserService userService)
        {
            _context = context;
            _userService = userService;
        }
        public async Task<int> AddNewBook(BookModel model)
        {
            var newBook = new Books()
            {
                Author = model.Author,
                Description = model.Description,
                Title = model.Title,
                LanguageId = model.LanguageId ?? 0,
                ApplicationUserId = _userService.GetUserId(),
                TotalPages = model.TotalPages ?? 0,
                CoverImageUrl = model.CoverImageUrl,
                BookPdfUrl = model.BookPdfUrl,
                CreatedOn = DateTime.UtcNow,
                UpdatedOn = DateTime.UtcNow,
            };
            newBook.BookGallery = new List<BookGallery>();
            foreach (var file in model.Gallery)
            {
                newBook.BookGallery.Add(new BookGallery()
                {
                    Name = file.Name,
                    URL = file.URL
                });
            }
            
            
           await _context.Books.AddAsync(newBook);
           await _context.SaveChangesAsync();

            return newBook.Id;
        }
        public async Task<List<BookModel>> GetAllBooks()
        {
            var books = new List<BookModel>();
            var allBooks = await _context.Books.Include(l => l.Language).ToListAsync();
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
                        Language = book.Language.Name,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                        CoverImageUrl = book.CoverImageUrl,
                    });
                }
            }
            return books;
        }
        public async Task<List<BookModel>> GetTopBooksAsync(int count)
        {
            return await _context.Books
                .Select(book => new BookModel()
                {
                    Author = book.Author,
                    Category = book.Category,
                    Description = book.Description,
                    Id = book.Id,
                    LanguageId = book.LanguageId,
                    Language = book.Language.Name,
                    Title = book.Title,
                    TotalPages = book.TotalPages,
                    CoverImageUrl = book.CoverImageUrl,
                }).Take(count).ToListAsync();
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
                    Gallery = book.BookGallery.Select(g => new GalleryModel()
                    {
                        Id    = g.Id,
                        Name = g.Name,
                        URL = g.URL
                    }).ToList(),
                    BookPdfUrl = book.BookPdfUrl
                }).FirstOrDefaultAsync();
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return null;
        }

        public string GetAppName()
        {
            return "Book store App";
        }
    }
}