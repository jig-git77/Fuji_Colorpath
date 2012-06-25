using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Linq;
using System.Collections.ObjectModel;
using ColorOrganizerDemoLib.Sync4DService;
using ColorOrganizerDemoLib.Models;

namespace ColorOrganizerDemoLib.ViewModels
{
    public class ChartPrintViewModel : BaseVM
    {
        public ChartPrintViewModel()
        {
            NextButtonCommand.Executed += delegate
                                              {
                                                  InsertProjectInformationAndRedirect();
                                              };
            PreviousButtonCommand.Executed += (sender, e) => PassToParent(EControl.StartICCProfile);
            Media = new ObservableCollection<string>()
                        {
                            "Media 1",
                            "Media 2",
                            "Media 3",
                            "Media 4",
                            "Media 5",
                        };
            Colorant = new ObservableCollection<string>()
                        {
                            "Colorant 1",
                            "Colorant 2",
                            "Colorant 3",
                            "Colorant 4",
                            "Colorant 5",
                        };
            RipSetting = new ObservableCollection<string>()
                        {
                            "RipSetting 1",
                            "RipSetting 2",
                            "RipSetting 3",
                            "RipSetting 4",
                            "RipSetting 5",
                        };
            MeasurementDevices = new ObservableCollection<EMeasurementDevice>();
            FilteredChartList = new ObservableCollection<ReferenceChart>();
            GetChartList();
        }

        public string SelectedMedia { get; set; }
        public string SelectedColorant { get; set; }
        public string SelectedRipSetting { get; set; }
        private EMeasurementDevice _selectedMeasurementDevice;
        public ReferenceChart SelectedChart { get; set; }
        public EMeasurementDevice SelectedMeasurementDevice
        {
            get
            {
                return _selectedMeasurementDevice;
            }
            set
            {
                _selectedMeasurementDevice = value;
                FilteredChartList = new ObservableCollection<ReferenceChart>();
                ChartList.Where(x => x.MeasurementDevice == value).ToList().ForEach(x => FilteredChartList.Add(x));
                RaisePropertyChanged("SelectedMeasurementDevice");
            }
        }



        public ObservableCollection<string> Media { get; set; }
        public ObservableCollection<string> Colorant { get; set; }
        public ObservableCollection<string> RipSetting { get; set; }
        public ObservableCollection<ReferenceChart> ChartList { get; set; }

        private ObservableCollection<EMeasurementDevice> _measurementDevices;
        public ObservableCollection<EMeasurementDevice> MeasurementDevices
        {
            get { return _measurementDevices; }
            set
            {
                _measurementDevices = value; RaisePropertyChanged("MeasurementDevices");
            }
        }

        private ObservableCollection<ReferenceChart> _filteredChartList;
        public ObservableCollection<ReferenceChart> FilteredChartList
        {
            get { return _filteredChartList; }
            set
            {
                _filteredChartList = value;
                RaisePropertyChanged("FilteredChartList");
            }
        }

        private void InsertProjectInformationAndRedirect()
        {
            var client = new Sync4DServiceContractClient("BasicHttpBinding_ISync4DServiceContract");
            client.StoreTempProjectInformationCompleted += delegate(object sender, StoreTempProjectInformationCompletedEventArgs e)
            {
                if (e.Error == null)
                {
                    if (e.Result != null)
                    {
                        if (this.Parent.StartIccProfile.SelectedIccProfile.ProjectId > 0)
                            Common.EditProjectInfoId = e.Result.ProjectInfoId;
                        else
                            Common.AddProjectInfoId = e.Result.ProjectInfoId;

                        this.Parent.WebHost.RedirectUrl =
                            String.Format(
                                "{0}/COProcessRequest.ashx?action={1}&uid={2}&pwd={3}&projinfoid={4}&deviceid={5}",
                                Common.FujiWebSite,
                                ((this.Parent.StartIccProfile.SelectedIccProfile.ProjectId > 0) ? "edit" : "add"),
                                Common.UserName,
                                Common.GetEncryptPassword(),
                                (this.Parent.StartIccProfile.SelectedIccProfile.ProjectId > 0) ? Common.EditProjectInfoId : Common.AddProjectInfoId,
                                this.Parent.SelectDevice.SelectedDevice.DeviceId);
                        PassToParent(EControl.WebHost);
                    }
                }
                else
                {
                    //e.Error
                }
            };

            var domainUrl = string.Empty;
            if (System.Windows.Browser.HtmlPage.Document != null)
            {
                var urlParts = System.Windows.Browser.HtmlPage.Document.DocumentUri.AbsoluteUri.Split('/');
                domainUrl = string.Join("/", urlParts, 0, urlParts.Length - 1);
            }

            if (this.Parent.StartIccProfile.SelectedIccProfile.ProjectId > 0)
            {
                client.StoreTempProjectInformationAsync(new StoreTempProjectInformationRequest()
                                                            {
                                                                SecurityToken = Common.SecurityToken,
                                                                UserName = Common.UserName,
                                                                UserPassword = Common.GetEncryptPassword(),
                                                                ProjectInfoId = Common.EditProjectInfoId,
                                                                ChartId = SelectedChart.ChartId,
                                                                MeasurementDeviceTypeId = Convert.ToInt32(SelectedMeasurementDevice),
                                                                ProjectId = this.Parent.StartIccProfile.SelectedIccProfile.ProjectId,
                                                                SilverlightPostUrl = domainUrl
                                                            });
            }
            else
            {
                client.StoreTempProjectInformationAsync(new StoreTempProjectInformationRequest()
                                                            {
                                                                SecurityToken = Common.SecurityToken,
                                                                UserName = Common.UserName,
                                                                UserPassword = Common.GetEncryptPassword(),
                                                                ProjectInfoId = Common.AddProjectInfoId,
                                                                Colorant = SelectedColorant,
                                                                Media = SelectedMedia,
                                                                RipSettings = SelectedRipSetting,
                                                                MeasurementDeviceTypeId = Convert.ToInt32(SelectedMeasurementDevice),
                                                                ChartId = SelectedChart.ChartId,
                                                                ProjectName = Parent.StartIccProfile.SelectedIccProfile.IccProfileName,
                                                                SilverlightPostUrl = domainUrl
                                                            });
            }
        }

        private void GetChartList()
        {
            ChartList = new ObservableCollection<ReferenceChart>();
            var client = new Sync4DServiceContractClient("BasicHttpBinding_ISync4DServiceContract");
            client.GetReferenceChartListCompleted += delegate(object sender, GetReferenceChartListCompletedEventArgs e)
            {
                if (e.Error == null)
                {
                    if (e.Result != null)
                    {
                        if (e.Result.ReferenceChartList.Count > 0)
                        {
                            foreach (var chart in e.Result.ReferenceChartList)
                            {
                                ChartList.Add(chart);
                            }

                            MeasurementDevices = new ObservableCollection<EMeasurementDevice>();
                            ChartList.Select(x => x.MeasurementDevice).Distinct().ToList().ForEach(x => MeasurementDevices.Add(x));
                        }
                        else
                        {
                            ChartList.Add(new ReferenceChart() { ChartId = 0, ChartName = "No chart found" });
                        }
                    }
                }
                else
                {
                    //e.Error
                }
            };
            client.GetReferenceChartListAsync(new GetReferenceChartListRequest()
            {
                SecurityToken = Common.SecurityToken,
                UserName = Common.UserName,
                UserPassword = Common.GetEncryptPassword()
            });
        }
    }
}
