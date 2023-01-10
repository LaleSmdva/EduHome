using EduHome.Core.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.DataAccess.Repositories.Interfaces;

public interface IRepository<T> where T:class,IEntity,new()
{
	 IEnumerable<T> GetAll();
	DbSet<T> Table { get; }
}

