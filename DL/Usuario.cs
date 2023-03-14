using System;
using System.Collections.Generic;

namespace DL;

public partial class Usuario
{
    public int IdUsuario { get; set; }

    public string UserName { get; set; } = null!;

    public string? Nombre { get; set; }

    public string? ApellidoPaterno { get; set; }

    public string? ApellidoMaterno { get; set; }

    public string? Email { get; set; }

    public string Password { get; set; } = null!;

    public DateTime FechaNacimiento { get; set; }

    public string Sexo { get; set; } = null!;

    public string Telefono { get; set; } = null!;

    public string? Celular { get; set; }

    public string? Curp { get; set; }

    public string? Imagen { get; set; }

    public int? IdRol { get; set; }

    public bool? Status { get; set; }

    public virtual ICollection<Direccion> Direccions { get; } = new List<Direccion>();

    public virtual Rol? IdRolNavigation { get; set; }

    public virtual ICollection<Ventum> Venta { get; } = new List<Ventum>();

    //Agredados para sql


    public string NombreRol { get; set; }

    public int IdDireccion { get; set; }

    public string NombrePais { get; set; }

    public string Calle { get; set; }

    public string NumeroInterior { get; set; }

    public string NumeroExteriro { get; set; }

    public int IdColonia { get; set; }

    public int IdMunicipio { get; set; }

    public int IdEstado { get; set; }

    public int IdPais { get; set; }

    public string NombreEStado { get; set; }

    public string NombreMunicipio { get; set; }

    public string NombreColonia { get; set; }
}
