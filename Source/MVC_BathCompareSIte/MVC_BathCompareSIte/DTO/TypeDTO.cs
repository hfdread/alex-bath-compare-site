using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC_BathCompareSIte.DTO
{
    public class TypeDTO
    {
        public Int32 Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public Int32 Parent { get; set; }

        public IList<string> ErrorList { get; set; }
    }
}