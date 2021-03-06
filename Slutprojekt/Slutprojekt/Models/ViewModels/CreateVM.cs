﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Slutprojekt.Models.ViewModels
{
    public class CreateVM
    {
		[Required]
        [StringLength(60, MinimumLength = 3)]
        public string UserName { get; set; }

		[Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
		public string Password { get; set; }

		//[Required]
		[DataType(DataType.EmailAddress)]
		public string Email { get; set; }

        public string Message { get; set; }
    }
}
