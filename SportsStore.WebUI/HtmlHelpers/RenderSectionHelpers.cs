using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.WebPages;
using SportsStore.WebUI.Models;

namespace SportsStore.WebUI.HtmlHelpers
{
    public static class RenderSectionHelpers
    {
        private static readonly object _o = new object(); 

        /// <summary>
        /// HtmlHelper Extension method 
        /// Used to render sections and give option to use default.
        /// Calls delegate - setup by client call to generate the default content
        /// </summary>
        /// <param name="webPage"></param>
        /// <param name="name"></param>
        /// <param name="defaultContents"></param>
        /// <returns></returns>
        public static HelperResult RenderSectionWithDefault(this WebPageBase webPage,
                                              string name,
                                              Func<object, HelperResult> defaultContents)
        {
            if(webPage.IsSectionDefined(name))
            {
                return webPage.RenderSection(name);
            }
            return defaultContents(_o);
        }

    }

}