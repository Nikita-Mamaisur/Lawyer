
using Lawyer.Context;
using Lawyer.Entities;
using Lawyer.Identity;
using Lawyer.Models;
using Lawyer.Models.Account;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using Microsoft.Owin.Security.Cookies;
using Owin;

[assembly: OwinStartup(typeof(Lawyer.Startup))]
namespace Lawyer
{
	public class Startup
	{
		public void Configuration(IAppBuilder app)
		{
			

			app.CreatePerOwinContext(() => new LawyerContext());

			app.CreatePerOwinContext<ApplicationRoleManager>(ApplicationRoleManager.Create); // регистрация менеджера ролей

			app.CreatePerOwinContext<UserManager<User>>((options, context) =>
			{
				var dbContext = context.Get<LawyerContext>();
				var userStore = new UserStore<User>(dbContext);
				var userManager = new UserManager<User>(userStore)
				{
					PasswordValidator = new PasswordValidator()
					{
						RequiredLength = 6,
						RequireDigit = false,
						RequireLowercase = false,
						RequireNonLetterOrDigit = false,
						RequireUppercase = false
					}
				};

				userManager.UserValidator = new UserValidator<User>(userManager)
				{
					AllowOnlyAlphanumericUserNames = false,
					RequireUniqueEmail = true
				};

				return userManager;
			});

			app.UseCookieAuthentication(new CookieAuthenticationOptions()
			{
				AuthenticationType = DefaultAuthenticationTypes.ApplicationCookie,
				LoginPath = new PathString("/account/login")
			});



			// TODO:  вытащить из OwinContext контекст и менеджеры
			LawyerContext contextt = new LawyerContext();
			var userManagerr = new ApplicationUserManager(new UserStore<User>(contextt));
			var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(contextt));
			
			
			// создаем две роли и добавляем роли в бд
			if (!roleManager.RoleExists("admin"))
			{
				var role1 = new IdentityRole { Name = "admin" };
				var role2 = new IdentityRole { Name = "defaultUser" };
				roleManager.Create(role1);
				roleManager.Create(role2);
			
				// создаем пользователя и добавляем для пользователя роль
				var admin = new User { Email = "somemail@mail.ru", UserName = "somemail@mail.ru", Name = "Linda" };
				string password = "qqqqqq";
				var result = userManagerr.Create(admin, password);
				userManagerr.AddToRole(admin.Id, "admin");
			}
		}
	}
}