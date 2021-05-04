using System;
using System.Collections.Generic;

#nullable disable

namespace COA.Data.Models
{
    public partial class Usuario
    {
        public int IdUsuario { get; set; }
        public string UserName { get; set; }
        public string Nombre { get; set; }
        public string Email { get; set; }
        public string Telefono { get; set; }
    }
}
