/// <summary>
/// Structure to define CIELab.
/// </summary>
public class CIELab
{
    #region Fields

    private double _l;
    private double _a;
    private double _b;

    #endregion

     

    #region Accessors

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