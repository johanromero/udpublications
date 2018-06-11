using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Modelo.Models
{
    public class LoginViewModel
    {
        [Required(ErrorMessage = "Ingrese el nombre de usuario")]
        [Display(Name = "Nombre de usuario")]
        public string Username { get; set; }

        [Required]
        [Display(Name = "Ingrese la contraseña")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        
    }
}
