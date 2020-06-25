using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkNotes.Models
{
	public class Person
	{
		public int ID { get; set; }
		public int CompanyID { get; set; }
		[Display(Name = "First Name")]
		public string FirstName { get; set; }
		[Display(Name = "Last Name")]
		public string LastName { get; set; }
		public string Phone { get; set; }
		public string Email { get; set; }
		public string Title { get; set; }

		public virtual Company Company { get; set; }

		[Display(Name = "Full Name")]
		public string FullName
		{
			get
			{
				return FirstName + " " + LastName;
			}
		}
	}
}