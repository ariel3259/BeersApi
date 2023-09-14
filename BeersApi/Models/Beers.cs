using BeersApi.Models.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeersApi.Models
{
    [Table("beers")]
    public class Beers: BaseEntity
    {
        [Column("Name")]
        public string Name { get; set; }
        [Column("alcohol_rate")]
        public int AlcoholRate { get; set; }
        [Column("price")]
        public int Price { get; set; }
        [Column("beer_type_id")]
        public Guid BeerTypeId { get; set; }
        public virtual BeerTypes? BeerTypes { get; set; }
    }
}
