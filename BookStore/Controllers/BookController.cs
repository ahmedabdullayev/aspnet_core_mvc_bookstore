using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class BookController : Controller
    {
        private readonly BookRepository _bookRepository;

        public BookController(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        
        public async Task<ViewResult> GetAllBooks()
        {
            var data = await _bookRepository.GetAllBooks();
            
            return View(data);
        }
        
        public ViewResult GetBook(int id, string title)
        {
            dynamic data = new System.Dynamic.ExpandoObject();
            data.book = _bookRepository.GetBookById(id);
            data.Name = "Ahmad";
            return View(data);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }

        public ViewResult AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            int id = await _bookRepository.AddNewBook(bookModel);
            if (id > 0)
            {
                return RedirectToAction("AddNewBook", new {isSuccess = true, bookId = id});
            }
            return View();
        }
    }
}