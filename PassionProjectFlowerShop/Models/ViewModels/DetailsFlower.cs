using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjectFlowerShop.Models.ViewModels
{
    public class DetailsFlower
    {
        public FlowerDto SelectedFlower { get; set; }
        public IEnumerable<OccasionDto> SpecialOccasions { get; set; }
    }
}