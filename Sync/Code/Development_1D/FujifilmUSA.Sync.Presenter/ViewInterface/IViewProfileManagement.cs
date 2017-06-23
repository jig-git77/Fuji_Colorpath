using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Presenter.ViewInterface
{
    public interface IViewProfileManagement : IViewBase
    {
        int LoginId { get; set; }
        string Password { get; set; }
        int SecurityQuestionId { get; set; }
        string SecurityAnswer { get; set; }
        Dictionary<int, string> SecurityQuestions { get; set; }
        string LanguageCultureCode { get; set; }
        List<Entity.Language> LanguageList { get; set; }
        int MeasurementDeviceID { get; set; }
        short UVMode { get; set; }
        short DensityType { get; set; }
        List<MeasurementDevice> MeasurementDevices { get; set; }
    }
}
