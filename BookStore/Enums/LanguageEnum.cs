using System.ComponentModel.DataAnnotations;

namespace BookStore.Enums
{
    public enum LanguageEnum
    {
        English = 1,
        [Display(Name = "Brazilian/Spanish")]
        Spanish = 2,
        Estonian = 3,
    }
}