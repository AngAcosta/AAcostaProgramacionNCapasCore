using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class CargaMasivaController : Controller
    {
        private readonly IConfiguration _configuration;
        private readonly Microsoft.AspNetCore.Hosting.IHostingEnvironment _hostingEnvironment;

        public  CargaMasivaController(IConfiguration configuration, Microsoft.AspNetCore.Hosting.IHostingEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = _hostingEnvironment;
        }

        public ActionResult CargaMasiva()
        {
            ML.Result result = new ML.Result();
            return View(result);
        }

        [HttpPost]
        public ActionResult CargaMasiva(ML.Usuario usuario)
        {
            IFormFile archivo = Request.Form.Files["FileExcel"];

            if (HttpContext.Session.GetString("PathArchivo") == null)
            {
                if (archivo.Length > 0)
                {
                    string fileName = Path.GetFileName(archivo.FileName);

                    string folderPath = _configuration["PathFolder:value"];
                    string extensionArchivo = Path.GetExtension(archivo.FileName).ToLower();
                    string extensionModulo = _configuration["TipoExcel"];

                    if (extensionArchivo == extensionModulo) //VALIDACION DEL ARCHIVO EXCEL
                    {
                        string filePath = Path.Combine(_hostingEnvironment.ContentRootPath, folderPath, Path.GetFileNameWithoutExtension(fileName)) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";


                        if (!System.IO.File.Exists(filePath))
                        {
                            using (FileStream stream = new FileStream(filePath, FileMode.Create))
                            {
                                archivo.CopyTo(stream);
                            }

                            string connectionString = _configuration["ConnectionStringExcel:value"] + filePath;
                            ML.Result resultUsuarios = BL.Usuario.ConvertXSLXtoDataTable(connectionString);

                            if (resultUsuarios.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarExcel(resultUsuarios.Objects);

                                if (resultValidacion.Objects.Count == 0)
                                {
                                    resultValidacion.Correct = true;
                                    HttpContext.Session.SetString("PathArchivo", filePath);
                                }
                                return View(resultValidacion);
                            }
                            else
                            {
                                ViewBag.Message = "El Excel no contiene registros";
                            }
                        }
                    }
                    else
                    {
                        ViewBag.Message = "Favor de seleccionar un archivo .xlsx, Verifique su archivo";
                    }
                }
                else
                {
                    ViewBag.Message = "No selecciono ningun archivo";
                }
            }
            else
            {
                string rutaArchivoExcel = HttpContext.Session.GetString("PathArchivo");
                string connectionString = _configuration["ConnectionStringExcel:value"] + rutaArchivoExcel;

                ML.Result resultData = BL.Usuario.ConvertXSLXtoDataTable(connectionString);

                if (resultData.Correct)
                {
                    ML.Result resultErrores = new ML.Result();
                    resultErrores.Objects = new List<object>();

                    foreach (ML.Usuario usuarioItem in resultData.Objects)
                    {
                        ML.Result resultAdd = BL.Usuario.Add(usuarioItem);

                        if (!resultAdd.Correct)
                        {
                            resultErrores.Objects.Add("No se inserto el Usuario con Nombre: "+ usuarioItem.Nombre + "ApellidoPaterno: " + usuarioItem.ApellidoPaterno + "ApellidoMaterno: " + usuarioItem.ApellidoMaterno + "Email: " + usuarioItem.Email + "Password: " + usuarioItem.Password + "FechaNacimiento: " + usuarioItem.FechaNacimiento + "Telefono: " + usuarioItem.Telefono + "Celular: " + usuarioItem.Celular + "CURP: " + usuarioItem.CURP + "Error: " + resultAdd.Message );
                        }
                    }
                    if (resultErrores.Objects.Count > 0 )
                    {
                        string fileError = Path.Combine(_hostingEnvironment.WebRootPath, @"~\Files\logErrores.txt");
                        using (StreamWriter writer = new StreamWriter(fileError))
                        {
                            foreach (string ln in resultErrores.Objects)
                            {
                                writer.WriteLine(ln);
                            }
                        }
                        ViewBag.Message = "Los Usuarios no han sido registrados correctamente";
                    }
                    else
                    {
                        ViewBag.Message = "Los Usuarios no han sido registrsdos correctamente";
                    }
                }
            }
            return PartialView("Modal");
        }
    }
}