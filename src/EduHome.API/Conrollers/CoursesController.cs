using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduHome.API.Conrollers
{
	[Route("api/[controller]")]
	[ApiController]
	public class CoursesController : ControllerBase
	{
		private readonly ICourseService _courseService;

		public CoursesController(ICourseService courseService)
		{
			_courseService = courseService;
		}
		//[HttpGet,Route("GetCourses")]
		[HttpGet("GetCourses")]
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
	}
}
