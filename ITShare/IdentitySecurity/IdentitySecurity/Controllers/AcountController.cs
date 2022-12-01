using System.Threading.Tasks;
using IdentitySecurity.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentitySecurity.Controllers
{
    public class AcountController : Controller
    {
        private readonly UserManager<IdentityUser> _usermanager;
        private readonly SignInManager<IdentityUser> _signInmanager;

        public AcountController( UserManager<IdentityUser>usermanager,SignInManager<IdentityUser> signInmanager)
        {
            this._usermanager = usermanager;
            this._signInmanager = signInmanager;   
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register( RegisterViewModel viewmodel)
        {
            if(ModelState.IsValid)
            {
                //1- copy Data from RegisterViewModel to IdentityUser
                IdentityUser user = new IdentityUser
                {
                    UserName = viewmodel.Email,
                    Email = viewmodel.Email    
                };
                //2-Store user in Db : UserManager Class
                var Result =await _usermanager.CreateAsync(user, viewmodel.Password);

                //3-process ? Successed or Fail
                if(Result.Succeeded)
                {
                    await _signInmanager.SignInAsync(user, isPersistent: true);
                    return RedirectToAction("Index", "Home");
                }
                //4-in Case of any error in registeration
                foreach (var item in Result.Errors)
                {
                    ModelState.AddModelError(string.Empty, item.Description);
                }
            }
            return View(viewmodel);
        }
    }
}
