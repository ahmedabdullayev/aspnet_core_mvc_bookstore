using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;

namespace BookStore.Repository
{
    public interface ILanguageRepository
    {
        Task<List<LanguageModel>> GetLanguages();
    }
}