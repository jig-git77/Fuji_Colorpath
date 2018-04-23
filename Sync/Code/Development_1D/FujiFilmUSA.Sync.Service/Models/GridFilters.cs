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
                if (filter.filters != null)
                {
                    foreach (var item in filter.filters)
                    {
                        //string filterString = item.field + " like " + item.value;
                        filterString += item.field + " " + getDataTableOpertors(item.@operator) + " " + item.value + " AND ";
                    }
                    if (filterString.EndsWith("AND "))
                        filterString = filterString.Remove(filterString.Length - 5, 5);
                    dv.RowFilter = filterString;

                }
                filteredDT = dv.ToTable();

                if (filteredDT.Rows.Count > 0 && sort.dir != null)
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

        public static string getDataTableOpertors(string kendoOperator)
        {
            string operators = "";
            switch (kendoOperator)
            {
                case "eq": operators = "="; break;
                case "neq": operators = "<>"; break;
                case "gte": operators = ">="; break;
                case "gt": operators = ">"; break;
                case "lte": operators = "<="; break;
                case "lt": operators = "<"; break;
                    //case "isnull": operators = "<>"; break;
                    //;case "isnotnull": operators = "<>"; break;
            }
            return operators;
        }


        //public Func<DataRow, bool> FilterColumn(string searchColumn, string searchValue, Type type)
        //{
        //    Func<DataRow, bool> searchFunc = null;
        //    switch (searchColumn)
        //    {
        //        case "EMP_NAME":
        //            searchFunc = x => x.Field < type.GetType() > (searchColumn) == searchValue;
        //            break;
        //        default:
        //            break;
        //    }
        //    return searchFunc;
        //}

        //public Func<POSITION_DETAIL_PS_IPAD_WORK, bool> FilterColumn(string searchColumn, string searchValue)
        //{
        //    Func<POSITION_DETAIL_PS_IPAD_WORK, bool> searchFunc = null;
        //    switch (searchColumn)
        //    {
        //        case "EMP_NAME":
        //            searchFunc = x => x.EMP_NAME == searchValue;
        //            break;
        //        case "EMP_CODE":
        //            searchFunc = x => x.EMP_CODE == searchValue;
        //            break;
        //        case "EMAIL_ID":
        //            searchFunc = x => x.EMAIL_ID == searchValue;
        //            break;
        //        case "HQ_NAME":
        //            searchFunc = x => x.HQ_NAME == searchValue;
        //            break;
        //        case "DESIGNATION":
        //            searchFunc = x => x.DESIGNATION == searchValue;
        //            break;
        //        case "MARKET_TYPE_FULL":
        //            searchFunc = x => x.MARKET_TYPE == (searchValue == "Domestic" ? "D" : "E");
        //            break;
        //        case "COUNTRY":
        //            searchFunc = x => x.COUNTRY == searchValue;
        //            break;
        //        default:
        //            searchFunc = x => x.EMP_NAME == searchValue;
        //            break;
        //    }
        //    return searchFunc;
        //}
    }
}