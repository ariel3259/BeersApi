using BeersApi.Models.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeersApi.Models
{
    [Table("drink_types")]
    public class DrinkTypes: BaseEntity
    {
        [Column("description")]
        public string Description { get; set; }
        public virtual List<Drinks> Drinks { get; set; }
    }
}
