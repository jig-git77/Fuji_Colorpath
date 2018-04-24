using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace FujiFilmUSA.Sync.Service.Models
{
    public class GridFilters
    {
        public static DataTable GetFilteredResult(DataTable DT, SortDescription sort, FilterContainer filter)
        {
            DataTable dtFilter = new DataTable();
            DataTable filteredDT = new DataTable();
            DataView dv = new DataView(DT);
            try
            {
                string filterString = "";
                if (filter != null && filter.filters != null)
                {
                    foreach (var item in filter.filters)
                    {
                        //string filterString = item.field + " like " + item.value;
                        //filterString += item.field + " " + getDataTableOpertors(item.@operator) + " " + item.value + " AND ";
                        filterString += getDataTableOpertors(item.field, item.value, item.@operator, item.dtype) + " AND ";
                    }
                    if (filterString.EndsWith("AND "))
                        filterString = filterString.Remove(filterString.Length - 5, 5);
                    dv.RowFilter = filterString;

                }
                filteredDT = dv.ToTable();

                if (filteredDT.Rows.Count > 0 && sort != null && sort.dir != null)
                {
                    string sortingExpression = sort.field + " " + sort.dir;
                    //filteredDT.DefaultView.Sort = sortingExpression;
                    DataView view = filteredDT.DefaultView;
                    view.Sort = sortingExpression;
                    DataTable dt = view.ToTable();
                    filteredDT = dt;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

            return filteredDT;
        }

        public static string getDataTableOpertors(string field, string value, string kendoOperator, string dataType)
        {
            string expression = field + " ";
            switch (kendoOperator)
            {
                case "eq":
                    if (dataType == "number")
                        expression += "= " + value;
                    else
                        expression += "= '" + value + "'";
                    break;
                case "neq":
                    if (dataType == "number")
                        expression += "<> " + value;
                    else
                        expression += "<> '" + value + "'";
                    break;
                case "gte": expression += ">= " + value; break;
                case "gt": expression += "> " + value; break;
                case "lte": expression += "<= " + value; break;
                case "lt": expression += "< " + value; break;
                case "contains": expression += "LIKE '%" + value + "%'"; break;
                case "doesnotcontain": expression += "NOT LIKE '%" + value + "%'"; break;
                case "startswith": expression += "LIKE '" + value + "*'"; break;
                case "endswith": expression += "LIKE '*" + value + "'"; break;
            }
            return expression;
        }
    }
}