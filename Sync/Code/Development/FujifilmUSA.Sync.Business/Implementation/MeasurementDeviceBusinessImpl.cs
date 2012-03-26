using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using System;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Implementation
{
  public class MeasurementDeviceBusinessImpl : IMeasurementDeviceBusiness
  {
      #region  IMeasurementDevice Members

      public int InsertMeasurementDevice(MeasurementDevice measurementdevice)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CP].[InsertMeasurementDevice]", new object[]{
                                                                                                                measurementdevice.LicenseId,
                                                                                                                measurementdevice.SerialNumber,
                                                                                                                measurementdevice.InstrumentTypeId,
                                                                                                                measurementdevice.IsActive
                                                                                                });
            return returnValue;
        }

      public int UpdateMeasurementDevice(MeasurementDevice measurementdevice)
        {
            int returnValue = 0;
            returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CP].[UpdateMeasurementDevice]", new object[]{
                                                                                                                measurementdevice.MeasurementDeviceId,
                                                                                                                measurementdevice.LicenseId,
                                                                                                                measurementdevice.SerialNumber,
                                                                                                                measurementdevice.InstrumentTypeId,
                                                                                                                measurementdevice.IsActive
                                                                                                 });
            return returnValue;
        }

      public bool DeleteMeasurementDevice(int measurementdeviceId)
        {
            DataAccessFactory.Instance.ExecuteProcedure("[CP].[DeleteMeasurementDevice]", new object[]{
             measurementdeviceId
            });
            return true;
        }

      public List<MeasurementDevice> GetMeasurementDevices()
        {
            var MeasurementDevices = new List<MeasurementDevice>();
            var dsmeasurementdevice = DataAccessFactory.Instance.GetDsResult("[CP].[GetMeasurementDevices]");
            if (dsmeasurementdevice != null && dsmeasurementdevice.Tables.Count > 0 && dsmeasurementdevice.Tables[0].Rows.Count > 0)
            {
                MeasurementDevices = dsmeasurementdevice.Tables[0].AsEnumerable().
                    Select(x => new MeasurementDevice()
                    {
                        MeasurementDeviceId = DataReader.ReadShort(x, "MeasurementDeviceID"),
                        CompanyName = DataReader.ReadString(x, "CompanyName"),
                        BranchName = DataReader.ReadString(x, "BranchName"),
                        LicenseType = DataReader.ReadString(x, "LicenseType"),
                        SerialNumber = DataReader.ReadString(x, "SerialNumber"),
                        InstrumentType = new InstrumentType() { InstrumentName = DataReader.ReadString(x, "InstrumentType") },
                        IsActive = DataReader.ReadBoolean(x,"IsActive")
                    }).ToList();
            }
            return MeasurementDevices;
        }

      public MeasurementDevice GetMeasurementDevice(int iMeasurementDeviceID)
      {
          var MeasurementDevices = new MeasurementDevice();
          var dsmeasurementdevice = DataAccessFactory.Instance.GetDsResult("[CP].[GetMeasurementDevice]",new object[]{iMeasurementDeviceID});
          if (dsmeasurementdevice != null && dsmeasurementdevice.Tables.Count > 0 && dsmeasurementdevice.Tables[0].Rows.Count > 0)
          {
              MeasurementDevices = dsmeasurementdevice.Tables[0].AsEnumerable().
                  Select(x => new MeasurementDevice()
                  {
                      MeasurementDeviceId = DataReader.ReadShort(x, "MeasurementDeviceID"),
                      CompanyId = DataReader.ReadInt(x, "CompanyID"),
                      BranchId = DataReader.ReadInt(x, "BranchID"),
                      LicenseId = DataReader.ReadInt(x, "LicenseID"),
                      LicenseTypeId = DataReader.ReadInt(x, "LicenseTypeID"),
                      SerialNumber = DataReader.ReadString(x, "SerialNumber"),
                      InstrumentType = new InstrumentType() { InstrumentTypeId = (short)DataReader.ReadInt(x, "InstrumentTypeID") },
                      IsActive = DataReader.ReadBoolean(x, "IsActive")
                  }).SingleOrDefault();
          }
          return MeasurementDevices;
      }

        #endregion
    }
}
