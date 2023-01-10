using AutoMapper;
using EduHome.Business.DTOs.Courses;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Core.Entities;
using EduHome.DataAccess.Repositories.Implementations;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.Business.Services.Implementations
{
	public class CourseService:ICourseService
	{
		private readonly ICourseRepository _courseRepository;
		private readonly IMapper _mapper;

		public CourseService(ICourseRepository courseRepository, IMapper mapper)
		{
			_courseRepository = courseRepository;
			_mapper = mapper;
		}

		public async Task<List<Course>> FindByCondition(Expression<Func<Course, bool>> expression)
		{
			return await _courseRepository.FindByCondition(expression).ToListAsync();
		}

		public Course FindById(int id)
		{
			return _courseRepository.FinddById(id);
		}

		public async Task<List<CourseDTO>> GetAllAsync()
		{
			var courses = _courseRepository.FindAll().ToList();
			var result= _mapper.Map<List<CourseDTO>>(courses);
			//if (courses == null || courses.Count == 0) throw new NotFoundException("Not found!");
			return result;
		}
		public async Task CreateAsync(Course entity)
		{
			await _courseRepository.Create(entity);
		}

		public void Update(Course entity)
		{
			_courseRepository.Update(entity);
		}

		public void Delete(Course entity)
		{
			_courseRepository.Delete(entity);
		}

		

	
	}
}
