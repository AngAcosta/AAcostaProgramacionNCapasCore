using Microsoft.AspNetCore.Mvc;

namespace PL.Controllers
{
    public class DepartamentoController : Controller
    {
        // GET: Departamento
        [HttpGet]
        public ActionResult GetAll()
        {
            ML.Departamento departamento = new ML.Departamento();
            ML.Result result = BL.Departamento.GetAll();//EF


            if (result.Correct)
            {
                departamento.Departamentos = result.Objects;
                return View(departamento);
            }
            else
            {
                return View(departamento);
            }
        }

        [HttpGet]
        public ActionResult Form(int? IdDepartamento)
        {
            ML.Result resultArea = BL.Area.GetAll();

            ML.Departamento departamento = new ML.Departamento();
            departamento.Area = new ML.Area();

            if (resultArea.Correct)
            {
                departamento.Area.Areas = resultArea.Objects;
            }
            if (IdDepartamento == null)
            {
                //add //formulario vacio
                return View(departamento);
            }
            else
            {
                //getById
                ML.Result result = BL.Departamento.GetById(IdDepartamento.Value);

                if (result.Correct)
                {
                    departamento = (ML.Departamento)result.Object; //unboxing
                    departamento.Area.Areas = resultArea.Objects;

                    //update
                    return View(departamento);
                }
                else
                {
                    ViewBag.Message = "Ocurrio al consultar la informacion del Departamento";
                    return View("Modal");
                }
            }
        }

        [HttpPost]
        public ActionResult Form(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            if (departamento.IdDepartamento == 0)
            {
                //add
                result = BL.Departamento.Add(departamento);

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
                result = BL.Departamento.Update(departamento);

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
        public ActionResult Delete(int IdDepartamento)
        {
            ML.Result result = BL.Departamento.Delete(IdDepartamento);

            if (result.Correct)
            {
                ViewBag.message = "Se elimino el registro";
                return View("modal");
            }
            else
            {
                ViewBag.message = "No se elimino el registro";
            }
            return View("Model");
        }
    }
}