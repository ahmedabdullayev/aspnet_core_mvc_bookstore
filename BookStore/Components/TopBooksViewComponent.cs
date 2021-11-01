using System.Threading.Tasks;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Components
{
    public class TopBooksViewComponent : ViewComponent
    {
        private readonly BookRepository _bookRepository;
        public TopBooksViewComponent(BookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        public async Task<IViewComponentResult> InvokeAsync(int count) // in Index.html
        {
            var books = await _bookRepository.GetTopBooksAsync(count);
            return View(books);
        }
    }
}