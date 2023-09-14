using BeersApi.Models.Abstracts;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeersApi.Models
{
    [Table("beer_types")]
    public class BeerTypes: BaseEntity
    {
        [Column("description")]
        public string Description { get; set; }
        public virtual List<Beers> Beers { get; set; }
    }
}
