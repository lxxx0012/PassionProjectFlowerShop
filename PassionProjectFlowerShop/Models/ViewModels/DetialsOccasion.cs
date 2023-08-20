using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PassionProjectFlowerShop.Models.ViewModels
{
    public class DetialsOccasion
    {
        public OccasionDto SelectedOccasion { get; set; }
        public IEnumerable<FlowerDto> Flowers { get; set; }
    }
}