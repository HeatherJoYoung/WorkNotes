using System;
using System.Collections.Generic;
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
		public int ContactID { get; set; }
		public int JobID { get; set; }
		public int ApplicationID { get; set; }
		public DateTime Date { get; set; }
		public ActivityType Type { get; set; }
		public string Notes { get; set; }

		public virtual Application Application { get; set; }
		public virtual Contact Contact { get; set; }
		public virtual Job Job { get; set; }
	}
}