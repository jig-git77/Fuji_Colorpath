using System.Collections.Generic;
using FujifilmUSA.ColorOrganizerService.DataContracts;
using FujifilmUSA.ColorOrganizerService.MessageContracts;
using Service.Common.Business;
using Service.Common.FaultContracts;
using FujifilmUSA.Sync.Business;
using FujifilmUSA.Sync.Business.Interface;
using Service.Common.DataContracts;

namespace FujifilmUSA.ColorOrganizerService.Business
{
    public partial class Sync4DBusiness
    {
        public static GetDeviceListResponse GetDeviceList(GetDeviceListRequest request)
        {
            var response = new GetDeviceListResponse
                               {
                                   Devices = new List<Device>()
                               };
            if (request == null)
            {
                request = new GetDeviceListRequest();
            }

            request.IsAuthenticationRequired = true;

            ExceptionHandling.LogMethod(request,
                                        () =>
                                        {
                                            var userBusiness = new UserBusiness();
                                            userBusiness.UserLogin(request.UserName, request.UserPassword);

                                            var deviceBusiness = BusinessFactory<IDeviceBusiness>.CreateObject();
                                            deviceBusiness.ErrorOccured += ErrorOccured;
                                            var devices = deviceBusiness.GetDevice(0, userBusiness.CurrentUser.UserId);
                                            
                                            if(devices.Count > 0)
                                            {
                                                devices.ForEach(x => response.
                                                                         Devices.
                                                                         Add(new Device()
                                                                                 {
                                                                                     DeviceId = x.DeviceId,
                                                                                     DeviceName = x.DeviceName
                                                                                 }));
                                                response.Count = response.Devices.Count;
                                                response.Message = Common.GetSuccessMessage();
                                            }
                                            else
                                            {
                                                ExceptionHandling.RaiseException("Devices not found", ErrorCode.DataNotFound);
                                            }
                                        }, "Get Device List");
            response.CalculateAndLogRequestTime("GetDeviceList");
            return response;
        }
    }
}