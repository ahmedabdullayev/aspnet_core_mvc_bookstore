using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repository
{
    public interface IAccountRepository
    {
        Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel);
        Task<SignInResult> PasswordSignInUserAsync(SignInModel signInModel);
        Task SignOutUserAsync();
    }
}