using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_BathCompareSIte.Forms
{
    public class SearchForm
    {
        public string ProductName { get; set; }
        public string CategoryId { get; set; }
        public string PriceMinimum { get; set; }
        public string PriceMaximum { get; set; }
        public string BrandId { get; set; }
        public string OtherFilter { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
    }
}