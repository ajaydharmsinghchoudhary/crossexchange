using System;
using System.ComponentModel.DataAnnotations;

namespace CrossExchange
{
    public class HourlyShareRateModel
    {
        [Required]
        public DateTime TimeStamp { get; set; }

        [Required]
        public string Symbol { get; set; }

        [Required]
        public decimal Rate { get; set; }
    }
}
