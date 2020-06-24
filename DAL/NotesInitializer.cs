using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using WorkNotes.Models;

namespace WorkNotes.DAL
{
	public class NotesInitializer : DropCreateDatabaseIfModelChanges<NotesContext>
	{
		protected override void Seed(NotesContext context)
		{
			var companies = new List<Company>()
			{
				new Company { Name = "Microsoft", Locations = new List<string> { "Seattle", "Boulder" } },
				new Company { Name = "Amazon", Locations = new List<string> { "Seattle", "Denver", "Boulder" } }
			};

			companies.ForEach(c => context.Companies.Add(c));
			context.SaveChanges();

			var persons = new List<Person>()
			{
				new Person { FirstName = "Jane", LastName = "Doe", Phone = "505-555-4545", Email = "jane@company.com", Title = "Recruiter", CompanyID = companies.Single( c => c.Name == "Microsoft").ID
				},
				new Person { FirstName = "John", LastName = "Smith", Phone = "505-444-4545", Email = "john@company.com", Title = "Recruiter", CompanyID = companies.Single( c => c.Name == "Amazon").ID
				}
			};

			persons.ForEach(p => context.Persons.Add(p));
			context.SaveChanges();

			var jobs = new List<Job>()
			{
				new Job { JobTitle = "Software Engineer", Description = "Design and proto-type new features. Review pull requests. Debug existing and new code. Fix code defects. Create and execute new tests for new and existing code. Continuously communicate and collaborate with other teams in Windows.", Qualifications = "2+ years of software development experience.. Coding experience in C/C++ in a professional capacity. BS/MS in Computer Science or equivalent.", PostingDate = DateTime.Parse("01/05/2020"), PostingSite = "LinkedIn", CompanyId = companies.Single( c => c.Name == "Microsoft").ID, PersonID = 1 },
				new Job { JobTitle = "Software Development Engineer II", Description = "On this team you will play a leading role in the definition, design and development of this new service. Work with development teams inside and outside Amazon as your core customers. Identify and eliminate developer pain points in multiple languages and toolchains. Iterate, test new ideas, and shape the future vision for software development at Amazon. Learn, use and master core AWS and Amazon technologies. Work closely with remarkable engineers and business leaders on hard problems.", Qualifications = "3+ years of professional software development experience. Programming experience with at least one modern language such as Java, C++, or C#. Computer Science fundamentals in object-oriented design, data structures and algorithms. Experience building complex software systems that have been successfully delivered to customers.", PostingDate = DateTime.Parse("02/02/2020"), PostingSite = "Glassdoor", CompanyId = companies.Single( c => c.Name == "Amazon").ID, PersonID = 2 },
			};

			jobs.ForEach(j => context.Jobs.Add(j));
			context.SaveChanges();

			var applications = new List<Application>()
			{
				new Application { Date = DateTime.Parse("01/06/2020"), Status = Status.Applied, JobID = 1 },
				new Application { Date = DateTime.Parse("02/03/2020"), Status = Status.Applied, JobID = 2 }
			};

			applications.ForEach(a => context.Applications.Add(a));
			context.SaveChanges();

			var activities = new List<Activity>()
			{
				new Activity { Date = DateTime.Parse("01/06/2020"), Type = ActivityType.Application, PersonID = 1, JobID = 1, ApplicationID = 1 },
				new Activity { Date = DateTime.Parse("02/03/2020"), Type = ActivityType.Application, PersonID = 2, JobID = 2, ApplicationID = 2 }
			};

			activities.ForEach(a => context.Activities.Add(a));
			context.SaveChanges();
		}
	}}