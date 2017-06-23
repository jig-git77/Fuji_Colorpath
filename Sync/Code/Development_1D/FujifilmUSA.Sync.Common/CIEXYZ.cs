using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FujifilmUSA.Sync.Common
{
    public class CIEXYZ
    {
        #region Fields

        private double _x; //X can be compared to Red -- Ranges from 0 to 0.9505
        private double _y; //Y can be compared to Green -- Ranges from 0 to 1.0
        private double _z; //Z can be compared to Blue -- Ranges from 0 to 1.089

        #endregion

        //#region Operators

        //public static bool operator ==(CIEXYZ item1, CIEXYZ item2)
        //{
        //    return item1.X == item2.X && item1.Y == item2.Y && item1.Z == item2.Z;
        //}

        //public static bool operator !=(CIEXYZ item1, CIEXYZ item2)
        //{
        //    return item1.X != item2.X || item1.Y != item2.Y || item1.Z != item2.Z;
        //}

        //#endregion

        #region Accessors

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