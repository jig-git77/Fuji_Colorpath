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
            DataView dv = new DataView(DT);
            try
            {
                
                foreach (var item in filter.filters)
                {
                    //string filterString = item.field + " like " + item.value;
                    string filterString = item.field + " " + getDataTableOpertors(item.@operator) + " " + item.value;
                    dv.RowFilter = filterString;
                }


                //EnumerableRowCollection dtEnum = DT.AsEnumerable();
                //EnumerableRowCollection<DataRow> dtEnum1 = from customer in DT.AsEnumerable()
                //                                           select customer;
                //EnumerableRowCollection<DataRow> filteredDT = null;
                //foreach (var item in filter.filters)
                //{
                //    string str = "";
                //    Func<DataRow, bool> searchFunc = null;
                //    string fieldType = DT.Columns[item.field].DataType.ToString();

                //    searchFunc = x => x.Field < typeof(fieldType) > (item.field) == item.value;
                //    //searchFunc = FilterColumn(item.field, item.value,fieldType);
                //    filteredDT = dtEnum1.Where(searchFunc);
                //}

            }
            catch (Exception ex)
            {
                throw ex;
            }

            return dv.ToTable();
        }

        public static string getDataTableOpertors(string kendoOperator)
        {
            string operators = "";
            switch (kendoOperator)
            {
                case "eq": operators = "="; break;
                case "neq": operators = "<>"; break;
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