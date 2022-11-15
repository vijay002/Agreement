using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agreement.Web.Utility
{
    public static class TableHelper
    {
        public static string MakeDatatableSearchCondition(this string[] source, string search)
        {
            string strLikeFormat = string.Empty;
            if (source.Length > 0 && !string.IsNullOrWhiteSpace(search))
            {
                StringBuilder sb = new StringBuilder();

                foreach (var item in search.Trim().Split(' '))
                {
                    int count = 0;

                    sb.Append("and ( ");
                    foreach (var column in source)
                    {
                        if (source.Length - 1 == count)
                        {
                            sb.Append(" " + column + " like '%" + item + "%' ");
                        }
                        else
                        {
                            sb.Append(" " + column + " like '%" + item + "%' or ");
                        }
                        count++;
                    }
                    sb.Append(") ");

                }
                strLikeFormat = sb.ToString();
            }
            return strLikeFormat;
        }

        [NonAction]
        public static string HTMLActionString(int rowId, string buttonName, string buttonTooltip = "", string className = "", string onClickMethod = "", string onHrefURL = "", bool isDisable = false, string inputStyle = "")
        {
            string WebsiteURL = "https://test.com";
            string htmlControl = "";
            string inputID = buttonName + "" + rowId.ToString();
            string onClickEvent = !string.IsNullOrEmpty(onClickMethod) ? onClickMethod : "";
            string onHref = !string.IsNullOrEmpty(onHrefURL) ? "href=\"" + WebsiteURL + onHrefURL + "\"" : "";

            if (isDisable)
            {
                htmlControl = "<i id=\"" + inputID.ToString() + "\" name=\"" + inputID + "\"    Title=\"" + buttonTooltip + "\" class=\"disabledicon " + className + "\" style=\" " + inputStyle + " \" ></i>";
            }
            else
            {
                htmlControl += "<i id=\"" + inputID.ToString() + "\" name=\"" + inputID + "\"  Title=\"" + buttonTooltip + "\" class=\"iconspace " + className + "\"  onclick=\"" + onClickEvent + "\" style=\" " + inputStyle + " \" ></i>";
            }

            if (!string.IsNullOrEmpty(onHref))
            {
                htmlControl = "<a " + onHref + " >" + htmlControl + "</a>";
            }

            return htmlControl;
        }
    }
}
