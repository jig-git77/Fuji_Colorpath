using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ColorPathSync.MessageContracts;

namespace ColorPathSync
{
    [ServiceContract]
    public interface IProfiler
    {
        [OperationContract]
        CreateSourceProfileResponse CreateSourceProfile(CreateSourceProfileRequest request);
    }
}
