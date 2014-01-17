using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ColorPathSync.Business
{
    public class CreateISOCurveProfileBusiness
    {
        public static Profile CreateISOCurve(Curve objCurve)
        {

            Profile objPf = new Profile();
            try
            {
                objPf = objCurve.CreateISOCurve();
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