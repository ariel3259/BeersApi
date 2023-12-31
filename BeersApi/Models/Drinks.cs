﻿using BeersApi.Models.Abstractions;
using System.ComponentModel.DataAnnotations.Schema;

namespace BeersApi.Models
{
    [Table("drinks")]
    public class Drinks: BaseEntity
    {
        [Column("name")]
        public string Name { get; set; }
        [Column("alcohol_rate")]
        public double AlcoholRate { get; set; }
        [Column("price")]
        public int Price { get; set; }
        [Column("drink_type_id")]
        public int DrinkTypeId { get; set; }
        public virtual DrinkTypes? DrinkType { get; set; }
    }
}
