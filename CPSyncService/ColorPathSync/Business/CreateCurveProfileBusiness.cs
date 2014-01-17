using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColorPathSync.Business
{
    public class CreateCurveProfileBusiness
    {
        public static Profile CreateCurve(Curve objCurve)
        {
            Profile objPf = new Profile();
            try
            {
                objPf = objCurve.CreateCurve();
                return objPf;
            }
            catch (Exception ex)
            {
                objPf.ErrorMsg = ex.ToString();
                return objPf;
            }
        }
    }
}