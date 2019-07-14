using Lawyer.Models;
using Lawyer.ServiceManagers;
using Microsoft.AspNet.Identity;
using System.Data.Entity;
using System.Net;
using System.Web.Mvc;


namespace Lawyer.Controllers
{
	[Authorize]
	public class AskController : Controller
	{
		private readonly ServiceManager _serviceManager = new ServiceManager();

		public ActionResult AddQuestions()
		{
			return View(new AskModel());
		}

		[HttpPost]
		public ActionResult AddQuestions(AskModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var userId = User.Identity.GetUserId();
				_serviceManager.Asks.AddAsk(model, userId);
				//return RedirectToAction("Index", "Home");

				returnUrl = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "/";
				return Redirect(returnUrl);
			}
			//return View(model);

			return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		}


		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult AddAnswer(AnswerModel model, string returnUrl)
		{
			if (ModelState.IsValid)
			{
				var userId = User.Identity.GetUserId();
				_serviceManager.Asks.AddAnswer(model, userId);

				returnUrl = !string.IsNullOrEmpty(returnUrl) ? returnUrl : "/";
				return Redirect(returnUrl);
			}

			return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
		}

		[AllowAnonymous]
		public ActionResult LastAsks(int count = 20)
		{
			var userId = User.Identity.GetUserId();

			var lastAsks = _serviceManager.Asks.GetLastAsks(count);
			
			return View("LastAsks", lastAsks);
		}

		[AllowAnonymous]
		public ActionResult LastAsksWithAnswer(int count = 20)
		{
			var userId = User.Identity.GetUserId();

			var lastAsks = _serviceManager.Asks.GetLastAsksForUser(count);

			return View("LastAsksWithAnswer", lastAsks);
		}


		[AllowAnonymous]
		public ActionResult ShowAnswer(string slug)
		{
			var model = _serviceManager.Asks.FindBySlug(slug);

			if (model == null)
			{
				return HttpNotFound();
			}

			return View(model);
		}

		[AllowAnonymous]
		public ActionResult Show(string slug)
		{
			var model = _serviceManager.Asks.FindBySlug(slug);

			if (model == null)
			{
				return HttpNotFound();
			}

			return View(model);
		}



		[HttpPost]
		public ActionResult EditButton( string slug)
		{
			
			try
			{
				_serviceManager.Asks.SetIsAnswered(slug);
				return RedirectToAction("LastAsks");
			}
			catch
			{
				return RedirectToAction("LastAsks");
			}
		}
	}
}