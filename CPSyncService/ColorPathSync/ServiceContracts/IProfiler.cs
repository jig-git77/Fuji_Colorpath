using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using ColorPathSync.MessageContracts;

namespace ColorPathSync.ServiceContracts
{
    [ServiceContract]
    public interface IProfiler
    {
        [OperationContract]
        CreateSourceProfileResponse CreateSourceProfile(CreateSourceProfileRequest request);

        [OperationContract]
        CreateDeviceLinkProfileResponse CreateDeviceLinkProfile(CreateDeviceLinkProfileRequest request);

        [OperationContract]
        OptimizeDeviceLinkProfileResponse OptimizeDeviceLinkProfile(OptimizeDeviceLinkProfileRequest request);

        [OperationContract]
        ExtractProfileValuesResponse ExtractProfileValues(ExtractProfileValuesRequest request);
    }
}
