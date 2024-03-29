using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace BookStore.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager; 
        private readonly IUserService _userService;
        private readonly IEmailService _emailService;
        private readonly IConfiguration _configuration;

        public AccountRepository(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IUserService userService, IEmailService emailService, IConfiguration configuration, RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userService = userService;
            _emailService = emailService;
            _configuration = configuration;
            _roleManager = roleManager;
        }

        public async Task<ApplicationUser> GetUserByEmailAsync(string email)
        {
            return await _userManager.FindByEmailAsync(email);
        }

        public string GetEmail()
        {
            return _userService.GetUserEmail();
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
          if (result.Succeeded)
          {
              await GenerateUserEmailConfirmationTokenAsync(user);
          }
          Console.WriteLine(result.Succeeded);
          return result;
        }

        private async Task<IdentityResult> GiveUserRoleToUser(ApplicationUser user)
        {
            var userRoleExistsAsync = await _roleManager.RoleExistsAsync("user");
            if (!userRoleExistsAsync)
            { 
                await _roleManager.CreateAsync(new IdentityRole("user"));
            }

            return await _userManager.AddToRoleAsync(user, "user");
        }
        public async Task GenerateUserEmailConfirmationTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);
            if(!string.IsNullOrEmpty(token))
            {
                await SendEmailConfirmationLink(user, token);
            }
        }

        public async Task GenerateForgotPasswordTokenAsync(ApplicationUser user)
        {
            var token = await _userManager.GeneratePasswordResetTokenAsync(user);
            if(!string.IsNullOrEmpty(token))
            {
                await SendForgotPasswordEmail(user, token);
            }
        }
        
        public async Task<SignInResult> PasswordSignInUserAsync(SignInModel signInModel)
        {
           var result = await _signInManager.PasswordSignInAsync(signInModel.Email, signInModel.Password, signInModel.RememberMe, true);
           return result;
        }

        public async Task SignOutUserAsync()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<IdentityResult> ChangeUserPasswordAsync(ChangePasswordModel model)
        {
            var userId = _userService.GetUserId();
            var user = await _userManager.FindByIdAsync(userId);
            return await _userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
        }
        
        public async Task<IdentityResult> ConfirmUserEmailAsync(string uid, string token)
        {
            await _userManager.ConfirmEmailAsync(await _userManager.FindByIdAsync(uid), token);
            var findByIdAsync = await _userManager.FindByIdAsync(uid);
            return await GiveUserRoleToUser(findByIdAsync);
        }

        public async Task<IdentityResult> ResetUserPasswordAsync(ResetPasswordModel resetPasswordModel)
        {
            var user = await _userManager.FindByIdAsync(resetPasswordModel.UserId);
            return await _userManager.ResetPasswordAsync(user, resetPasswordModel.Token,
                resetPasswordModel.NewPassword);
        }
        private async Task SendEmailConfirmationLink(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:EmailConfirmation").Value;
            var format = string.Format(appDomain + confirmationLink, user.Id, token);
            Console.WriteLine("here: " + format);
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() {user.Email},
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new ("{{Link}}", string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };
            await _emailService.SendEmailForConfirmation(options);
        }
        
        private async Task SendForgotPasswordEmail(ApplicationUser user, string token)
        {
            string appDomain = _configuration.GetSection("Application:AppDomain").Value;
            string confirmationLink = _configuration.GetSection("Application:ForgotPassword").Value;
            var format = string.Format(appDomain + confirmationLink, user.Id, token);
            Console.WriteLine("here: " + format);
            UserEmailOptions options = new UserEmailOptions
            {
                ToEmails = new List<string>() {user.Email},
                PlaceHolders = new List<KeyValuePair<string, string>>()
                {
                    new KeyValuePair<string, string>("{{UserName}}", user.FirstName),
                    new ("{{Link}}", string.Format(appDomain + confirmationLink, user.Id, token))
                }
            };
            await _emailService.SendEmailForForgotPassword(options);
        }
    }
}