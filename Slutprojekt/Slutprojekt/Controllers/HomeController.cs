using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Slutprojekt.Models;
using Slutprojekt.Models.ViewModels;
using Slutprojekt.Stats;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Slutprojekt.Controllers
{
	[Authorize]
	public class HomeController : Controller
	{

		private readonly AccountRepository repository;
		private readonly StatsRepository statsRep;

		public HomeController(AccountRepository repository, StatsRepository statsRep)
		{
			this.repository = repository;
			this.statsRep = statsRep;
		}


		[Route("")]
		[Route("Index")]
		[HttpGet]
		[AllowAnonymous]
		public IActionResult Index()
		{
			return View();
		}


		//[Route("UpdateDB")]
		//[HttpGet]
		//[AllowAnonymous]
		//public IActionResult UpdateDB()
		//{
		//	var users = repository.GetAllUserNames();
		//	statsRep.MoveExistingUsers(users);
		//	return RedirectToAction(nameof(HomeController.Index));
		//}


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
			var model = new HomeVM();
			model.User = User.Identity.Name;
			model.RecentNews = statsRep.GetTheNews();
			model.OldNews = statsRep.GetOldNews();
			model.recentMatches = statsRep.GetRecentMatches();
			return View(model);
		}

		[Route("TicLobby")]
		public IActionResult Lobby()
		{

			var model = new LobbyVM();
			model.UserName = User.Identity.Name;
			model.GameName = "Tic-Tac-Toe";
			return View(model);

		}

		[Route("MathLobby")]
		public IActionResult MathLobby()
		{

			var model = new LobbyVM();
			model.UserName = User.Identity.Name;
			model.GameName = "MathGame";
			return View(model);

		}

		[Route("MemoryLobby")]
		public IActionResult MemoryLobby()
		{

			var model = new LobbyVM();
			model.UserName = User.Identity.Name;
			model.GameName = "Memory";
			return View(model);
		}

		[Route("Ranking")]
		public IActionResult Ranking()
		{
			var model = new RankingVM();
			model.MathUsers = statsRep.GetMathUsers();
			model.MemoryUsers = statsRep.GetMemoryUsers();
			model.TicUsers = statsRep.GetTicUsers();
			return View(model);
		}

		[Route("AddNews")]
		[HttpGet]
		public IActionResult AddNews()
		{
			var model = new AddNewsVM();
			model.UserName = User.Identity.Name;
			return View(model);
		}

		[Route("AddNews")]
		[HttpPost]
		public IActionResult AddNews(AddNewsVM model)
		{
			model.UserName = User.Identity.Name;
			statsRep.ReportTheNews(model);
			return RedirectToAction(nameof(HomeController.Home));
		}
	}
}
