using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class ProductoController : Controller
    {
        [HttpGet]
        [Route("api/Producto/GetAll")]
        public ActionResult GetAll()
        {
            ML.Producto producto = new ML.Producto();
            ML.Result result = BL.Producto.GetAll();

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
            return View();
        }

        [HttpPost]
        [Route("api/Producto/Add")]
        public ActionResult Add([FromBody] ML.Producto producto)
        {
            ML.Result result = BL.Producto.Add(producto);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/Producto/Update")]
        public ActionResult Update([FromBody] ML.Producto producto)
        {
            ML.Result result = BL.Producto.Update(producto);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/Producto/Delete")]
        public ActionResult Delete([FromBody] int IdProducto)
        {
            ML.Result result = BL.Producto.Delete(IdProducto);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [HttpPost]
        [Route("api/Producto/GetById")]
        public ActionResult GetById([FromBody] int IdProducto)
        {
            ML.Result result = BL.Producto.GetById(IdProducto);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}