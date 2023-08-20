using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace PassionProjectFlowerShop.Models
{
    public class Occasion
    {
        [Key]
        public int OccasionId { get; set; }
        public string OccasionName { get; set; }
        public DateTime OccasionDate { get; set; }

        //An occasion can be flower
        //An occasion can be bouquet

        public ICollection<Flower> Flowers { get; set; }
    }

    public class OccasionDto
    {
        public int OccasionId { get; set; }
        public string OccasionName { get; set; }
        public DateTime OccasionDate { get; set; }
    }
}