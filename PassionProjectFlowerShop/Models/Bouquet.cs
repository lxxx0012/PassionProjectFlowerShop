using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PassionProjectFlowerShop.Models
{
    public class Bouquet
    {
        [Key]
        public int BouquetId { get; set; }
        public string BouquetName { get; set; }
        public decimal BouquetPrice { get; set; }
        public string BouquetDescription { get; set; }

        //A Bouquet has A flower
        //A flower can be in any occasions
    }

    public class BouquetDto
    {
        public int BouquetId { get; set; }
        public string BouquetName { get; set; }
        public decimal BouquetPrice { get; set; }
        public string BouquetDescription { get; set; }

    }
}