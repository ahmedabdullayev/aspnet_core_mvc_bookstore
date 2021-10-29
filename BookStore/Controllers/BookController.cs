using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using BookStore.ViewModels;
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
        
        public async Task<ViewResult> GetBook(int id, string title)
        {
            dynamic data = new System.Dynamic.ExpandoObject();
            data.book = await _bookRepository.GetBookById(id);
            data.Name = "Ahmad";
            //var obj = await _bookRepository.GetBookById(id);
            
            return View(data);
        }

        public List<BookModel> SearchBooks(string bookName, string authorName)
        {
            return _bookRepository.SearchBook(bookName, authorName);
        }

        public ViewResult AddNewBook(bool isSuccess = false, int bookId = 0)
        {
            // var model = new BookModel()
            // {
            //     Language = "English"
            // };
            ViewBag.Language = new List<string>() {"English", "Spanish", "Estonian"};
            var bookModel = new BookModel();
            bookModel.Languages = new List<string>()
            {
                "English", "Estonian", "French"
            };
            ViewBag.IsSuccess = isSuccess;
            ViewBag.BookId = bookId;
            return View(bookModel);
        }

        [HttpPost]
        public async Task<IActionResult> AddNewBook(BookModel bookModel)
        {
            if (ModelState.IsValid)
            {
                int id = await _bookRepository.AddNewBook(bookModel);
                if (id > 0)
                {
                    return RedirectToAction("AddNewBook", new {isSuccess = true, bookId = id});
                }
            }
            else
            {
                
            }
            // ViewBag.IsSuccess = false;
            // ViewBag.BookId = 0;
            ModelState.AddModelError("", "This is my custom error msg");
            return View();
        }
    }
}