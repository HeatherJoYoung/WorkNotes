using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WorkNotes.Models
{
	public class Job
	{
		public int ID { get; set; }

		public int CompanyId { get; set; }

		[StringLength(40)]
		[Display(Name = "Job Title")]
		[Required]
		public string JobTitle { get; set; }

		public string Description { get; set; }

		public string Qualifications { get; set; }

		[Display(Name = "Date Posted")]
		[DataType(DataType.Date)]
		[DisplayFormat(DataFormatString = "{0:MM/dd/yy}", ApplyFormatInEditMode = true)]
		[Column(TypeName = "date")]
		public DateTime PostingDate { get; set; }

		[StringLength(40)]
		[Display(Name = "Posting Site")]
		public string PostingSite { get; set; }

		public virtual Company Company { get; set; }
		public virtual List<Person> Contacts { get; set; }

		public Person Recruiter
		{
			get
			{
				return (Contacts == null || Contacts.Count == 0) ? null : Contacts.Where(c => c.Title == ContactTitle.Recruiter).FirstOrDefault();
			}
		}

		public Person Manager
		{
			get
			{
				return (Contacts == null || Contacts.Count == 0) ? null : Contacts.Where(c => c.Title == ContactTitle.Manager).FirstOrDefault();
			}
		}
	}
}