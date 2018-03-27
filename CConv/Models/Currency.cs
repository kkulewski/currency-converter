namespace CConv.Models
{
    public class Currency : ICurrency
    {
        public string Code { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
    }
}
