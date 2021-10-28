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
                        Language = book.Language,
                        Title = book.Title,
                        TotalPages = book.TotalPages,
                    });
                }
            }
            return books;
        }

        public async Task<BookModel> GetBookById(int id)
        {
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                var bookModel = new BookModel
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = book.Author,
                    Description = book.Description,
                    Category = book.Category,
                    Language = book.Language,
                    TotalPages = book.TotalPages,

                };
                return bookModel;
            }

            return null;

            // return DataSource().Where(i => i.Id == id).FirstOrDefault();
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(i => i.Title.Contains(title) || i.Author.Contains(authorName)).ToList();
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel() { 
                    Id = 1, Title = "MVC", Author = "Winny", Description = "Description for the MVC book", 
                    Category = "Programming", Language = "English", TotalPages = 322
                },
                new BookModel()
                {
                    Id = 2, Title = "C#", Author = "Winny", Description = "Description for the C# book",
                    Category = "Programming", Language = "Portugal", TotalPages = 765
                },
                new BookModel()
                {
                    Id = 3, Title = "Java", Author = "Peeter", Description = "Description for the Java book",
                    Category = "Programming", Language = "Deutsch", TotalPages = 543
                },
                new BookModel()
                {
                    Id = 4, Title = "PHP", Author = "Monika", Description = "Description for the PHP book",
                    Category = "Programming", Language = "Spanish", TotalPages = 313
                },
            };
        }
    }
}