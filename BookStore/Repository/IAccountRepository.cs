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
        Task<IdentityResult> ChangeUserPasswordAsync(ChangePasswordModel model);

        Task<IdentityResult> ConfirmUserEmailAsync(string uid, string token);

        Task GenerateUserEmailConfirmationTokenAsync(ApplicationUser user);

        Task<ApplicationUser> GetUserByEmailAsync(string email);
    }
}