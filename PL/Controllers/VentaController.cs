using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class VentaController : Controller
    {
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Result resultDepartamento = BL.Departamento.GetAll();
            ML.Result resultProducto = BL.Producto.GetAll();
            ML.Result result = BL.Producto.GetAll();

     
            ML.Producto producto = new ML.Producto();
            producto.Departamento = new ML.Departamento();


            if (resultProducto.Correct)
            {
                producto.Departamento.Departamentos = resultDepartamento.Objects;
                producto.Productos = resultDepartamento.Objects;
            }
            return View(producto);
        }
    }
}
