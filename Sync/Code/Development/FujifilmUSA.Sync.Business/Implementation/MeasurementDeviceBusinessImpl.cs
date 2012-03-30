using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MeasurementDeviceBusinessImpl : IMeasurementDeviceBusiness
    {
        #region  IMeasurementDevice Members

        public int InsertMeasurementDevice(MeasurementDevice measurementdevice)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[InsertMeasurementDevice]",
                            new object[]
                                    {
                                        measurementdevice.LicenseId,
                                        measurementdevice.SerialNumber,
                                        measurementdevice.InstrumentTypeId,
                                        measurementdevice.IsActive
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_InsertMeasurementDevice"));
            return returnValue;
        }

        public int UpdateMeasurementDevice(MeasurementDevice measurementdevice)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(
                () =>
                {
                    returnValue = DataAccessFactory.
                        Instance.
                        ExecuteProcedure(
                            "[CP].[UpdateMeasurementDevice]",
                            new object[]
                                    {
                                        measurementdevice.MeasurementDeviceId,
                                        measurementdevice.LicenseId,
                                        measurementdevice.SerialNumber,
                                        measurementdevice.InstrumentTypeId,
                                        measurementdevice.IsActive
                                    });
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_UpdateMeasurementDevice"));
            return returnValue;
        }

        public bool DeleteMeasurementDevice(int measurementdeviceId)
        {
            Common.ImplementBusinessLogic(
                () => DataAccessFactory.
                          Instance.
                          ExecuteProcedure(
                              "[CP].[DeleteMeasurementDevice]",
                              new object[]
                                  {
                                      measurementdeviceId
                                  }),
                new BusinessHandlerRequest(this, ErrorOccured, "Error_DeleteMeasurementDevice"));
            return true;
        }

        public List<MeasurementDevice> GetMeasurementDevice(int measurementDeviceId)
        {
            var measurementDevices = new List<MeasurementDevice>();
            Common.ImplementBusinessLogic(
                () =>
                    {
                        var dsmeasurementdevice = DataAccessFactory.Instance.GetDsResult("[CP].[GetMeasurementDevice]", new object[] {measurementDeviceId});
                        if (dsmeasurementdevice != null && dsmeasurementdevice.Tables.Count > 0 && dsmeasurementdevice.Tables[0].Rows.Count > 0)
                        {
                            measurementDevices = dsmeasurementdevice.Tables[0].AsEnumerable().
                                Select(x => new MeasurementDevice()
                                                {
                                                    MeasurementDeviceId = DataReader.ReadShort(x, "MeasurementDeviceID"),
                                                    CompanyId = DataReader.ReadInt(x, "CompanyID"),
                                                    CompanyName = DataReader.ReadString(x, "CompanyName"),
                                                    BranchId = DataReader.ReadInt(x, "BranchID"),
                                                    BranchName = DataReader.ReadString(x, "BranchName"),
                                                    LicenseId = DataReader.ReadInt(x, "LicenseID"),
                                                    LicenseTypeId = DataReader.ReadInt(x, "LicenseTypeID"),
                                                    LicenseType = DataReader.ReadString(x, "LicenseType"),
                                                    SerialNumber = DataReader.ReadString(x, "SerialNumber"),
                                                    InstrumentType = new InstrumentType() { InstrumentTypeId = (short)DataReader.ReadInt(x, "InstrumentTypeID"), InstrumentName = DataReader.ReadString(x, "InstrumentType") },
                                                    IsActive = DataReader.ReadBoolean(x, "IsActive")
                                                }).ToList();
                        }
                    },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementDevice"));
            return measurementDevices;
        }

        public List<MeasurementDevice> GetMeasurementDevices()
        {
            var measurementDevices = new List<MeasurementDevice>();
            Common.ImplementBusinessLogic(
                () =>
                {
                    var dsmeasurementdevice = DataAccessFactory.Instance.GetDsResult("[CP].[GetMeasurementDevices]");
                    if (dsmeasurementdevice != null && dsmeasurementdevice.Tables.Count > 0 && dsmeasurementdevice.Tables[0].Rows.Count > 0)
                    {
                        measurementDevices = dsmeasurementdevice.Tables[0].AsEnumerable().
                            Select(x => new MeasurementDevice()
                            {
                                MeasurementDeviceId = DataReader.ReadShort(x, "MeasurementDeviceID"),
                                CompanyName = DataReader.ReadString(x, "CompanyName"),
                                BranchName = DataReader.ReadString(x, "BranchName"),
                                LicenseType = DataReader.ReadString(x, "LicenseType"),
                                SerialNumber = DataReader.ReadString(x, "SerialNumber"),
                                InstrumentType = new InstrumentType() { InstrumentName = DataReader.ReadString(x, "InstrumentType") },
                                IsActive = DataReader.ReadBoolean(x, "IsActive")
                            }).ToList();
                    }
                },
                new BusinessHandlerRequest(this, ErrorOccured, "Error_GetMeasurementDevices"));
            return measurementDevices;
        }

      
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
