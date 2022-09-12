using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterReferenceChartPreset
    {
        private readonly IViewReferenceChartPreset _ViewReferenceChartType;

        /// <summary>
        /// PresenterReferenceChartType
        /// </summary>
        public PresenterReferenceChartPreset(IViewReferenceChartPreset ViewReferenceChartType)
        {
            _ViewReferenceChartType = ViewReferenceChartType;
        }

        /// <summary>
        /// GetReferenceChartType
        /// </summary>
        public void GetReferenceChartType()
        {
            var PrintStandardTypeBusiness = BusinessFactory<IReferenceChartPresetBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _ViewReferenceChartType.ErrorOccured;
            _ViewReferenceChartType.lstReferenceChartTypeID = PrintStandardTypeBusiness.GetReferenceChartType();
        }

        /// <summary>
        /// GetMetricTargetPreset
        /// </summary>
        /// <param name="ReferenceChartTypeID"></param>
        public void GetMetricTargetPreset()
        {
            var PrintStandardTypeBusiness = BusinessFactory<IReferenceChartPresetBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _ViewReferenceChartType.ErrorOccured;
            _ViewReferenceChartType.lstMetricTargetPresetID = PrintStandardTypeBusiness.GetMetricTargetPresetID(_ViewReferenceChartType.ReferenceChartTypeID);
        }

        /// <summary>
        /// GetReferenceChartPreset
        /// </summary>
        /// <param name="ReferenceChartTypeID"></param>
        public void GetReferenceChartPreset()
        {
            var PrintStandardTypeBusiness = BusinessFactory<IReferenceChartPresetBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _ViewReferenceChartType.ErrorOccured;
            _ViewReferenceChartType.lstReferenceChartPresetId = PrintStandardTypeBusiness.GetReferenceChartPreset(_ViewReferenceChartType.ReferenceChartTypeID);
        }

        /// <summary>
        /// InsertReferenceChartPreset
        /// </summary>
        /// <param name="ReferenceChartTypeID"></param>
        public int InsertReferenceChartPreset()
        {
            int intReturn = 0;
            var PrintStandardTypeBusiness = BusinessFactory<IReferenceChartPresetBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _ViewReferenceChartType.ErrorOccured;
            intReturn = PrintStandardTypeBusiness.InsertReferenceChartPreset(_ViewReferenceChartType.ReferenceChartTypeID, _ViewReferenceChartType.lstReferenceChartPreset);
            return intReturn;
        }
    }
}
