using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Runtime.Serialization;
/// <summary>
/// Summary description for ChartSetting
/// </summary>
namespace MeasurementPlugInSvc
{
     [DataContract]
    public class ChartSetting
    {
       
	    public ChartSetting()
	    {
		//
		// TODO: Add constructor logic here
		//
	    }


        /// <summary>
        /// For Eye-One Isis
        /// </summary>
        private Decimal _PatchSizeX = 0;
        [DataMember(EmitDefaultValue = false, Order = 1)]     /* EmitDefaultValue = false --> stop any unset properties being included in the serialised JSON from WCF as null  */
        public Decimal PatchSizeX
        {
            get { return _PatchSizeX; }
            set { _PatchSizeX = value; }
        }


        /// <summary>
        /// For Eye-One Isis
        /// </summary>
        private Decimal _PatchSizeY = 0;
        [DataMember(EmitDefaultValue = false, Order = 2)]
        public Decimal PatchSizeY
        {
            get { return _PatchSizeY; }
            set { _PatchSizeY = value; }
        }

        /// <summary>
        /// For Eye-One Isis
        /// </summary>
        private Decimal _BarSizeX = 0;
        [DataMember(EmitDefaultValue = false, Order = 3)]
        public Decimal BarSizeX
        {
            get { return _BarSizeX; }
            set { _BarSizeX = value; }
        }


        /// <summary>
        /// For Eye-One Isis
        /// </summary>
        private Decimal _BarSizeY = 0;
        [DataMember(EmitDefaultValue = false, Order = 4)]
        public Decimal BarSizeY
        {
            get { return _BarSizeY; }
            set { _BarSizeY = value; }
        }


        /// <summary>
        /// For Eye-One Isis
        /// </summary>
        private Decimal _DistToFirstLineY = 0;
        [DataMember(EmitDefaultValue = false, Order = 5)]
        public Decimal DistToFirstLineY
        {
            get { return _DistToFirstLineY; }
            set { _DistToFirstLineY = value; }
        }

        /// <summary>
        /// For Eye-One Isis
        /// </summary>
        private Decimal _MarkerDistance = 0;
        [DataMember(EmitDefaultValue = false, Order = 6)]
        public Decimal MarkerDistance
        {
            get { return _MarkerDistance; }
            set { _MarkerDistance = value; }
        }


        /// <summary>
        /// For Eye-One Isis
        /// </summary>
        private Decimal _DistToBar = 0;
        [DataMember(EmitDefaultValue = false, Order = 7)]
        public Decimal DistToBar
        {
            get { return _DistToBar; }
            set { _DistToBar = value; }
        }



        /// <summary>
        /// For Eye-One Isis
        /// </summary>
        private Decimal _DistToFirstField = 0;
        [DataMember(EmitDefaultValue = false, Order = 8)]
        public Decimal DistToFirstField
        {
            get { return _DistToFirstField; }
            set { _DistToFirstField = value; }
        }


        /// <summary>
        /// For Eye-One Isis
        /// </summary>
        private Decimal _ChartSizeX = 0;
        [DataMember(EmitDefaultValue = false, Order = 9)]
        public Decimal ChartSizeX
        {
            get { return _ChartSizeX; }
            set { _ChartSizeX = value; }
        }


        /// <summary>
        /// For Eye-One Isis
        /// </summary>
        private Decimal _ChartSizeY = 0;
        [DataMember(EmitDefaultValue = false, Order = 10)]
        public Decimal ChartSizeY
        {
            get { return _ChartSizeY; }
            set { _ChartSizeY = value; }
        }



    }
}