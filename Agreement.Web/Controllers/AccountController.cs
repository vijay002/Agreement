using Agreement.Domain.Account;
using Agreement.Services;
using Agreement.Web.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Agreement.Web.Controllers
{
    [Route("[controller]/[action]")]
    public class AccountController : Controller
    {
        #region Variables and Property
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IUnitOfWork _unitOfWork;
        #endregion

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IUnitOfWork unitOfWork

            )
        {
            _userManager = userManager;
            _signInManager = signInManager;

            _unitOfWork = unitOfWork;
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);
            ViewData["ReturnUrl"] = returnUrl;
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            if (ModelState.IsValid)
            {
                bool isEmailActivated = await _unitOfWork.UserRepository.IsAccountActivatedAsync(model.Email, _userManager);
                if (!isEmailActivated)
                {
                    ModelState.AddModelError("", "You need to confirm your email.");
                    ViewData["error"] = "You need to register first or You need to confirm your email.";
                    return View(model);
                }
                var user = await _userManager.FindByEmailAsync(model.Email);
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, model.RememberMe, lockoutOnFailure: false);
                if (result.Succeeded)
                {

                    var customClaims = new[]
                                        {
                                            new Claim("logged_in_day", DateTime.UtcNow.DayOfWeek.ToString()),
                                            new Claim("UserId", user.Id)
                                        };

                    var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);

                    if (customClaims != null && claimsPrincipal?.Identity is ClaimsIdentity claimsIdentity)
                    {
                        claimsIdentity.AddClaims(customClaims);
                    }

                    //SignInUserAsync
                    await _signInManager.Context.SignInAsync(IdentityConstants.ApplicationScheme,
                        claimsPrincipal,
                        new AuthenticationProperties { IsPersistent = model.RememberMe });

                    return RedirectToLocal(returnUrl);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt.");
                    ViewData["error"] = "Invalid login attempt";
                    return View(model);
                }
            }
            return View();
        }

        [HttpGet]
        public IActionResult AccessDenied()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

        #region Private method

        
        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            else
            {
                //return Redirect("/home");
                return RedirectToAction("index", "home");
            }
        }
        #endregion

    }
}
