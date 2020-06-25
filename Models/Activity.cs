using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkNotes.Models
{
	public enum ActivityType
	{ 
		Phone, Email, Application, Meeting
	}
	public class Activity
	{
		public int ID { get; set; }

		public int PersonID { get; set; }

		public int? JobID { get; set; }

		public int? ApplicationID { get; set; }

		[DataType(DataType.Date)]
		[Required]
		public DateTime Date { get; set; }

		[Required]
		public ActivityType Type { get; set; }

		[StringLength(1200)]
		public string Notes { get; set; }

		public virtual Application Application { get; set; }
		public virtual Person Contact { get; set; }
		public virtual Job Job { get; set; }
	}
}