using Lawyer.Entities;
using Microsoft.AspNet.Identity;

namespace Lawyer.Identity
{
	public class ApplicationUserManager : UserManager<User>
	{
		public ApplicationUserManager(IUserStore<User> store)
				: base(store)
		{
		}
	}
}