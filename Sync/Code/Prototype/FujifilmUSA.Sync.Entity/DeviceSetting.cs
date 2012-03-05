using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Entity
{
    public class DeviceSetting
    {
        public enum DensityType
        {
            DIN = 1,
            DINNB = 2,
            ANSIA = 3,
            ANSIE = 4,
            ANSII = 5,
            ANSIT = 6
        }


        public enum IlluminantType
        {
            D50 = 1,
            D55 = 2,
            D65 = 3,
            D75 = 4,
        }
        public enum ObserverType
        {
            TwoDegree = 1,
            TenDegree = 2
        }

        public enum IlluminationMode
        {

            UV = 1,           //UV Included
            UVCut = 2         //No UV i.e. UVCut
        }


        private IlluminantType _illuminant;
        private DensityType _densityStandard;
        private ObserverType _observer;
        private IlluminationMode _mode;

        public string Illuminant
        {
            get { return Enum.GetName(typeof(IlluminantType), this._illuminant); }
            set { this._illuminant = (IlluminantType)Enum.Parse(typeof(IlluminantType), value); }
        }
        public string DensityStandard
        {
            get { return Enum.GetName(typeof(DensityType), this._densityStandard); }
            set { this._densityStandard = (DensityType)Enum.Parse(typeof(DensityType), value); }
        }
        public string Observer
        {
            get { return Enum.GetName(typeof(ObserverType), this._observer); }
            set { this._observer = (ObserverType)Enum.Parse(typeof(DensityType), value); }
        }
        public string Mode
        {
            get { return Enum.GetName(typeof(IlluminationMode), this._mode); }
            set { this._mode = (IlluminationMode)Enum.Parse(typeof(IlluminationMode), value); }
        }


        /// <summary>
        /// Constructor: Creates an instance of a DeviceSetting Object
        /// </summary>
        public DeviceSetting()
        {
            this._illuminant = IlluminantType.D50;
            this._densityStandard = DensityType.ANSIT;
            this._observer = ObserverType.TwoDegree;
            this._mode = IlluminationMode.UV;
        }

    }
}
