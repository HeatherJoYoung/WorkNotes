using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WorkNotes.Models
{
	public class Person
	{
		public int ID { get; set; }
		public int CompanyID { get; set; }
		public string FirstName { get; set; }
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Title { get; set; }

		public virtual Company Company { get; set; }
	}
}