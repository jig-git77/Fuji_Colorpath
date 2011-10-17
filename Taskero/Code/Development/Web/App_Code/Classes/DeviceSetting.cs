using System;
using System.Collections.Generic;

using System.Web;
using System.Runtime.Serialization;

namespace MeasurementPlugInSvc
{

     [DataContract]
    public class DeviceSetting
    {

         public enum DensityType
         {
             DIN  = 1,
             DINNB = 2,
             ANSIA = 3,
             ANSIE = 4,
             ANSII = 5,
             ANSIT  = 6
         }


         public enum IlluminantType
         {
             D50  = 1,
             D55  = 2,
             D65  = 3 ,
             D75  = 4,
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


         public IlluminantType Illuminant { get; set; }
         public DensityType DensityStandard { get; set; }
         public ObserverType Observer { get; set; }
         public IlluminationMode Mode { get; set; }

         /* http://iainjmitchell.com/blog/?p=395 */   
         [DataMember(Name = "Illuminant")]
         public string IlluminantString
         {
             get { return Enum.GetName(typeof(IlluminantType), this.Illuminant); }
             set { this.Illuminant = (IlluminantType)Enum.Parse(typeof(IlluminantType), value); }
         }


         [DataMember(Name = "DensityStandard")]
         public string DensityTypeString
         {
             get { return Enum.GetName(typeof(DensityType), this.DensityStandard); }
             set { this.DensityStandard = (DensityType)Enum.Parse(typeof(DensityType), value); }
         }


         [DataMember(Name = "Observer")]
         public string ObserverAngle
         {
             get { return Enum.GetName(typeof(ObserverType), this.Observer); }
             set { this.Observer = (ObserverType)Enum.Parse(typeof(DensityType), value); }
         }


         [DataMember(Name = "Mode")]
         public string IlluminationModeString   
         {
             get { return Enum.GetName(typeof(IlluminationMode), this.Mode); }
             set { this.Mode = (IlluminationMode)Enum.Parse(typeof(IlluminationMode), value); }
         }


         /// <summary>
         /// Constructor: Creates an instance of a DeviceSetting Object
        /// </summary>
         public DeviceSetting()
        {
            this.Illuminant = IlluminantType.D50;
            this.DensityStandard = DensityType.ANSIT;
            this.Observer  = ObserverType.TwoDegree;
            this.Mode  = IlluminationMode.UV;
        }




    }

}