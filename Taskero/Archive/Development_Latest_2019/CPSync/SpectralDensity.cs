/// <summary>
/// Structure to define SpectralDensity
/// </summary>
public class SpectralDensity
{
    #region Fields

    private double _Red; //Measured Density using Red Filter (i.e. Cyan)
    private double _Green; //Measured Density using Green Filter (i.e. Magenta)
    private double _Blue; //Measured Density using Blue Filter (i.e. Yellow)
    private double _Visual; //Measured Density Visual

    #endregion

    #region Accessors

    public double Red
    {
        get
        {
            return _Red;
        }
        set
        {
            _Red = value;
        }
    }

    public double Green
    {
        get
        {
            return _Green;
        }
        set
        {
            _Green = value;
        }
    }

    public double Blue
    {
        get
        {
            return _Blue;
        }
        set
        {
            _Blue = value;
        }
    }

    public double Visual
    {
        get
        {
            return _Visual;
        }
        set
        {
            _Visual = value;
        }
    }
    #endregion

    /// <summary>
    /// Creates an instance of a Spectral Density structure.
    /// </summary>
    /// <param name="r">Measured Density using Red Filter (Cyan uses Red Filter ).</param>
    /// <param name="g">Green value</param>
    /// <param name="b">Blue value .</param>
    public SpectralDensity(double r, double g, double b, double v)
       
    {
        this.Red = r;
        this.Green = g;
        this.Blue = b;
        this.Visual = v;
    }




}
