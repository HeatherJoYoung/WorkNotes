using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkNotes.Models
{
	public class Job
	{
		public int ID { get; set; }
		public int CompanyId { get; set; }
		public int? PersonID { get; set; }
		[Display(Name = "Job Title")]
		public string JobTitle { get; set; }
		public string Description { get; set; }
		public string Qualifications { get; set; }
		[Display(Name = "Date Posted")]
		public DateTime PostingDate { get; set; }
		[Display(Name = "Posting Site")]
		public string PostingSite { get; set; }

		public virtual Company Company { get; set; }
		public virtual Person Recruiter { get; set; }
	}
}