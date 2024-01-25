using System.Runtime.Serialization;

namespace TaskeroWeb.Models.Classes
{
    /// <summary>
    /// Class to define CIEXYZ.
    /// </summary>
    [DataContract]
    public class CIEXYZ
    {

        #region Fields

        private double _x; //X can be compared to Red -- Ranges from 0 to 0.9505
        private double _y; //Y can be compared to Green -- Ranges from 0 to 1.0
        private double _z; //Z can be compared to Blue -- Ranges from 0 to 1.089

        #endregion


        #region Accessors
        [DataMember(Order = 1)]
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
            }
        }
        [DataMember(Order = 2)]
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
            }
        }

        [DataMember(Order = 3)]
        public double Z
        {
            get
            {
                return _z;
            }
            set
            {
                _z = value;
            }
        }

        #endregion

        /// <summary>
        /// Creates an instance of a CIEXYZ structure.
        /// </summary>
        public CIEXYZ(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        #region Methods

        public override bool Equals(object obj)
        {
            if ((obj == null) | (this.GetType() != obj.GetType()))
            {
                return false;
            }
            return (this == (CIEXYZ)obj);
        }

        public override int GetHashCode()
        {
            return System.Convert.ToInt32((System.Math.Pow((System.Math.Pow(X.GetHashCode(), Y.GetHashCode())), Z.GetHashCode())));
        }

        #endregion

    }
}