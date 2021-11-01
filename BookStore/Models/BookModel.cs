using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using BookStore.Enums;
using BookStore.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BookStore.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        // [StringLength(100, MinimumLength = 3)]
        // [Required(ErrorMessage = "Please enter the title of your book")]
        // [MyCustomValidation("azure", Text = "mvc", ErrorMessage = "Error Message")]
        public string Title { get; set; }
        [Required(ErrorMessage = "Please enter the title of your book")]
        public string Author { get; set; }
        [StringLength(255, MinimumLength = 5)]
        public string Description { get; set; }
        public string Category { get; set; }
        [Display(Name = "Language of the book")]
        [Required(ErrorMessage = "Please choose language for your book")]
        public int? LanguageId { get; set; }
        public string Language { get; set; }
        [Required(ErrorMessage = "Please enter the total pages")]
        [Display(Name = "Total pages of book")]
        public int? TotalPages { get; set; }

        [Display(Name = "Choose cover photo of your image")]
        [Required]
        public IFormFile CoverPhoto { get; set; } // image file
        
        //url for main img wwwroot/books/cover
        public string CoverImageUrl { get; set; }
        
        [Display(Name = "Choose the gallery images of your image")]
        [Required]
        public IFormFileCollection GalleryFiles { get; set; } // Images of one book

        public List<GalleryModel> Gallery { get; set; }
        
        [Display(Name = "Upload pdf of the book")]
        [Required]
        public IFormFile BookPdf { get; set; } // image file
        
        //url for main img wwwroot/books/cover
        public string BookPdfUrl { get; set; }
        
        //just for dropdown of languages
        public List<LanguageModel> Languages { get; set; }
        

    }
}