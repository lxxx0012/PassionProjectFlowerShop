using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PassionProjectFlowerShop.Models
{
    public class Flower
    {
        [Key]
        public int FlowerId { get; set; }
        public string FlowerName { get; set; }
        public string FlowerColor { get; set; }
        public int FlowerPrice { get; set; }

        
        [ForeignKey("Bouquet")]
        //public int OccasionId { get; set; }
        //public virtual Occasion Occasion { get; set; }
        public int BouquetId { get; set; }
        public string BouquetName { get; set; }
        public int OccasionId { get; set; }
        public string OccasionName { get; set; }
        public virtual Bouquet Bouquet { get; set; }

        public ICollection<Occasion> Occasions { get; set; }

    }

    public class FlowerDto
    {
        public int FlowerId { get; set; }
        public string FlowerName { get; set; }
        public string FlowerColor { get; set;}
        public int FlowerPrice { get; set;}
        public int OccasionId { get;set; }
        public string OccasionName { get;set; }
        public int BouquetId { get; set; }
        public string BouquetName { get; set; }
    }
}