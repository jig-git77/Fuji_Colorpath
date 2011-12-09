using System;
namespace FujifilmUSA.Sync.Entity
{
    public class MeasurementPatch
    {
        public int MeasurementPatchId { get; set; }
        public int MeasurementId { get; set; }
        public string SampleName { get; set; }
        public short SampleId { get; set; }
        //public Density Density { get; set; }
        //public CieXyz Ciexyz { get; set; }
        //public CieLab Cielab { get; set; }
        public decimal CmykC { get; set; }
        public decimal CmykM { get; set; }
        public decimal CmykY { get; set; }
        public decimal CmykK { get; set; }
    }

    public class CieXyz
    {
        public double X { get; set; }
        public double Y { get; set; }
        public double Z { get; set; }

        public CieXyz(double x, double y, double z)
        {
            this.X = x;
            this.Y = y;
            this.Z = z;
        }

        public override bool Equals(object obj)
        {
            if ((obj == null) | (this.GetType() != obj.GetType()))
            {
                return false;
            }
            return (this == (CieXyz)obj);
        }

        public override int GetHashCode()
        {
            return System.Convert.ToInt32((System.Math.Pow((System.Math.Pow(X.GetHashCode(), Y.GetHashCode())), Z.GetHashCode())));
        }
    }

    public class Density
    {
        public double Red { get; set; }
        public double Green { get; set; }
        public double Blue { get; set; }
        public double Visual { get; set; }

        public Density(double r, double g, double b, double v)
        {
            this.Red = r;
            this.Green = g;
            this.Blue = b;
            this.Visual = v;
        }
    }
    
    public class CieLab
    {
        public double L { get; set; }
        public double A { get; set; }
        public double B { get; set; }
    
        public CieLab(double l, double a, double b)
        {
            this.L = l;
            this.A = a;
            this.B = b;
        }
    }
}
