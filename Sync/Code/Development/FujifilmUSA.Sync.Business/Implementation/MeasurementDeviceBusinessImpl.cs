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
        #region IMeasurementDeviceBusiness Members

        public int InsertSyncMeasurementDevice(MeasurementDevice measurementDevice)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(() =>
            {
                returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspInsertSyncMeasurementDevice]", new object[]{
                 measurementDevice.CPDongleId,
                measurementDevice.SerialNumber,
                measurementDevice.MeasurementDeviceTypeDetail.DeviceTypeId,
                measurementDevice.IsActive
            });
            }, this, ErrorOccured);
            return returnValue;
        }

        public int UpdateSyncMeasurementDevice(MeasurementDevice measurementDevice)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(() =>
            {
                returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspUpdateSyncMeasurementDevice]", new object[]{
             measurementDevice.MeasurementDeviceId,
                measurementDevice.CPDongleId,
                measurementDevice.SerialNumber,
                measurementDevice.MeasurementDeviceTypeDetail.DeviceTypeId,
                measurementDevice.IsActive
            });

            }, this, ErrorOccured);
            return returnValue;
        }

        public bool DeleteSyncMeasurementDevice(int syncMeasurementDeviceId)
        {
            Common.ImplementBusinessLogic(() =>
            {
                DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspDeleteSyncMeasurementDevice]", new object[]{
             syncMeasurementDeviceId
            });

            }, this, ErrorOccured);
            return true;
        }

        public MeasurementDevice GetSyncMeasurementDevice(int measurementDeviceId)
        {
            var measurementDevice = new MeasurementDevice();
            Common.ImplementBusinessLogic(() =>
            {
                var dsMeasurementDevice = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetSyncMeasurementDeviceByDeviceId]", new object[] { measurementDeviceId });
                if (dsMeasurementDevice != null && dsMeasurementDevice.Tables.Count > 0 && dsMeasurementDevice.Tables[0].Rows.Count > 0)
                {
                    measurementDevice = dsMeasurementDevice.Tables[0].AsEnumerable().
                        Select(x => new MeasurementDevice()
                        {
                            CPDongleId = DataReader.ReadInt(x, "CPDongleID"),
                            IsActive = DataReader.ReadBoolean(x, "IsActive"),
                            MeasurementDeviceId = DataReader.ReadInt(x, "CPSyncMeasurementDeviceID"),
                            SerialNumber = DataReader.ReadString(x, "SerialNumber"),
                            CompanyId = DataReader.ReadInt(x, "CompanyID"),
                            BranchId = DataReader.ReadInt(x, "BranchID"),
                            MeasurementDeviceTypeDetail = new MeasurementDeviceType()
                            {
                                DeviceTypeId = DataReader.ReadInt(x, "MeasurementDeviceTypeID"),
                                DeviceTypeName = DataReader.ReadString(x, "MeasurementDeviceTypeName")
                            }
                        }).FirstOrDefault();
                }
            }, this, ErrorOccured);
            return measurementDevice;
        }

        public List<MeasurementDevice> GetSyncMeasurementDevices()
        {
            var measurementDeviceList = new List<MeasurementDevice>();
            Common.ImplementBusinessLogic(() =>
            {
                var dsMeasurementDeviceList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetSyncMeasurementDeviceList]");

                if (dsMeasurementDeviceList != null && dsMeasurementDeviceList.Tables.Count > 0 && dsMeasurementDeviceList.Tables[0].Rows.Count > 0)
                {
                    measurementDeviceList = dsMeasurementDeviceList.Tables[0].AsEnumerable().Select(x => new MeasurementDevice()
                    {
                        CPDongleId = DataReader.ReadInt(x, "CPDongleID"),
                        IsActive = DataReader.ReadBoolean(x, "IsActive"),
                        MeasurementDeviceId = DataReader.ReadInt(x, "CPSyncMeasurementDeviceID"),
                        SerialNumber = DataReader.ReadString(x, "SerialNumber"),
                        CompanyName = DataReader.ReadString(x, "CompanyName"),
                        BranchName = DataReader.ReadString(x, "BranchName"),
                        CompanyId = DataReader.ReadInt(x, "CompanyID"),
                        BranchId = DataReader.ReadInt(x, "BranchID"),
                        CPSerialNumber = DataReader.ReadString(x, "CPSerialNum"),
                        MeasurementDeviceTypeDetail = new MeasurementDeviceType()
                        {
                            DeviceTypeId = DataReader.ReadInt(x, "MeasurementDeviceTypeID"),
                            DeviceTypeName = DataReader.ReadString(x, "MeasurementDeviceTypeName")
                        }
                    }).ToList();
                }
            }, this, ErrorOccured);
            return measurementDeviceList;
        }

        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
