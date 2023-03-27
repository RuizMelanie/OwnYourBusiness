using System.Data.SqlTypes;

namespace ProyectoWebApiRest.Models
{
    public class Producto
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public string QuantityPerUnit { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
