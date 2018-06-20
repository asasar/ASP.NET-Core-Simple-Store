using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SimpleStore.Models;
using SimpleStore.Utils;
using SimpleStore.ViewModels;
using System.Threading.Tasks;

namespace SimpleStore.Controllers.Web
{
    public class AuthController : Controller
    {
        private SignInManager<SimpleStoreUser> _singinManager;
        private UserManager<SimpleStoreUser> _usermanager;

        public AuthController(SignInManager<SimpleStoreUser> singinManager, UserManager<SimpleStoreUser> usermanager)
        {
            _singinManager = singinManager;
            _usermanager = usermanager;
        }

        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Admin", "Index");
            }

            return View();
        }

        public IActionResult AccessDenied()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Login(LoginViewModel userInfo, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                var sigInResult = await _singinManager.PasswordSignInAsync(
                        userInfo.Email,
                        userInfo.Password,
                        true,
                        false);

                if (sigInResult.Succeeded)
                {
                    var user = await _usermanager.FindByEmailAsync(userInfo.Email);
                    var roles = await _usermanager.GetRolesAsync(user);

                    if (roles.Contains(ConstantsSimpleStore.roleNameBackend))
                    {
                        return RedirectToAction("Index", "Admin");
                    }
                    else if (roles.Contains(ConstantsSimpleStore.roleNameCustomer))
                    {
                        if (returnUrl != string.Empty)
                        {
                            return Redirect(returnUrl); 
                        }
                        else
                        { 
                             return Redirect("/");
                        }
                     
                    }
                }
                else
                {
                    ModelState.AddModelError("", "UserName or password incorrect");
                }
            }
            return View();
        }

        public IActionResult Signup()
        {
            return View();
        }


        [HttpPost]
        public async Task<ActionResult> Signup(LoginViewModel userInfo)
        {
            if (ModelState.IsValid)
            {
                var user = new SimpleStoreUser()
                {
                    UserName = userInfo.Email,
                    Email = userInfo.Email
                };

                var resultuser = await _usermanager.CreateAsync(user, userInfo.Password);

                if (resultuser.Succeeded)
                {
                    await _usermanager.AddToRoleAsync(user, ConstantsSimpleStore.roleNameCustomer);
                    var sigInResult =  await _singinManager .PasswordSignInAsync(
                       userInfo.Email,
                       userInfo.Password,
                       true,
                       false);
                    return Redirect("/");
                }
                else
                {
                    ModelState.AddModelError("Password", resultuser.ToString());
                }
            }
            return View();
        }

        [HttpGet]
        public async Task<ActionResult> Logout()
        {
            if (User.Identity.IsAuthenticated)
            {
                await _singinManager.SignOutAsync();
            }
            return Redirect("/");
        }
    }
}
