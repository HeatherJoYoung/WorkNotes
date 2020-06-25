using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkNotes.Models
{
	public enum ApplicationStatus
	{ 
		Applied, OfferReceived, Rejected
	}
	public class Application
	{
		public int ID { get; set; }

		[Required]
		public int JobID { get; set; }

		[Display(Name = "Application Date")]
		[DataType(DataType.Date)]
		[Required]
		public DateTime Date { get; set; }

		public ApplicationStatus Status { get; set; }

		public virtual Job Job { get; set; }
	}
}