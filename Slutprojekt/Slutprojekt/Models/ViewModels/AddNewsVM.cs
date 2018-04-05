using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Slutprojekt.Models.ViewModels
{
    public class AddNewsVM
    {
		public string UserName { get; set; }

		[Required(ErrorMessage = "You forgot something...")]
		[Display(Name = "The News:")]
		public string TheNews { get; set; }
	}
}
