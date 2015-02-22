using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_BathCompareSIte.DTO
{
    public class CategoryDTO
    {
        [ScaffoldColumn(false)]
        public Int32 Id { get; set; }

        [Required(ErrorMessage = "Name field cannot be empty.")]
        public string Name { get; set; }
        public string Description { get; set; }
        public string ParentCategoryId { get; set; }

        [ScaffoldColumn(false)]
        public DateTime CreateDate { get; set; }
        [ScaffoldColumn(false)]
        public DateTime UpdateDate { get; set; }

        //unbound properties
        public IList<string> ErrorList { get; set; }
        public string Parent { get; set; }
        public int selIndex { get; set; }
        public string ParentList { get; set; }
    }

    [Serializable]
    public class CategoryTreeDto
    {
        public string id { get; set; }
        public string text { get; set; }
        public string url { get; set; }
        public string imageUrl { get; set; }
        public string spriteCssClass { get; set; }
        public string hasChildren { get; set; }
    }

    [Serializable]
    public class ColorTreeDto
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    [Serializable]
    public class SizeTreeDto
    {
        public string id { get; set; }
        public string text { get; set; }
    }

    public class CategoryListDTO
    {
        public IList<CategoryDTO> DtoList { get; set; }
        public IList<string> ErrorList { get; set; }
    }
}