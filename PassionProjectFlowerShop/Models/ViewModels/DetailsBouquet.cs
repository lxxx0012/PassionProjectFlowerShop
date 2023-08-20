using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjectFlowerShop.Models.ViewModels
{
    public class DetailsBouquet
    {
        public BouquetDto SelectedBouquet { get; set; }

        public IEnumerable<FlowerDto> RelatedFlowers { get; set; }
    }
}