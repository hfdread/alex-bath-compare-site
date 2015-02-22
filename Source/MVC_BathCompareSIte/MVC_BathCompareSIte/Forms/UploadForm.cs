using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC_BathCompareSIte.Forms
{
    public class UploadForm
    {
        public string filePath { get; set; }
        public bool isWithHeaders { get; set; }
    }

    public class UploadFileForm
    {
        [Required]
        public HttpPostedFileBase uploadedFile { get; set; }
    }
}