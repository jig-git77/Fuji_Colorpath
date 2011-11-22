using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterDLP
    {
        private readonly IViewAddProject _ViewAddProject;
        public PresenterDLP(IViewAddProject ViewAddProject)
        {
            this._ViewAddProject = ViewAddProject;
        }

        public void GetDLPList()
        {
            IDLPBusiness task = (IDLPBusiness)new BusinessFactory<IDLPBusiness>().CreateObject();
            _ViewAddProject.GetDLPList = task.GetDLPList();

        }
    }
}
