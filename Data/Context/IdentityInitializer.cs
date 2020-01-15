using Infrastructure.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Data.Context
{
    public class IdentityInitializer : IIdentityInitializer
    {
        private const string ADMIN_USER = "Admin";
        private const string ADMIN_PASSWORD = "Admin12345!";
        private readonly UserManager<IdentityUser> _userManager;

        public IdentityInitializer(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        public async void SeedData()
        {
            IdentityUser user = await _userManager.FindByIdAsync(ADMIN_USER);
            if (user == null)
            {
                user = new IdentityUser(ADMIN_USER);
                await _userManager.CreateAsync(user, ADMIN_PASSWORD);
            }
        }
    }
}
