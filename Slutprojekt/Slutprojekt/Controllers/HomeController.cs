using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slutprojekt.Models;
using Slutprojekt.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Slutprojekt.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{

		private readonly AccountRepository repository;

		public HomeController(AccountRepository repository)
		{
			this.repository = repository;
		}


		[Route("")]
		[Route("Index")]
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}

		[Route("Index")]
		[AllowAnonymous]
		[HttpPost]
		public IActionResult Index(LoginVM model)
		{
			var result = repository.ValidateUser(model);
			if (result.Result.Succeeded)
			{
				return RedirectToAction(nameof(HomeController.Home));
			}
			else
				return View(model);
		}

		[Route("Create")]
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Create()
		{
			//Formulär med användarnamn, lösenord, email
			return View(new CreateVM());

		}

		[Route("Create")]
		[HttpPost]
		[AllowAnonymous]
		public async Task<IActionResult> Create(CreateVM model)
		{
			//Validera formuläret

			if (!ModelState.IsValid)
			{
				return View(model);
			}

			var (succes, mess) = await repository.CreateUserAsync(model);

			if (!succes)
			{
				model.Message = mess;
				ModelState.AddModelError(nameof(CreateVM.Password), "Bad input");
				return View(model);
			}
			//    return RedirectToAction(nameof(HomeController.Home));

			return RedirectToAction(nameof(HomeController.Index));
		}


		[Route("Home")]
		public IActionResult Home()
		{
			return View();
		}

		[Route("Lobby")]
		public IActionResult Lobby()
		{

			var model = new LobbyVM();
			model.UserName = User.Identity.Name;
			return View(model);

		}
	}
}
