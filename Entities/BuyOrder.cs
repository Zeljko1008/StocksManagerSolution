using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Entities
{
    public class BuyOrder
    {
        [Key]
        public Guid BuyOrderID { get; set; }
        [Required(ErrorMessage = "Stock Symbol is required")]
        [StringLength(20)]
        public string? StockSymbol { get; set; }

        [Required(ErrorMessage = "Stock Name is required")]
        [StringLength(100)]
        public string? StockName { get; set; }

        public DateTime DateAndTimeOfOrder { get; set; }
        [Range(1, 99999, ErrorMessage = "{0} shuold be between {1} and {2}")]

        public uint Quantity { get; set; }
        [Range(1, 99999.99, ErrorMessage = "{0} shuold be between {1} and {2}")]
        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

       
    }
}
