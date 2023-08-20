using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjectFlowerShop.Models.ViewModels
{
    public class UpdateFlower
    {
        //This viewmodel is a class which stores information we needed for /Bouquet/Update/{}

        //Existing Bouquet information

        public FlowerDto SelectedFlower { get; set; }

        //All bouquets 

        public IEnumerable<BouquetDto> BouquetOptions { get; set; }
    }
}