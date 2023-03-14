using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace ML
{
    public class Usuario
    {
        public int IdUsuario { get; set; }

        public string UserName { get; set; }


        //[Required]
        //[RegularExpression("^[A-Za-z]+$", ErrorMessage = "Solo inserta letras")]
        public string Nombre { get; set; }


        //[Required]
        //[RegularExpression("^[A-Za-z]+$", ErrorMessage = "Solo inserta letras")]
        public string ApellidoPaterno { get; set; }


        //[Required]
        //[RegularExpression("^[A-Za-z]+$", ErrorMessage = "Solo inserta letras")] 
        public string ApellidoMaterno { get; set; }


        //[Required]
        //[RegularExpression(@"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$", ErrorMessage = "Ingresa un Email valido")]
        public string Email { get; set; }


        //[Required]
        //[RegularExpression(@"^(?=\w*\d)(?=\w*[A-Z])(?=\w*[a-z])\S{8,16}$", ErrorMessage = "El Password debe de tener un caracter especial, Una mayuscula y que sea de minimo 8 Carateres")]
        public string Password { get; set; }

        public string FechaNacimiento { get; set; }

        public string Sexo { get; set; }


        //[Required]
        //[RegularExpression("^[0-9]+$", ErrorMessage = "Solo inserta Numeros")]
        public string Telefono { get; set; }


        //[Required]
        //[RegularExpression("^[0-9]+$", ErrorMessage = "Solo inserta Numeros")]
        public string Celular { get; set; }

        public string CURP { get; set; }

        public string Imagen { get; set; }

        public ML.Direccion Direccion { get; set; }

        public ML.Rol Rol { get; set; }

        public bool Status { get; set; }

        public List<object> Usuarios { get; set; }
    }
}
