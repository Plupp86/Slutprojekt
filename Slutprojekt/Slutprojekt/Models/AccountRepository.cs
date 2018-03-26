using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Slutprojekt.Models.Entities;
using Slutprojekt.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Slutprojekt.Models
{
	public class AccountRepository
	{
		UserManager<IdentityUser> userManager;
		SignInManager<IdentityUser> signInManager;
		RoleManager<IdentityRole> roleManager;
		IdentityDbContext identityContext;
        SlutprojektDBContext context;

        public AccountRepository(
			UserManager<IdentityUser> userManager,
			SignInManager<IdentityUser> signInManager,
			RoleManager<IdentityRole> roleManager,
			IdentityDbContext identityContext,
		    SlutprojektDBContext context)
		{
			this.userManager = userManager;
			this.signInManager = signInManager;
			this.roleManager = roleManager;
			this.identityContext = identityContext;
			this.context = context;
		}

		public void GenerateDBSchema()
		{
			identityContext.Database.EnsureCreated();
		}

        public async Task<(bool,string)> CreateUserAsync(CreateVM model)
        {
            IdentityUser user = new IdentityUser(model.UserName);
            var result = await userManager.CreateAsync(user, model.Password);

            return (result.Succeeded, result.ToString());


        }

        public async Task<SignInResult> ValidateUser(LoginVM model)
		{

			return await signInManager.PasswordSignInAsync(
				model.UserName,
				model.Password,
				false,
				false);
		}

		public async void SignOutUser()
		{
			await signInManager.SignOutAsync();
		}

		//public MemberVM GetUserInfo(HttpContext httpcontext)
		//{
		//	MemberVM model = new MemberVM();
		//	string userId = userManager.GetUserId(httpcontext.User);
		//	var user = context.User
		//		.SingleOrDefault(u => u.Id == userId);

		//	if (user != null)
		//	{
		//		model.FirstName = user.FirstName;
		//		model.LastName = user.LastName;
		//	}


		//	return model;
		//}

		//public async Task<string> GetUserName()
		//{
		//	var name = userManager.GetUserId(HttpContext.User);
		//	return " ";
		//}
	}

}
