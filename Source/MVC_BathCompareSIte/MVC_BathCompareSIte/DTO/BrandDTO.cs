using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_BathCompareSIte.DTO
{
    public class BrandDTO
    {
        [ScaffoldColumn(false)]
        public Int32 Id { get; set; }
        [Required(ErrorMessage = "Brand name cannot be empty.")]
        public string Name { get; set; }
        public string Description { get; set; }
        [ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime UpdateDate { get; set; }

        public IList<string> ErrorList { get; set; }
    }

    public class BrandListDTO
    {
        public IList<BrandDTO> DtoList { get; set; }
        public IList<string> ErrorList { get; set; }
    }
}