using EduHome.Core.Entities;
using EduHome.Core.Interfaces;
using EduHome.DataAccess.Contexts;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.DataAccess.Repositories.Implementations
{
	public class Repository<T> : IRepository<T> where T : class, IEntity, new()
	{
		private readonly AppDbContext _context;
		//private readonly DbSet<T> _table;

		public Repository(AppDbContext context/*, DbSet<T> table*/)
		{
			_context = context;
			//_table = table;
		}

		public DbSet<T> Table { get; set; }

		public Task<IEnumerable<T>> GetAll()
		{
			throw new NotImplementedException();
		}
	}
}
