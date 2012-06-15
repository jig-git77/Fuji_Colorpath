using System;
using System.Collections.Generic;
using System.Web;
using System.Runtime.Serialization;
using System.Data;
using System.Data.SqlClient; 

namespace MeasurementPlugInSvc
{

    [DataContract]
    public class ReferencePatch
    {


        private string _SampleName;
        [DataMember(Order = 1)]
        public string SampleName
        {
            get { return _SampleName; }
            set { _SampleName = value; }
        }

        private decimal _CMYK_C = 0;
        [DataMember(Order = 2)]
        public decimal CMYK_C
        {
            get { return _CMYK_C; }
            set { _CMYK_C = value; }
        }


        private decimal _CMYK_M = 0;
        [DataMember(Order = 3)]
        public decimal CMYK_M
        {
            get { return _CMYK_M; }
            set { _CMYK_M = value; }
        }


        private decimal _CMYK_Y = 0;
        [DataMember(Order = 4)]
        public decimal CMYK_Y
        {
            get { return _CMYK_Y; }
            set { _CMYK_Y = value; }
        }

        private decimal _CMYK_K = 0;
        [DataMember(Order = 5)]
        public decimal CMYK_K
        {
            get { return _CMYK_K; }
            set { _CMYK_K = value; }
        }


        private CIELab _TargetLAB;
        [DataMember(Order = 6)]
        public CIELab TargetLAB
        {
            get { return _TargetLAB; }
            set { _TargetLAB = value; }
        }


        private int _PatchTypeID = 0;
        [DataMember(Order = 7)]
        public int PatchTypeID
        {
            get { return _PatchTypeID; }
            set { _PatchTypeID = value; }
        }


        private string _HTMLColor = null;
        [DataMember(Order = 8)]
        public string HTMLColor
        {
            get { return _HTMLColor; }
            set { _HTMLColor = value; }
        }

        //do not know wheter we will use it or not
        private int _SampleID;
        [DataMember(Order = 9)]
        public int SampleID
        {
            get { return _SampleID; }
            set { _SampleID = value; }
        }            

    }

}