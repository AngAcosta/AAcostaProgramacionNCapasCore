using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.OleDb;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Usuario
    {
        public static ML.Result Add(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {

                    int query = context.Database.ExecuteSqlRaw($"UsuarioAddSp '{usuario.UserName}','{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}','{usuario.Email}','{usuario.Password}','{usuario.FechaNacimiento}','{usuario.Sexo}','{usuario.Telefono}','{usuario.Celular}','{usuario.CURP}', '{usuario.Imagen}','{(byte)usuario.Rol.IdRol}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}','{(byte)usuario.Direccion.Colonia.IdColonia}'");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se Inserto el Ususario";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public static ML.Result Update(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {

                    int query = context.Database.ExecuteSqlRaw($"UsuarioUpdateSp '{usuario.IdUsuario}','{usuario.UserName}','{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}','{usuario.Email}','{usuario.Password}','{usuario.FechaNacimiento}','{usuario.Sexo}','{usuario.Telefono}','{usuario.Celular}','{usuario.CURP}', '{usuario.Imagen}','{(byte)usuario.Rol.IdRol}','{usuario.Direccion.Calle}','{usuario.Direccion.NumeroInterior}','{usuario.Direccion.NumeroExterior}','{(byte)usuario.Direccion.Colonia.IdColonia}'");

                    if (query >= 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se Modifico el Usuario";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;

        }
        public static ML.Result Delete(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {
                    //var query = context.UsuarioDeleteSp(IdUsuario);
                    var query = context.Database.ExecuteSqlRaw($"UsuarioDeleteSp {IdUsuario}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se Elimino el Usuario";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }
        public static ML.Result GetAll(ML.Usuario usuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {
                    //var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll").ToList();
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetAll '{usuario.Nombre}','{usuario.ApellidoPaterno}','{usuario.ApellidoMaterno}'").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            usuario = new ML.Usuario(); // VACIA LOS DATOS SOLICITADOS PARA GUARDAR LOS DATOS DEL REGISTRO

                            usuario.IdUsuario = obj.IdUsuario;
                            usuario.UserName = obj.UserName;
                            usuario.Nombre = obj.Nombre;
                            usuario.ApellidoPaterno = obj.ApellidoPaterno;
                            usuario.ApellidoMaterno = obj.ApellidoMaterno;
                            usuario.Email = obj.Email;
                            usuario.Password = obj.Password;
                            usuario.FechaNacimiento = obj.FechaNacimiento.ToString("dd/MM/yyyy");
                            usuario.Sexo = obj.Sexo;
                            usuario.Telefono = obj.Telefono;
                            usuario.Celular = obj.Celular;
                            usuario.CURP = obj.Curp;
                            usuario.Imagen = obj.Imagen;


                            usuario.Rol = new ML.Rol();
                            usuario.Rol.IdRol = obj.IdRol.Value;
                            usuario.Rol.Nombre = obj.NombreRol;

                            usuario.Direccion = new ML.Direccion();
                            usuario.Direccion.IdDireccion = obj.IdDireccion;
                            usuario.Direccion.Calle = obj.Calle;
                            usuario.Direccion.NumeroInterior = obj.NumeroInterior;
                            usuario.Direccion.NumeroExterior = obj.NumeroExteriro;

                            usuario.Direccion.Colonia = new ML.Colonia();
                            usuario.Direccion.Colonia.IdColonia = obj.IdColonia;

                            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                            usuario.Direccion.Colonia.Municipio.IdMunicipio = obj.IdColonia;

                            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                            usuario.Direccion.Colonia.Municipio.Estado.IdEstado = obj.IdEstado;

                            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                            usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = obj.IdPais;

                            usuario.Status = obj.Status.Value;


                            result.Objects.Add(usuario);
                        }
                    }
                    result.Correct = true;
                }
                //result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Ex = ex;

            }
            return result;
        }
        public static ML.Result GetByIdEF(int IdUsuario)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetById {IdUsuario}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString("dd/MM/yyyy");
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.CURP = query.Curp;
                        usuario.Imagen = query.Imagen;


                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol.Value;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = query.IdDireccion;
                        usuario.Direccion.Calle = query.Calle;
                        usuario.Direccion.NumeroInterior = query.NumeroInterior;
                        usuario.Direccion.NumeroExterior = query.NumeroExteriro;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais;

                        usuario.Status = query.Status.Value;


                        result.Object = usuario;
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result ChangeStatus(int IdUsuario, bool Status)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {

                    int query = context.Database.ExecuteSqlRaw($"UsuarioChangeStatus '{IdUsuario}','{Status}'");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se Activo el Status";
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result UsuarioGetbyUserName(string UserName)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {
                    var query = context.Usuarios.FromSqlRaw($"UsuarioGetByUserName '{UserName}'").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Usuario usuario = new ML.Usuario();

                        usuario.IdUsuario = query.IdUsuario;
                        usuario.UserName = query.UserName;
                        usuario.Nombre = query.Nombre;
                        usuario.ApellidoPaterno = query.ApellidoPaterno;
                        usuario.ApellidoMaterno = query.ApellidoMaterno;
                        usuario.Email = query.Email;
                        usuario.Password = query.Password;
                        usuario.FechaNacimiento = query.FechaNacimiento.ToString("dd/MM/yyyy");
                        usuario.Sexo = query.Sexo;
                        usuario.Telefono = query.Telefono;
                        usuario.Celular = query.Celular;
                        usuario.CURP = query.Curp;
                        usuario.Imagen = query.Imagen;


                        usuario.Rol = new ML.Rol();
                        usuario.Rol.IdRol = query.IdRol.Value;

                        usuario.Direccion = new ML.Direccion();
                        usuario.Direccion.IdDireccion = query.IdDireccion;
                        usuario.Direccion.Calle = query.Calle;
                        usuario.Direccion.NumeroInterior = query.NumeroInterior;
                        usuario.Direccion.NumeroExterior = query.NumeroExteriro;

                        usuario.Direccion.Colonia = new ML.Colonia();
                        usuario.Direccion.Colonia.IdColonia = query.IdColonia;

                        usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                        usuario.Direccion.Colonia.Municipio.IdMunicipio = query.IdMunicipio;

                        usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                        usuario.Direccion.Colonia.Municipio.Estado.IdEstado = query.IdEstado;

                        usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                        usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais = query.IdPais;

                        usuario.Status = query.Status.Value;

                        result.Object = usuario;
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = true;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result ConvertXSLXtoDataTable(string connString)
        {
            ML. Result result =  new ML.Result();

            try
            {
                using (OleDbConnection context = new OleDbConnection(connString))
                {
                    //string query = "SELECT * FROM [HOJAS1$]";
                    string query = "SELECT * FROM [Sheet1]";
                    using (OleDbCommand cmd = new OleDbCommand())
                    {
                        cmd.CommandText = query;
                        cmd.Connection = context;

                        OleDbDataAdapter da = new OleDbDataAdapter();
                        da.SelectCommand = cmd;

                        DataTable tableUsuario = new DataTable();
                        da.Fill(tableUsuario);

                        if (tableUsuario.Rows.Count > 0)
                        {
                            result.Objects = new List<object>();

                            foreach (DataRow row in tableUsuario.Rows)
                            {
                                ML.Usuario usuario = new ML.Usuario();
                                usuario.Nombre = row[0].ToString();
                                usuario.ApellidoPaterno = row[1].ToString();
                                usuario.ApellidoMaterno = row[3].ToString();
                                usuario.Email = row[4].ToString();
                                usuario.Password = row[5].ToString();
                                usuario.Sexo = row[6].ToString();
                                usuario.Telefono = row[7].ToString();
                                usuario.Celular = row[8].ToString();
                                usuario.CURP = row[9].ToString();
                                
                                usuario.Status = bool.Parse(row[10].ToString());

                                result.Objects.Add(usuario);
                            }
                            result.Correct = true;
                        }
                        result.Object = tableUsuario;

                        if (tableUsuario.Rows.Count > 1)
                        {
                            result.Correct = true;
                        }
                        else
                        {
                            result.Correct = false;
                            result.Message = "No existe registro em el excel";
                        }
                    }
                }
            }
            catch(Exception ex)
            {
                result.Correct=false;
                result.Message = ex.Message;
            }
            return result;
        }

        public static ML.Result ValidarExcel(List<object> Object)
        {
            ML.Result result = new ML.Result();

            try
            {
                result.Objects = new List<object>();

                int i = 1;

                foreach (ML.Usuario usuario in Object)
                {
                    ML.ErrorExcel error = new ML.ErrorExcel();
                    error.IdRegistro = i++;

                    if (usuario.UserName == "")
                    {
                        error.Mensaje += "Ingresar el UserName";
                    }

                    if (usuario.Nombre == "")
                    {
                        error.Mensaje += "Ingresar el Nombre";
                    }

                    if (usuario.ApellidoPaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Paterno";
                    }

                    if (usuario.ApellidoMaterno == "")
                    {
                        error.Mensaje += "Ingresar el Apellido Materno";
                    }

                    if (usuario.Email == "")
                    {
                        error.Mensaje += "Ingresar el Email";
                    }

                    if (usuario.Password == "")
                    {
                        error.Mensaje += "Ingresar el Password";
                    }

                    if (usuario.FechaNacimiento == "")
                    {
                        error.Mensaje += "Ingresar la Fecha";
                    }

                    if (usuario.Sexo == "")
                    {
                        error.Mensaje += "Ingresae el Sexo";
                    }

                    if (usuario.Telefono == "")
                    {
                        error.Mensaje += "Ingresar el Telefono";
                    }

                    if (usuario.Celular == "")
                    {
                        error.Mensaje += "Ingresar el Celular";
                    }

                    if (usuario.CURP == "")
                    {
                        error.Mensaje += "Ingresar CURP";
                    }

                    if (error.Mensaje != null)
                    {
                        result.Objects.Add(error);
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct=false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}