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
        bool Ping();

        [OperationContract]
        CreateSourceProfileResponse CreateSourceProfile(CreateSourceProfileRequest request);

        [OperationContract]
        CreateDeviceLinkProfileResponse CreateDeviceLinkProfile(CreateDeviceLinkProfileRequest request);

        [OperationContract]
        OptimizeDeviceLinkProfileResponse OptimizeDeviceLinkProfile(OptimizeDeviceLinkProfileRequest request);

        [OperationContract]
        CreateDestinationAndDeviceLinkProfileResponse CreateDestinationAndDeviceLinkProfile(CreateDestinationAndDeviceLinkProfileRequest request);

        [OperationContract]
        ExtractProfileValuesResponse ExtractProfileValues(ExtractProfileValuesRequest request);

        [OperationContract]
        CreateG7AlignmentCurveResponse CreateG7AlignmentCurve(CreateG7AlignmentCurveRequest request);

        [OperationContract]
        CreateISOAlignmentCurveResponse CreateISOAlignmentCurve(CreateISOAlignmentCurveRequest request);

        [OperationContract]
        Profile CreateCurve(Curve objSync);

        [OperationContract]
        Profile CreateISOCurve(Curve objSync);

    }
}
