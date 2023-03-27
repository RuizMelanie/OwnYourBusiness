namespace ProyectoWebApiRest.Models
{
    public class Stock
    {
        public int ProductID { get; set; }
        public string ProductName { get; set; }
        public int UnitsInStock { get; set; }   
        public int UnitsOnOrder { get; set; }
        public int ReorderLevel { get; set; }
    }
}
