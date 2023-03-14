using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class UsuarioController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAll(usuario);//EF
           

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario) //BUSCAR POR NOMB,AP,AM
        {
           
            ML.Result result = BL.Usuario.GetAll(usuario); //EF
            //ML.Usuario usuario = new ML.Usuario();

            if (result.Correct)
            {
                usuario.Usuarios = result.Objects;
                return View(usuario);
            }
            else
            {
                return View(usuario);
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdUsuario)
        {
            ML.Result resultRol = BL.Rol.GetAll();
            ML.Result resultPais = BL.Pais.GetAll();

            ML.Usuario usuario = new ML.Usuario();
            usuario.Rol = new ML.Rol();


            usuario.Direccion = new ML.Direccion();
            usuario.Direccion.Colonia = new ML.Colonia();
            usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            if (resultRol.Correct && resultPais.Correct)
            {
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
                usuario.Rol.Roles = resultRol.Objects;
            }
            if (IdUsuario == null)
            {
                //add //formulario vacio
                return View(usuario);
            }
            else
            {
                //getById
                ML.Result result = BL.Usuario.GetByIdEF(IdUsuario.Value);

                if (result.Correct)
                {
                    usuario = (ML.Usuario)result.Object; //unboxing
                    usuario.Rol.Roles = resultRol.Objects;
                    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;

                    ML.Result resultEstado = BL.Estado.EstadoGetByIdPais(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                    ML.Result resultMunicipio = BL.Municipio.MunicipioGetByIdEstado(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                    ML.Result resultColonia = BL.Colonia.ColoniaGetByIdMunicipio(usuario.Direccion.Colonia.Municipio.IdMunicipio);

                    usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstado.Objects;
                    usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipio.Objects;
                    usuario.Direccion.Colonia.Colonias = resultColonia.Objects;

                    //update
                    return View(usuario);
                }
                else
                {
                    ViewBag.Message = "Ocurrio al consultar la informacion del Usuario";
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            //HttpPostedFileBase file = Request.Files["fuImage"];
            IFormFile file = Request.Form.Files["fuImage"];
            
            if (file != null)
            {
                byte[] imagen = ConvertToBytes(file);

                usuario.Imagen = Convert.ToBase64String(imagen);
            }

            ML.Result result = new ML.Result();

            //if (ModelState.IsValid == true)
            //{

                if (usuario.IdUsuario == 0)
                {
                    //add
                    result = BL.Usuario.Add(usuario);

                    if (result.Correct)
                    {
                        ViewBag.Message = "Se Completo del Registro";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al Insertar el Registro";
                    }
                    return View("Modal");
                }
                else
                {
                    //update
                    result = BL.Usuario.Update(usuario);

                    if (result.Correct)
                    {
                        ViewBag.Message = "Se Actualizo el Registro";
                    }
                    else
                    {
                        ViewBag.Message = "Ocurrio un error al Actualizar el Registro";
                    }
                    return View("Modal");
                }
            //}
            //else
            //{
            //    usuario.Rol = new ML.Rol();

            //    usuario.Direccion = new ML.Direccion();
            //    usuario.Direccion.Colonia = new ML.Colonia();
            //    usuario.Direccion.Colonia.Municipio = new ML.Municipio();
            //    usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
            //    usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();

            //    ML.Result resultRol = BL.Rol.GetAll();
            //    ML.Result resultPais = BL.Pais.GetAll();

            //    usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPais.Objects;
            //    usuario.Rol.Roles = resultRol.Objects;
            //    return View(usuario);
            //}
        }

        [HttpGet]
        public ActionResult Delete(int IdUsuario)
        {
            ML.Result result = BL.Usuario.Delete(IdUsuario);

            if (result.Correct)
            {
                ViewBag.message = "se elimino el registro";
                return View("modal");
            }
            else
            {
                ViewBag.message = "no se elimino el registro";
            }
            return View("Model");
        }

        
        public JsonResult EstadoGetByIdPais(int IdPais)
        {
            ML.Result result = new ML.Result();
            result = BL.Estado.EstadoGetByIdPais(IdPais);

            return Json(result.Objects);
        }

        public JsonResult MunicipioGetByIdEstado(int IdEstado)
        {
            ML.Result result = new ML.Result();
            result = BL.Municipio.MunicipioGetByIdEstado(IdEstado);

            return Json(result.Objects);
        }

        public JsonResult ColoniaGetByIdMunicipio(int IdMunicipio)
        {
            ML.Result result = new ML.Result();
            result = BL.Colonia.ColoniaGetByIdMunicipio(IdMunicipio);

            return Json(result.Objects);
        }

        public byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            return bytes;
        }

        public JsonResult ChangeStatus(int IdUsuario , bool Status)
        {
            ML.Result result = BL.Usuario.ChangeStatus(IdUsuario , Status);

            return Json(result.Objects);
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Login(string UserName, string Password)
        {
            ML.Result result = BL.Usuario.UsuarioGetbyUserName(UserName);

            if (result.Correct)
            {
                ML.Usuario usuario = (ML.Usuario)result.Object;

                if (Password == usuario.Password)
                {
                    return RedirectToAction("GetAll");
                }
                else
                {
                    ViewBag.Message = "Contraseña Incorrecta";
                    return PartialView("Modal");
                }
            }
            else
            {
                ViewBag.Message = "El UserName no existe";
                return PartialView("Modal");
            }
        }
    }
}