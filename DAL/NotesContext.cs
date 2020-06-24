using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using WorkNotes.Models;

namespace WorkNotes.DAL
{
	public class NotesContext : DbContext
	{
		public NotesContext() : base("NotesContext")
		{ 
		}

		public DbSet<Activity> Activities { get; set; }
		public DbSet<Application> Applications { get; set; }
		public DbSet<Company> Companies { get; set; }
		public DbSet<Job> Jobs { get; set; }
		public DbSet<Person> Persons { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
		}
	}
}