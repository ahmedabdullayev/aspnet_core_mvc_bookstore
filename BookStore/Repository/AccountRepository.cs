using System;
using System.Threading.Tasks;
using BookStore.Models;
using Microsoft.AspNetCore.Identity;

namespace BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        
        public async Task<IdentityResult> CreateUserAsync(SignUpUserModel userModel)
        {
            var user = new ApplicationUser()
            {
                Email = userModel.Email,
                UserName = userModel.Email,
                FirstName = userModel.FirstName,
                LastName = userModel.LastName,
            };
          var result = await _userManager.CreateAsync(user, userModel.Password);
          Console.WriteLine(result.Succeeded);
          return result;
        }

        public async Task<SignInResult> PasswordSignInUserAsync(SignInModel signInModel)
        {
           var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, false);
           return result;
        }

        public async Task SignOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }
}