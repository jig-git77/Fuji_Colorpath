using ColorPathSync.Business;
using ColorPathSync.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ColorPathSync
{
    public class CPSync : ICPSync
    {
        public Profile CreateCurve(Curve objCurve)
        {
            Profile objPf = new Profile();

            try {

               objPf= objCurve.CreateCurve();
               return objPf;

            } 
            catch (Exception ex) {

                objPf.ErrorMsg = ex.ToString();
                return objPf;
            }
        }


        public Profile CreateISOCurve(Curve objCurve)
        {

            Profile objPf = new Profile();
            try
            {

                //objPf = objCurve.CreateISOCurve();
                return objPf;

            }
            catch (Exception ex)
            {
                objPf.ErrorMsg = ex.ToString();
                return objPf;
            }
        }

        public Profile GetG7NPDC(Curve objCurve)
        {

            Profile objPf = new Profile();
            try
            {

                objPf = objCurve.GetG7NPDC();
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
