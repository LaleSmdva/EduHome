using EduHome.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Services.Interfaces
{
	public interface ICourseService
	{
		Task<List<Course>> GetAllAsync();

		Task<List<Course>> FindByCondition(Expression<Func<Course, bool>> expression);
		Course FindById(int id);
		Task CreateAsync(Course entity);
		void Update(Course entity);
		void Delete(Course entity);
	}
}
