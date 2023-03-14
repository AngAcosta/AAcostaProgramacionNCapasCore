using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace BL
{
    public class Producto
    {
        public static ML.Result Add(ML.Producto producto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {

                    int query = context.Database.ExecuteSqlRaw($"ProductoAddSp '{producto.Nombre}','{producto.PrecioUnitario}','{producto.Stock}','{producto.Proveedor.IdProveedor}','{producto.Departamento.IdDepartamento}','{producto.Descripcion}','{producto.Imagen}'");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se Inserto el Producto";
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
        public static ML.Result Update(ML.Producto producto )
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {

                    int query = context.Database.ExecuteSqlRaw($"ProductoUpdateSp '{producto.IdProducto}','{producto.Nombre}','{producto.PrecioUnitario}','{producto.Stock}','{producto.Proveedor.IdProveedor}','{producto.Departamento.IdDepartamento}','{producto.Descripcion}','{producto.Imagen}'");

                    if (query >= 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se Modifico el Producto";
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
        public static ML.Result Delete(int IdProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"ProductoDeleteSp {IdProducto}");
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.Message = "No se Elimino el Producto";
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
                    var query = context.Productos.FromSqlRaw($"ProductoGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();

                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.PrecioUnitario = obj.PrecioUnitario;
                            producto.Stock = obj.Stock;

                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = obj.IdProveedor.Value;
                            producto.Proveedor.Nombre = obj.NombreProveedor;

                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = obj.IdDepartamento.Value;
                            producto.Departamento.Nombre = obj.NombreDepartamento;

                            producto.Descripcion = obj.Descripcion;
                            producto.Imagen = obj.Imagen;                          

                            result.Objects.Add(producto);
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
        public static ML.Result GetById(int IdProducto)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AacostaProgramacionNcapasContext context = new DL.AacostaProgramacionNcapasContext())
                {
                    var query = context.Productos.FromSqlRaw($"ProductoGetById {IdProducto}").AsEnumerable().FirstOrDefault();

                    if (query != null)
                    {
                        ML.Producto producto = new ML.Producto();

                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.PrecioUnitario = query.PrecioUnitario;
                        producto.Stock = query.Stock;

                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = query.IdProveedor.Value;
                        producto.Proveedor.Nombre = query.NombreProveedor;

                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = query.IdDepartamento.Value;
                        producto.Departamento.Nombre = query.NombreDepartamento;

                        producto.Descripcion = query.Descripcion;
                        producto.Imagen = query.Imagen;


                        result.Object = producto;
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
    }
}