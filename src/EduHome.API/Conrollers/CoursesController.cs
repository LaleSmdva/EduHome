using EduHome.Business.DTOs.Courses;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using EduHome.Core.Entities;
using EduHome.DataAccess.Repositories.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Net;

namespace EduHome.API.Conrollers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CoursesController : ControllerBase
	{
		private readonly ICourseService _courseService;
		private readonly ICourseRepository _courseRepository;

		public CoursesController(ICourseService courseService)
		{
			_courseService = courseService;
		}
		//[HttpGet,Route("GetCourses")]
		[HttpGet("")]
		public async Task<IActionResult> Get()
		{
			try
			{
				var courses = await _courseService.GetAllAsync();
				return Ok(courses);
			}
			catch (NotFoundException ex)
			{

				return NotFound(ex.Message);
			}

		}

		[HttpPost]

		public async Task<IActionResult> Create(CoursePostDTO coursePostDTO)
		{
			try
			{
				await _courseService.CreateAsync(coursePostDTO);
				//return Ok();
				//or
				return StatusCode((int)HttpStatusCode.Created);
			}
			catch (Exception)
			{

				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}


		//[HttpGet]
		//public async Task<IActionResult> Get()
		//{
		//	var courses = await _courseService.GetAllAsync();

		//	if (courses == null || courses.Count==0 )
		//	{
		//		throw new NotFoundException("not found");
		//	}
		//	return Ok(courses);
		//}

		//public async Task  Create()
		//{

		//}
	}
}
