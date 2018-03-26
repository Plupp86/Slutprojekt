using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slutprojekt.Models.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Slutprojekt.Controllers
{	
	//[Authorize]
    public class HomeController : Controller
    {
		// GET: /<controller>/

		[Route("")]
		[Route("Index")]
		[HttpGet]
		//[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}

		[Route("Index")]
		[HttpPost]
		//[AllowAnonymous]
		public IActionResult Index(LoginVM model)
		{
			//Validering att formuläret är korrekt ifyllt

			//Validering att användare/lösenord är korrekt


			return RedirectToAction(nameof(HomeController.Home));
		}

		[Route("Create")]
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Create()
		{
			//Formulär med användarnamn, lösenord, email
			return View();
		}

		[Route("Create")]
		[HttpPost]
		[AllowAnonymous]
		public IActionResult Create(CreateVM model)
		{
			//Validera formuläret

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

			//SignalR
			return View();
		}


	}
}
