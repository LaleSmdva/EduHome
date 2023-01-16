using EduHome.Core.Entities.Identity;
using EduHome.Core.Enums;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduHome.DataAccess.Contexts
{
	public class AppDbContextInitializer
	{
		private readonly UserManager<AppUser> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;

		public AppDbContextInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager)
		{
			_userManager = userManager;
			_roleManager = roleManager;
		}

		public async Task SeedAsync()
		{
			foreach (var role in Enum.GetValues(typeof(Roles)))
			{
				if (!await _roleManager.RoleExistsAsync(role.ToString()))
					{
					await _roleManager.CreateAsync(new() { Name=role.ToString()});

				}


			}
		}
	}
}



