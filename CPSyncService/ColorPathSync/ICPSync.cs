using ColorPathSync.MessageContracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace ColorPathSync
{

    [ServiceContract]
    public interface ICPSync
    {

        [OperationContract]
        Profile CreateCurve(Curve objSync);

        [OperationContract]
        Profile CreateISOCurve(Curve objSync);


        [OperationContract]
        Profile GetG7NPDC(Curve objCurve);

        
    }
   
}
