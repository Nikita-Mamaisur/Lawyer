﻿using Lawyer.Context;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lawyer.Models
{
	class ApplicationRoleManager : RoleManager<ApplicationRole>
	{
		public ApplicationRoleManager(RoleStore<ApplicationRole> store)
					: base(store)
		{ }
		public static ApplicationRoleManager Create(IdentityFactoryOptions<ApplicationRoleManager> options,
												IOwinContext context)
		{
			return new ApplicationRoleManager(new
					RoleStore<ApplicationRole>(context.Get<LawyerContext>()));
		}
	}
}