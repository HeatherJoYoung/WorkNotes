﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkNotes.Models
{
	public class Job
	{
		public int ID { get; set; }
		public int CompanyId { get; set; }
		public int PersonID { get; set; }
		public string JobTitle { get; set; }
		public string Description { get; set; }
		public string Qualifications { get; set; }
		public DateTime PostingDate { get; set; }
		public string PostingSite { get; set; }

		public virtual Company Company { get; set; }
		public virtual Person Recruiter { get; set; }
	}
}