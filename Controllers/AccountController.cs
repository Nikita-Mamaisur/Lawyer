using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using System.Web;
using System.Web.Mvc;
using Lawyer.Entities;
using Lawyer.Models.Account;
using System.Threading.Tasks;
using System.Web.Security;

namespace Lawyer.Controllers
{
	public class AccountController : Controller
	{
		public IAuthenticationManager SignInManager
		{
			get => HttpContext.GetOwinContext().Authentication;
		}
		public UserManager<User> UserManager
		{
			get => HttpContext.GetOwinContext().GetUserManager<UserManager<User>>();
		}
		public ActionResult Login()
		{
			return View(new LoginModel());
		}

		[HttpPost]

		public ActionResult Login(LoginModel model)
		{
			if (ModelState.IsValid)
			{
				var user = UserManager.Find(model.Email, model.Password);
				var identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);

				SignInManager.SignIn(identity);

				return RedirectToAction("Index", "Home");
			}

			return View(model);
		}

		public ActionResult Logout()
		{
			SignInManager.SignOut();
			return RedirectToAction("Login");
		}

		public ActionResult Register()
		{
			return View(new RegisterModel());
		}

		[HttpPost]
		public ActionResult Register(RegisterModel model)
		{
			if (ModelState.IsValid)
			{
				var user = new User()
				{
					UserName = model.Email,
					Name = model.Name,
					Email = model.Email,
				};

				var result = UserManager.Create(user, model.Password);
				if (result.Succeeded)
				{
					var identity = UserManager.CreateIdentity(user, DefaultAuthenticationTypes.ApplicationCookie);
					UserManager.AddToRole(user.Id,  "defaultUser");  // вот тут добавлена роль по-дефолту
					SignInManager.SignIn(identity);
					return RedirectToAction("Index", "Home");
				}
				else
				{
					foreach (var error in result.Errors)
					{
						ModelState.AddModelError(string.Empty, error);
					}
				}
			}

			return View(model);
		}
		[HttpGet]
		public ActionResult Delete()
		{
			return View();
		}

		[HttpPost]
		[ActionName("Delete")]
		public async Task<ActionResult> DeleteConfirmed()
		{
			User user = await UserManager.FindByEmailAsync(User.Identity.Name);
			if (user != null)
			{
				IdentityResult result = await UserManager.DeleteAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("Logout", "Account");
				}
			}
			return RedirectToAction("Index", "Home");
		}

		public async Task<ActionResult> Edit()
		{
			User user = await UserManager.FindByEmailAsync(User.Identity.Name);
			if (user != null)
			{
				EditModel model = new EditModel { Name = user.Name };
				return View(model);
			}
			return RedirectToAction("Login", "Account");
		}

		[HttpPost]
		public async Task<ActionResult> Edit(EditModel model)
		{
			User user = await UserManager.FindByEmailAsync(User.Identity.Name);
			if (user != null)
			{
				user.Name = model.Name;
				IdentityResult result = await UserManager.UpdateAsync(user);
				if (result.Succeeded)
				{
					return RedirectToAction("Index", "Home");
				}
				else
				{
					ModelState.AddModelError("", "Что-то пошло не так");
				}
			}
			else
			{
				ModelState.AddModelError("", "Пользователь не найден");
			}

			return View(model);
		}
	}
}