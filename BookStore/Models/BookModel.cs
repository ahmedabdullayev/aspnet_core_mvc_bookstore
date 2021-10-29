using System.ComponentModel.DataAnnotations;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        [StringLength(100, MinimumLength = 3)]
        [Required(ErrorMessage = "Please enter the title of your book")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the title of your book")]
        public string Author { get; set; }
        [StringLength(255, MinimumLength = 5)]
        public string Description { get; set; }
        public string Category { get; set; }
        [Required(ErrorMessage = "Please choose language for your book")]
        public string Language { get; set; }
        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name = "Total pages of book")]
        public int? TotalPages { get; set; }
        
    }
}