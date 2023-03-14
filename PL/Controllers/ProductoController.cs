using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class ProductoController : Controller
    {
        // GET: Usuario
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            ML.Result result = BL.Producto.GetAll();//EF


            if (result.Correct)
            {
                producto.Productos = result.Objects;
                return View(producto);
            }
            else
            {
                return View(producto);
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdProducto)
        {
            ML.Result resultDepartamento = BL.Departamento.GetAll();
            ML.Result resultProveedor = BL.Proveedor.GetAll();

            ML.Producto producto = new ML.Producto();
            producto.Departamento = new ML.Departamento();
            producto.Proveedor= new  ML.Proveedor();

            if (resultDepartamento.Correct && resultProveedor.Correct)
            {
                producto.Departamento.Departamentos = resultDepartamento.Objects;
                producto.Proveedor.Proveedores = resultProveedor.Objects;
            }
            if (IdProducto == null)
            {
                //add //formulario vacio
                return View(producto);
            }
            else
            {
                //getById
                ML.Result result = BL.Producto.GetById(IdProducto.Value);

                if (result.Correct)
                {
                    producto = (ML.Producto)result.Object; //unboxing
                    producto.Proveedor.Proveedores = resultProveedor.Objects;
                    producto.Departamento.Departamentos = resultDepartamento.Objects;

                    //update
                    return View(producto);
                }
                else
                {
                    ViewBag.Message = "Ocurrio al consultar la informacion del Producto";
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            //HttpPostedFileBase file = Request.Files["fuImage"];
            IFormFile file = Request.Form.Files["fuImage"];

            if (file != null)
            {
                byte[] imagen = ConvertToBytes(file);

                producto.Imagen = Convert.ToBase64String(imagen);
            }

            ML.Result result = new ML.Result();

            if (producto.IdProducto == 0)
            {
                //add
                result = BL.Producto.Add(producto);

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
                result = BL.Producto.Update(producto);

                if (result.Correct)
                {
                    ViewBag.Message = "Se Actualizo el Registro";
                    return View("Modal");
                }
                else
                {
                    ViewBag.Message = "Ocurrio un error al Actualizar el Registro";
                    return View("Modal");
                }
                return View("Modal");
            }
        }

        [HttpGet]
        public ActionResult Delete(int IdProducto)
        {
            ML.Result result = BL.Producto.Delete(IdProducto);

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

        public byte[] ConvertToBytes(IFormFile imagen)
        {
            using var fileStream = imagen.OpenReadStream();

            byte[] bytes = new byte[fileStream.Length];
            fileStream.Read(bytes, 0, (int)fileStream.Length);
            return bytes;
        }
    }
}