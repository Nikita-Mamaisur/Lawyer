using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Lawyer.Models
{
	public class AnswerModel
	{
		public Guid Id { get; set; }
		public Guid AskId { get; set; }
		public string Text { get; set; }
		public DateTime Date { get; set; }
		public bool Confirmation { get; set; }
	}
}