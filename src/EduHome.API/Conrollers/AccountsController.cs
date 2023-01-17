using EduHome.Business.DTOs.Auth;
using EduHome.Business.Exceptions;
using EduHome.Business.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace EduHome.API.Conrollers
{
	[Route("api/[controller]")]
	[ApiController]
	public class AccountsController : ControllerBase
	{
		private readonly IAuthService _authService;

		public AccountsController(IAuthService authService)
		{
			_authService = authService;
		}

		[HttpPost("[action]")]

		public async Task<IActionResult> Register(RegisterDTO registerDTO)
			{
			try
			{
				await _authService.Register(registerDTO);
				return Ok("User successfully created");
			}
			catch (UserCreatFailException)
			{
				return BadRequest();
			}
			catch (RoleCreateFailException)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}

		[HttpPost("[action]")]
		public async Task<IActionResult> Login(LoginDTO loginDTO)
		{
			try
			{
				var tokenResponse= await _authService.Login(loginDTO);
				return Ok(tokenResponse);
			}
			catch (AuthFailException)
			{
				return BadRequest();
			}
			catch (Exception)
			{
				return StatusCode((int)HttpStatusCode.InternalServerError);
			}
		}
	}
}
