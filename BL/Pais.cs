using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Pais
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {
                    var query = context.Pais.FromSqlRaw ("PaisGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Pais pais = new ML.Pais();

                            pais.IdPais = obj.IdPais;
                            pais.Nombre = obj.Nombre;

                            result.Objects.Add(pais);
                        }
                    }
                }
                result.Correct = true;
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}