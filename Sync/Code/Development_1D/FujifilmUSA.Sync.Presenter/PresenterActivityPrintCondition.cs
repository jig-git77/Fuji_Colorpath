using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using System.Linq;


namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterActivityPrintCondition
    {
        private readonly IViewActivityPrintCondition _viewActivityPrintCondition;

        public PresenterActivityPrintCondition(IViewActivityPrintCondition viewActivityPrintCondition)
        {
            _viewActivityPrintCondition = viewActivityPrintCondition;
        }       

        public void GetPrintActivities()
        {
            var ActivityBusiness = BusinessFactory<IMiscDataBusiness>.CreateObject();
            ActivityBusiness.ErrorOccured += _viewActivityPrintCondition.ErrorOccured;
            _viewActivityPrintCondition.ActivityPrintList = ActivityBusiness.GetPrintActivities(_viewActivityPrintCondition.RegionID);
        }
        
    }
}
