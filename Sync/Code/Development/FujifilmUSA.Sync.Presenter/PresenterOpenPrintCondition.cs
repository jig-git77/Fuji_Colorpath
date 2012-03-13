using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterOpenPrintCondition
    {
        private readonly IViewOpenPrintCondition _viewOpenPrintCondition;

        public PresenterOpenPrintCondition(IViewOpenPrintCondition viewOpenPrintCondition)
        {
            _viewOpenPrintCondition = viewOpenPrintCondition;
        }

        public void GetProjectDetail()
        {
            var task = BusinessFactory<IProjectBusiness>.CreateObject();
            _viewOpenPrintCondition.Project = task.GetProjectDetail(_viewOpenPrintCondition.ProjectId);
        }
    }
}
