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
    public class StockController : Controller
    {
        private readonly string cadenaSQL;
        public StockController(IConfiguration config)
        {
            cadenaSQL = config.GetConnectionString("CadenaSQL");
        }

        [HttpGet]
        [Route("Listar/{ProductID:int}")]
        public IActionResult Listar(int ProductID)
        {
            List<Stock> lista = new();
            _ = new Stock();

            try
            {
                using var conexion = new SqlConnection(cadenaSQL);
                conexion.Open();
                var cmd = new SqlCommand("sp_stock_completo", conexion);
                cmd.Parameters.AddWithValue("p_ProductID", ProductID);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var rd = cmd.ExecuteReader())
                {

                    while (rd.Read())
                    {

                        lista.Add(new Stock
                        {
                            ProductID = Convert.ToInt32(rd["ProductID"]),
                            ProductName = rd["ProductName"].ToString(),
                            UnitsInStock = Convert.ToInt32(rd["UnitsInStock"]),
                            UnitsOnOrder = Convert.ToInt32(rd["UnitsOnOrder"]),
                            ReorderLevel = Convert.ToInt32(rd["ReorderLevel"])
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
        public IActionResult Actualizar([FromBody] Stocks objeto)
        {
            try
            {

                using (var conexion = new SqlConnection(cadenaSQL))
                {
                    conexion.Open();
                    var cmd = new SqlCommand("sp_stock_actualizar", conexion);
                    cmd.Parameters.AddWithValue("p_ProductID", objeto.ProductID);
                    cmd.Parameters.AddWithValue("p_UnitsInStock", objeto.UnitsInStock);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();

                }

                return StatusCode(StatusCodes.Status200OK, new { mensaje = "Stock Actualizado" });
            }   
            catch (Exception error)
            {

                return StatusCode(StatusCodes.Status500InternalServerError, new { mensaje = error.Message });

            }
        }
    }

}
