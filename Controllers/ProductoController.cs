using ProyectoWebApiRest.Models;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using System.Data.SqlClient;


namespace ProyectoWebApiRest.Controllers
{
    [EnableCors("NewPolicy")]
    [Route("Api/[controller]")]
    [ApiController]
    public class ProductoController : Controller
    {
        private readonly string cadenaSQL;
        public ProductoController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Listar/{ProductID:int}")]
        public IActionResult Listar(int ProductID)
        {
            List<Producto> lista = new();
            _ = new Producto();

            try
            {
                using var conexion = new SqlConnection(cadenaSQL);
                conexion.Open();
                var cmd = new SqlCommand("sp_list_products", conexion);
                cmd.Parameters.AddWithValue("ProductID", ProductID);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var rd = cmd.ExecuteReader())
                {

                    while (rd.Read())
                    {

                        lista.Add(new Producto
                        {
                            ProductID = Convert.ToInt32(rd["ProductID"]),
                            ProductName = rd["ProductName"].ToString(),
                            QuantityPerUnit = rd["QuantityPerUnit"].ToString(),
                            UnitPrice = Convert.ToDecimal(rd["UnitPrice"])
                        });
                    }
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Ok", response = lista });
            }
            catch (Exception error)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message, response = lista });
            }

        }

        [HttpPut]
        [Route("Editar")]
        public IActionResult Editar([FromBody] Producto objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_edit_products", conexion);
                    cmd.Parameters.AddWithValue("ProductID", objeto.ProductID == 0 ? DBNull.Value : objeto.ProductID);
                    cmd.Parameters.AddWithValue("ProductName", objeto.ProductName is null ? DBNull.Value : objeto.ProductName);
                    cmd.Parameters.AddWithValue("QuantityPerUnit", objeto.QuantityPerUnit is null ? DBNull.Value : objeto.QuantityPerUnit);
                    cmd.Parameters.AddWithValue("UnitPrice", objeto.UnitPrice);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto Editado" });
            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }

        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Producto objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_save_products", conexion);
                    cmd.Parameters.AddWithValue("ProductName", objeto.ProductName);
                    cmd.Parameters.AddWithValue("QuantityPerUnit", objeto.QuantityPerUnit);
                    cmd.Parameters.AddWithValue("UnitPrice", objeto.UnitPrice);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto Nuevo Agregado" });

            }
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });
            }

        }

        [HttpDelete]
        [Route("Eliminar/{ProductID:int}")]
        public IActionResult Eliminar(int ProductID)
        {
            try
            {
                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_delete_products", conexion);
                    cmd.Parameters.AddWithValue("ProductID", ProductID);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Producto Eliminado" });

            }

            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }
    }
}
