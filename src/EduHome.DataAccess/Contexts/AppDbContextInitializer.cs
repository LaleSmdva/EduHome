﻿using EduHome.Core.Entities.Identity;
using EduHome.Core.Enums;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
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
        private readonly IConfiguration _configuration;
        private readonly AppDbContext _context;

        public AppDbContextInitializer(UserManager<AppUser> userManager, RoleManager<IdentityRole> roleManager, IConfiguration configuration, AppDbContext context)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
            _context = context;
        }
        //automatic update-database
        public async Task InitializeAsync()
        {
            await _context.Database.MigrateAsync();
        }
        public async Task RoleSeedAsync()
        {
            foreach (var role in Enum.GetValues(typeof(Roles)))
            {
                if (!await _roleManager.RoleExistsAsync(role.ToString()))
                {
                    await _roleManager.CreateAsync(new() { Name = role.ToString() });
                }
            }
        }
        public async Task UserSeedAsync()
        {
            AppUser admin = new()
            {
                UserName = _configuration["AdminSettings:UserName"],
                Email = _configuration["AdminSettings:Email"]
            };
            if (!_userManager.Users.Any(u => u.UserName == admin.UserName))
            {
                await _userManager.CreateAsync(admin, _configuration["AdminSettings:Password"]);
                await _userManager.AddToRoleAsync(admin, Roles.Admin.ToString());
            }
        }
    }
}



