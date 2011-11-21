using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Presenter.ViewInterface;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Implementation;

namespace FujifilmUSA.Sync.Presenter
{
    public class PresenterCompany
    {
        private readonly IViewCompany _ViewCompany;

        public PresenterCompany(IViewCompany ViewCompany)
        {
            this._ViewCompany = ViewCompany;
        }
        public void GetCompanyList()
        {
            ICompanyBusiness task = (ICompanyBusiness)new BusinessFactory<TaskeroUserBusinessImpl>();
            _ViewCompany.GetCompanyList = task.GetCompanyList();
        }
    }
}
