using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using System.Linq;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterPrintStandardType
    {
        private readonly IViewPrintStandardType _viewPrintStandardType;

        public PresenterPrintStandardType(IViewPrintStandardType viewPrintStandardType)
        {
            _viewPrintStandardType = viewPrintStandardType;
        }


        public void SourceProfileList()
        {
            var PrintStandardTypeBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _viewPrintStandardType.ErrorOccured;
            _viewPrintStandardType.SourceProfileList = PrintStandardTypeBusiness.GetSourceProfileList(_viewPrintStandardType.CurrentUserId);
        }

        public void GetPrintStandardTypes()
        {
            var PrintStandardTypeBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _viewPrintStandardType.ErrorOccured;
            _viewPrintStandardType.PrintStandardTypes = PrintStandardTypeBusiness.GetPrintStandardType(0, _viewPrintStandardType.CurrentUserId);
        }

        public void GetPrintStandardType()
        {
            var PrintStandardTypeBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _viewPrintStandardType.ErrorOccured;
            _viewPrintStandardType.PrintStandardType = PrintStandardTypeBusiness.GetPrintStandardType(_viewPrintStandardType.PrintStandardType.PrintStandardTypeID, _viewPrintStandardType.CurrentUserId).FirstOrDefault();
        }

        public void InsertPrintStandardType()
        {
            var PrintStandardTypeBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _viewPrintStandardType.ErrorOccured;
            PrintStandardTypeBusiness.InsertPrintStandardType(_viewPrintStandardType.PrintStandardType);
        }

        public void UpdatePrintStandardType()
        {
            var PrintStandardTypeBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _viewPrintStandardType.ErrorOccured;
            PrintStandardTypeBusiness.UpdatePrintStandardType(_viewPrintStandardType.PrintStandardType);
        }

        public void DeletePrintStandardType()
        {
            var PrintStandardTypeBusiness = BusinessFactory<ISourceProfileBusiness>.CreateObject();
            PrintStandardTypeBusiness.ErrorOccured += _viewPrintStandardType.ErrorOccured;
            PrintStandardTypeBusiness.DeletePrintStandardType(_viewPrintStandardType.PrintStandardType.PrintStandardTypeID);
        }       

    }
}
