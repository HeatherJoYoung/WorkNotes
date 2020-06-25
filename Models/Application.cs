using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkNotes.Models
{
	public enum Status
	{ 
		Applied, OfferReceived, Rejected
	}
	public class Application
	{
		public int ID { get; set; }
		public int JobID { get; set; }
		[Display(Name = "Application Date")]
		public DateTime Date { get; set; }
		public Status Status { get; set; }

		public virtual Job Job { get; set; }
	}
}