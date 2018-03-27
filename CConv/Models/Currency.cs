namespace CConv.Models
{
    public class Currency : ICurrency
    {
        public string ShortName { get; set; }
        public string Name { get; set; }
        public decimal Rate { get; set; }
    }
}
