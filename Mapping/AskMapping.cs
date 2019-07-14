using Lawyer.Entities;
using Lawyer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lawyer.Mapping
{
	public static partial class Mapping
	{
		public static IQueryable<AskModel> SelectAskModel(this IQueryable<Ask> query)
		{
			var answerQuery = query.SelectMany(x => x.Answers).SelectAnswerModel();

			return query.GroupJoin(answerQuery, p => p.Id, c => c.AskId, (ask, answers) => new AskModel()
			{
				Id = ask.Id,
				Body = ask.Body,
				Date = ask.Date,
				Title = ask.Title,
				Slug = ask.Slug,
				Answers = answers.ToList(),
				IsAnswered = ask.IsAnswered
			});
		}

		public static IQueryable<AnswerModel> SelectAnswerModel(this IQueryable<Answer> query)
		{
			return query.Select(x => new AnswerModel()
			{
				Id = x.Id,
				AskId = x.Ask.Id,
				Text = x.Text,
				 Confirmation = x.Confirmation
			});
		}
	}
}