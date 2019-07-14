using Lawyer.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Lawyer.Mapping;
using Lawyer.Models;
using Lawyer.Helpers;
using System.Web.Mvc;

namespace Lawyer.Services
{

	public class AskServices
	{
		private readonly DbContext _context;
		private readonly DbSet<User> _userSet;
		private readonly DbSet<Ask> _askSet;
		private readonly DbSet<Answer> _answerSet;

		public AskServices(DbContext context)
		{
			_context = context;
			_userSet = _context.Set<User>();
			_askSet = context.Set<Ask>();
			_answerSet = context.Set<Answer>();
		}

		public void AddAsk(AskModel model, string userId)
		{
			var user = new User() { Id = userId };
			_userSet.Attach(user);

			var ask = new Ask()
			{
				Title = model.Title,
				Slug = SlugGenerator.GenerateSlug(model.Title),
				Body = model.Body,
				Date = DateTime.UtcNow,
				Author = user
			};
			_askSet.Add(ask);
			_context.SaveChanges();

			model.Id = ask.Id;
		}

		public void AddAnswer(AnswerModel model, string userId)
		{
			var user = new User() { Id = userId };
			var ask = new Ask() { Id = model.AskId, IsAnswered = model.Confirmation };

			_userSet.Attach(user);
			_askSet.Attach(ask);

			var answer = new Answer()
			{
				Author = user,
				Text = model.Text,
				Ask = ask,
				Date = DateTime.UtcNow,
				Confirmation = true
			};

			_answerSet.Add(answer);
			_context.SaveChanges();

			model.Id = answer.Id;
		}

		public AskModel FindBySlug(string slug)
		{
			return _askSet.Where(x => x.Slug == slug)
				.SelectAskModel()
				.SingleOrDefault();
		}

		public AskModel FindById(Guid guid)
		{
			return _askSet.Where(x => x.Id == guid)
				.SelectAskModel()
				.SingleOrDefault();
		}

		public List<AskModel> GetLastAsks(int count)
		{
			return _askSet.OrderByDescending(p => p.Date)
				.Take(count)
				.SelectAskModel()
				.ToList();
		}

		public void SetIsAnswered(string slug)
		{
			var set = _askSet.Where(x => x.Slug == slug).SingleOrDefault();

			set.IsAnswered = true;
			_context.SaveChanges();
		}

		//public AskModel FindBySlugForUser(string slug)
		//{
		//	return _askSet.Where(x => x.Slug == slug)
		//		.SelectAskModel()
		//		.SingleOrDefault();
		//}

		public List<AskModel> GetLastAsksForUser(int count)
		{
			return _askSet.OrderByDescending(p => p.Date)
				.Where(x => x.IsAnswered == true)
				.Take(count)
				.SelectAskModel()
				.ToList();
		}
	}
}