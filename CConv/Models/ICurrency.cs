namespace CConv.Models
{
    public interface ICurrency
    {
        /// <summary>
        /// Currency code.
        /// Example: EUR, USD
        /// </summary>
        string Code { get; set; }

        /// <summary>
        /// Currency long name.
        /// Example: Euro, United States dollar
        /// </summary>
        string Name { get; set; }

        /// <summary>
        /// Conversion rate.
        /// </summary>
        decimal Rate { get; set; }
    }
}
