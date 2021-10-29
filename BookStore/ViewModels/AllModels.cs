using System.Collections.Generic;
using BookStore.Models;

namespace BookStore.ViewModels
{
    public class AllModels
    {
        public IEnumerable<BookModel> allBooks { get; set; }
    }
}