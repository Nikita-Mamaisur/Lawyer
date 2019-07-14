using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Lawyer.App_Start
{
	public class BundleConfig
	{
		public static void RegisterBundles(BundleCollection bundles)
		{
			bundles.Add(new StyleBundle("~/content/lib")
			   .Include("~/content/bootstrap.min.css")
			   .Include("~/content/summernote-bs4.css"));

			bundles.Add(new ScriptBundle("~/scripts/lib")
				.Include("~/scripts/jquery-3.4.1.min.js")
				.Include("~/scripts/umd/popper.min.js")
				.Include("~/scripts/bootstrap.min.js")
				.Include("~/scripts/summernote/summernote-bs4.min.js"));
		}
	}
}