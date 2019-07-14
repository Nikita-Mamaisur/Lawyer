using Lawyer.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Lawyer.Models
{
	public class AskModel
	{
		public Guid Id { get; set; }

		public string Slug { get; set; }

		[Required]
		[StringLength(50, MinimumLength = 5)]
		public string Title { get; set; }

		[Required]
		[AllowHtml]
		[StringLength(maximumLength: 300, MinimumLength = 10, ErrorMessage = "* Part answers must be between 10 and 300 character in length.")]
		public string Body { get; set; }

		public DateTime Date { get; set; }

		public bool IsAnswered { get; set; }

		public List<AnswerModel> Answers { get; set; }
	}
}