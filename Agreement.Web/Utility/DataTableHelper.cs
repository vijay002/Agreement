using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agreement.Web.Utility
{
    public static class DataTableHelper
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

        //public string MakeDatatableForSearch(string jsonSearchFilter)
        //{
        //    string strWhereCondition = string.Empty;
        //    try
        //    {
        //        List<SearchFilter> searchflt = new JavaScriptSerializer().Deserialize<List<SearchFilter>>(jsonSearchFilter);
        //        foreach (var item in searchflt)
        //        {
        //            if (!string.IsNullOrWhiteSpace(item.Value) && item.Value != "false")
        //            {
        //                //if (item.ColumnName.Contains(',')) // Multiple Table Field on signle Column and make it with OR operation
        //                //{
        //                //    strWhereCondition += " and ( 1=0 ";
        //                //    foreach (var Cname in item.ColumnName.Split(','))
        //                //    {
        //                //        //if
        //                //            strWhereCondition += MakeDatatableSearchWithMultipleCondition(item.FilterType, Cname, item.Value, item.DataType, "or");
        //                //        //else
        //                //        //    strWhereCondition += Enums.MakeDatatableSearchWithMultipleConditionForNumeric(item.FilterType, (item.DataType == "date" ? "CONVERT(date, " + Cname + ")" : Cname), item.Value, "or");
        //                //    }
        //                //    strWhereCondition += " ) ";
        //                //}
        //                //else
        //                //{
        //                strWhereCondition += MakeDatatableSearchWithMultipleCondition(item.FilterType, item.ColumnName, item.Value, item.DataType);

        //                /*if (item.DataType == "string")
        //                    strWhereCondition += Enums.MakeDatatableSearchWithMultipleCondition(item.FilterType, item.ColumnName, item.Value);
        //                else
        //                    strWhereCondition += Enums.MakeDatatableSearchWithMultipleConditionForNumeric(item.FilterType, (item.DataType == "date" ? "CONVERT(date, " + item.ColumnName + ")" : item.ColumnName), item.Value);*/
        //                //}
        //            }
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ex.Message.ToString();
        //    }
        //    return strWhereCondition;
        //}


    }
}
