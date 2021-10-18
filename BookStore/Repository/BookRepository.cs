using System.Collections.Generic;
using System.Linq;
using BookStore.Models;

namespace BookStore.Repository
{
    public class BookRepository
    {
        public List<BookModel> GetAllBooks()
        {
            return DataSource();
        }

        public BookModel GetBookById(int id)
        {
            return DataSource().Where(i => i.Id == id).FirstOrDefault();
        }

        public List<BookModel> SearchBook(string title, string authorName)
        {
            return DataSource().Where(i => i.Title.Contains(title) || i.Author.Contains(authorName)).ToList();
        }

        private List<BookModel> DataSource()
        {
            return new List<BookModel>()
            {
                new BookModel() {Id = 1, Title = "MVC", Author = "Winny"},
                new BookModel() {Id = 2, Title = "c#", Author = "Winny"},
                new BookModel() {Id = 3, Title = "Java", Author = "Peeter"},
                new BookModel() {Id = 4, Title = "Php", Author = "Monika"},
            };
        }
    }
}