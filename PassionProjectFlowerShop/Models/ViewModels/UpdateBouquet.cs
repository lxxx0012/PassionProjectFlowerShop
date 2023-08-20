using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjectFlowerShop.Models.ViewModels
{
    public class UpdateBouquet
    {
        //This viewmodel is a class which stores information we needed for /Bouquet/Update/{}

        //Existing Bouquet information

        public BouquetDto SelectedBouquet { get; set; }

        //All bouquets 

        public IEnumerable<FlowerDto> FlowerOptions { get; set; }
    }
}