using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using System.Linq;


namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterLookupTVILABDensity
    {
        private readonly IViewLookupTVILABDensity _viewLookupTVILABDensity;

        public PresenterLookupTVILABDensity(IViewLookupTVILABDensity viewLookupTVILABDensity)
        {
            _viewLookupTVILABDensity = viewLookupTVILABDensity;
        }

        public void PrintStandardTypeList()
        {
            var PrintStandardTypeBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _viewLookupTVILABDensity.ErrorOccured;
            _viewLookupTVILABDensity.PrintStandardTypeList = PrintStandardTypeBusiness.GetPrintStandardTypeList(_viewLookupTVILABDensity.CurrentUserId);
        }

        public void GetPrintStandardLAB()
        {
            var PrintStandardTypeBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _viewLookupTVILABDensity.ErrorOccured;
            _viewLookupTVILABDensity.PrintStandardLAB = PrintStandardTypeBusiness.GetPrintStandardLAB(_viewLookupTVILABDensity.PrintStandardTypeID, 0);
        }
      
        public void GetDummyPrintStandardLAB()
        {
            var PrintStandardTypeBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _viewLookupTVILABDensity.ErrorOccured;
            _viewLookupTVILABDensity.PrintStandardLAB = PrintStandardTypeBusiness.GetPrintStandardLAB(_viewLookupTVILABDensity.PrintStandardTypeID, _viewLookupTVILABDensity.ProjectTypeID);
        }

        public void GetPrintStandardTVI()
        {
            var PrintStandardTypeBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _viewLookupTVILABDensity.ErrorOccured;
            _viewLookupTVILABDensity.PrintStandardTVI = PrintStandardTypeBusiness.GetPrintStandardTVI(_viewLookupTVILABDensity.PrintStandardTypeID);
        }

        public void GetPrintStandardDensity()
        {
            var PrintStandardTypeBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _viewLookupTVILABDensity.ErrorOccured;
            _viewLookupTVILABDensity.PrintStandardDensity = PrintStandardTypeBusiness.GetPrintStandardDensity(_viewLookupTVILABDensity.PrintStandardTypeID);
        }

        public void SaveLookupTVILABDensity()
        {
            var PrintStandardTypeBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _viewLookupTVILABDensity.ErrorOccured;
            PrintStandardTypeBusiness.InsertLookupTVILABDensity(_viewLookupTVILABDensity.PrintStandardTypeID, _viewLookupTVILABDensity.PrintStandardLAB, _viewLookupTVILABDensity.PrintStandardTVI, _viewLookupTVILABDensity.PrintStandardDensity);
        }
    }
}
