using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Lawyer.Context;
using Lawyer.Services;

namespace Lawyer.ServiceManagers
{
	public class ServiceManager
	{
		public AskServices Asks { get; }

		public ServiceManager()
		{
			var context = new LawyerContext();
			Asks = new AskServices(context);
		}
	}
}