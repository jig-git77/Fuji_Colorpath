using System.Runtime.Serialization;

namespace TaskeroWeb.Models.Classes
{
    /// <summary>
    /// Class to define CIELab.
    /// </summary>
    [DataContract]
    public class CIELab
    {
        #region Fields

        private double _l;
        private double _a;
        private double _b;

        #endregion



        #region Accessors
        [DataMember(Order = 1)]
        public double L
        {
            get
            {
                return _l;
            }
            set
            {
                _l = value;
            }
        }
        [DataMember(Order = 2)]
        public double A
        {
            get
            {
                return _a;
            }
            set
            {
                _a = value;
            }
        }
        [DataMember(Order = 3)]
        public double B
        {
            get
            {
                return _b;
            }
            set
            {
                _b = value;
            }
        }

        #endregion

        /// <summary>
        /// Creates an instance of a CIELAB structure.
        /// </summary>
        public CIELab(double l, double a, double b)
        {
            this.L = l;
            this.A = a;
            this.B = b;
        }




    }
}