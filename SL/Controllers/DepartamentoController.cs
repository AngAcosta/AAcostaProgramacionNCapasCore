using Microsoft.AspNetCore.Mvc;

namespace SL.Controllers
{
    public class DepartamentoController : Controller
    {
        [HttpGet]
        [Route("api/Departamento/GetAll")]
        public ActionResult GetAll()
        {
            ML.Departamento departamento = new ML.Departamento();
            ML.Result result = BL.Departamento.GetAll();

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
        [Route("api/Departamento/Add")]
        public ActionResult Add([FromBody] ML.Departamento departamento)
        {
            ML.Result result = BL.Departamento.Add(departamento);

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
        [Route("api/Departamento/Update")]
        public ActionResult Update([FromBody] ML.Departamento departamento)
        {
            ML.Result result = BL.Departamento.Update(departamento);

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
        [Route("api/Departamento/Delete")]
        public ActionResult Delete([FromBody] int IdDepartamento)
        {
            ML.Result result = BL.Departamento.Delete(IdDepartamento);

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
        [Route("api/Departamento/GetById")]
        public ActionResult GetById([FromBody] int IdDepartamento)
        {
            ML.Result result = BL.Departamento.GetById(IdDepartamento);

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