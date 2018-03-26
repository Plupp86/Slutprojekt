﻿using System;
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
	//[Authorize]
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

            var(succes, mess)= await repository.CreateUserAsync(model);

            if (!succes)
            {
                model.Message = mess;
                ModelState.AddModelError(nameof(CreateVM.Password), "Wrong input");
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

			//SignalR
			return View();
        }
        //@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SlutprojektDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
        //Scaffold-DbContext "Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=SlutprojektDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False" Microsoft.EntityFrameworkCore.SqlServer -OutputDir "Models/Entities" -Context "SlutprojektDBContext" -Force
    }
}
