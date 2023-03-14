using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL
{
    public class Departamento
    {
        public static ML.Result Add(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {
                    int query = context.Database.ExecuteSqlRaw($"DepartamentoAddSp '{departamento.Nombre}', '{(byte)departamento.Area.IdArea}'");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se Inserto el Departamento";
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
        public static ML.Result Update(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {

                    int query = context.Database.ExecuteSqlRaw($"DepartamentoUpdateSp '{departamento.IdDepartamento}','{departamento.Nombre}','{departamento.Area.IdArea}'");

                    if (query >= 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se Modifico el Departamento";
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
        public static ML.Result Delete(int IdDepartamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"DepartamentoDeleteSp {IdDepartamento}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se Elimino el Departamento";
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
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw("DepartamentoGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();
                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();

                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;

                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = obj.IdArea.Value;
                            departamento.Area.Nombre = obj.NombreArea;

                            result.Objects.Add(departamento);
                        }
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Ex = ex;

            }
            return result;
        }
        public static ML.Result GetById(int IdDepartamento)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {
                    var query = context.Departamentos.FromSqlRaw($"DepartamentoGetById {IdDepartamento}").AsEnumerable().FirstOrDefault();
                   

                    if (query != null)
                    {
                        ML.Departamento departamento = new ML.Departamento();

                        departamento.IdDepartamento = query.IdDepartamento;
                        departamento.Nombre = query.Nombre;

                        departamento.Area = new ML.Area();
                        departamento.Area.IdArea = query.IdArea.Value;
                        departamento.Area.Nombre = query.NombreArea;

                        result.Object = departamento;
                    }
                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {

            }
            return result;
        }

    }
}