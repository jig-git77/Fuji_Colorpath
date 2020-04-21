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
        CreateDestinationAndDeviceLinkProfileResponse CreateDestinationAndDeviceLinkProfile(CreateDestinationAndDeviceLinkProfileRequest request);

        [OperationContract]
        OptimizeDeviceLinkProfileResponse OptimizeDeviceLinkProfile(OptimizeDeviceLinkProfileRequest request);

        [OperationContract]
        CreateSourceToSourceDeviceLinkProfileResponse CreateSourceToSourceDeviceLinkProfile(CreateSourceToSourceDeviceLinkProfileRequest request);

        [OperationContract]
        CreateG7AlignmentCurveResponse CreateG7AlignmentCurve(CreateG7AlignmentCurveRequest request);

        [OperationContract]
        CreateISOAlignmentCurveResponse CreateISOAlignmentCurve(CreateISOAlignmentCurveRequest request);

        [OperationContract]
        CreateSourceProfileResponse CreateSourceProfile(CreateSourceProfileRequest request);
              
        [OperationContract]
        ExtractProfileValuesResponse ExtractProfileValues(ExtractProfileValuesRequest request);

        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [OperationContract]
        CreateSpotLibraryResponse CreateSpotLibraryValues(CreateSpotLibraryRequest request);

        [System.ServiceModel.XmlSerializerFormatAttribute()]
        [OperationContract]
        OptimizeSpotLibraryResponse OptimizeSpotLibraryValues(OptimizeSpotLibraryRequest request);

        ////Declaration for the ISO and G7 for Alignment Curve (Old Code)
        //[OperationContract]
        //Profile CreateCurve(Curve objSync);

        //[OperationContract]
        //Profile CreateISOCurve(Curve objSync);

    }
}
