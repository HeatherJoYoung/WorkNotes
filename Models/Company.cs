using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WorkNotes.Models
{
	public class Company
	{
		public int ID { get; set; }
		public string Name { get; set; }
		public string Location { get; set; }
	}
}