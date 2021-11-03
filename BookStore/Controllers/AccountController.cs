using System.Threading.Tasks;
using BookStore.Models;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAccountRepository _accountRepository;
        public AccountController(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }
        
        [Route("signup")]
        public IActionResult Signup()
        {
            return View();
        }
        
        [Route("signup")]
        [HttpPost]
        public async Task<IActionResult> Signup(SignUpUserModel signUpUserModel)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.CreateUserAsync(signUpUserModel);
                if (!result.Succeeded)
                {
                    foreach (var errorMessage in result.Errors)
                    {
                        ModelState.AddModelError("",errorMessage.Description);
                    }
                }
            }
            return View();
        }
        
        [Route("login")]
        public IActionResult Login()
        {
            return View();
        }
        
        [Route("login")]
        [HttpPost]
        public async Task<IActionResult> Login(SignInModel signInModel, string ReturnUrl)
        {
            if (ModelState.IsValid)
            {
                var result = await _accountRepository.PasswordSignInUserAsync(signInModel);
                if (result.Succeeded)
                {
                    if (!string.IsNullOrEmpty(ReturnUrl))
                    {
                        return LocalRedirect(ReturnUrl);
                    }
                    return RedirectToAction("Index", "Home");
                }
                ModelState.AddModelError("", "Invalid Credentials");
            }
            return View();
        }

        public async Task<IActionResult> Signout()
        {
            await _accountRepository.SignOutUserAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}