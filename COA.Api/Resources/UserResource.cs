using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace COA.Api.Resources
{
    public class UserResource
    {
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no debe superar los 50 caracteres")]
        public string UserName { get; set; }
        
        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no debe superar los 50 caracteres")]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Ingrese un nombre válido")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio")]
        [StringLength(50, ErrorMessage = "El campo {0} no debe superar los 50 caracteres")]
        [EmailAddress(ErrorMessage = "Ingrese un Email válido")]
        public string Email { get; set; }

        [StringLength(50, ErrorMessage = "El campo {0} no debe superar los 20 caracteres")]
        public string Telefono { get; set; }
    }
}
