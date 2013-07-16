using FujifilmUSA.Sync.Presenter.ViewInterface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterWebLogActivity
    {
        private readonly IViewWebLogActivity _viewWebLogActivity;

        public PresenterWebLogActivity(IViewWebLogActivity viewWebLogActivity)
        {
            _viewWebLogActivity = viewWebLogActivity;
        }

        public void LogActivity()
        {
            Business.LogActivityManager.WebLogActivity(
                _viewWebLogActivity.ActivityType,
                _viewWebLogActivity.Paramaters,
                _viewWebLogActivity.IpAddress);
        }
    }
}
