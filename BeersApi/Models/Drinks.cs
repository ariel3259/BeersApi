using BeersApi.Models.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeersApi.Models
{
    [Table("drinks")]
    public class Drinks: BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("alcohol_rate")]
        public int AlcoholRate { get; set; }
        [Column("price")]
        public int Price { get; set; }
        [Column("drink_type_id")]
        public Guid DrinkTypeId { get; set; }
        public virtual DrinkTypes? DrinkType { get; set; }
    }
}
