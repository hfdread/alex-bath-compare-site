using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using System.Web.Mvc;
using Kendo.Mvc.UI;

namespace MVC_BathCompareSIte.Utils
{
    public static class CustomHtmlHelpers
    {
        /// <summary>
        /// Renders an image html element.
        /// </summary>
        /// <param name="helper"></param>
        /// <param name="source">Path of the image</param>
        /// <param name="ctlId">control Id</param>
        /// <param name="_class">css class</param>
        /// <param name="altText">alternate text for the html element</param>
        /// <returns></returns>
        public static MvcHtmlString RenderImage(this HtmlHelper helper, string source, string ctlId, string _class,
            string altText)
        {
            var retImage = string.Format(@"<img src=""{0}"" id=""{1}"" />", source, ctlId, _class, altText);

            return new MvcHtmlString(retImage);
        }
    }
}