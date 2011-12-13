using System.Collections.Generic;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using System.Data;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using System;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MeasurementDeviceBusinessImpl : IMeasurementDeviceBusiness
    {
        #region IMeasurementDeviceBusiness Members

        public Dictionary<string, string> GetMeasurementDeviceListBySyncDeviceId(int syncDeviceId)
        {

            var dstMeasurementDeviceList = new Dictionary<string, string>();
            //========== Get data from data access=================//
            var dsMeasurementDeviceList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetMeasurementDeviceBySyncDeviceID]", new object[] { syncDeviceId });


            if (dsMeasurementDeviceList != null && dsMeasurementDeviceList.Tables.Count > 0)
            {
                dstMeasurementDeviceList = (dsMeasurementDeviceList.Tables[0].AsEnumerable().
                    Select(measurementDevice => new
                                                    {
                                                        Value =
                                                        DataReader.ReadInt(measurementDevice, "MeasurementDeviceTypeID") +
                                                        " - " +
                                                        DataReader.ReadInt(measurementDevice, "MeasurementDeviceID"),
                                                        Text =
                                                        DataReader.ReadString(measurementDevice, "MeasurementDeviceTypeName") + " - " + DataReader.ReadString(measurementDevice, "SerialNumber")
                                                    })).ToDictionary(measurementDevice => measurementDevice.Value, measurementDevice => measurementDevice.Text);

            }
            return dstMeasurementDeviceList;
        }

        #endregion




        #region Insert Sync Measurement Device
        public int InsertSyncMeasurementDevice(Entity.MeasurementDevice measurementDevice)
        {
            //var measurementDeviceInParameter = new List<Parameters>();
            ////var measurementDeviceOutParameter = new List<Parameters>();
            //measurementDeviceInParameter.Add(new Parameters() { ParameterName = "@CPDongleID", Size = 4, Type = DbType.Int16, Value = measurementDevice.CPDongleId });
            //measurementDeviceInParameter.Add(new Parameters() { ParameterName = "@SerialNumber", Size = 0, Type = DbType.String, Value = measurementDevice.SerialNumber });
            //measurementDeviceInParameter.Add(new Parameters() { ParameterName = "@MeasurementDeviceTypeID", Size = 4, Type = DbType.Int16, Value = measurementDevice.MeasurementDeviceTypeDetail.DeviceTypeId });
            //measurementDeviceInParameter.Add(new Parameters() { ParameterName = "@IsActive", Size = 1, Type = DbType.Boolean, Value = measurementDevice.IsActive });

            //measurementDeviceOutParameter.Add(new Parameters() { ParameterName = "@MeasurementDeviceID", Size = 4, Type = DbType.Int16 });

            return DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspInsertSyncMeasurementDevice]", new object[]{
                 measurementDevice.CPDongleId,
                measurementDevice.SerialNumber,
                measurementDevice.MeasurementDeviceTypeDetail.DeviceTypeId,
                measurementDevice.IsActive
            });

            //DataAccessFactory.Instance.ExecuteProcedureWithTransaction(
            //    new List<System.Action>()
            //        {
            //            () =>
            //                {
            //                    measurementDeviceOutParameter =
            //                        DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspInsertSyncMeasurementDevice]",
            //                                                                    measurementDeviceInParameter,
            //                                                                    measurementDeviceOutParameter);
            //                    measurementDevice.MeasurementDeviceId = Convert.ToInt16(measurementDeviceOutParameter.Single(x => x.ParameterName == "@MeasurementDeviceID").Value);
            //                }
            //        });
            //int measurementDeviceId = Convert.ToInt16(measurementDeviceOutParameter.Single(x => x.ParameterName == "@MeasurementDeviceID").Value);
            //return GetSyncMeasurementDeviceByMeasurementDeviceId(measurementDeviceId);
        }
        #endregion

        #region Update Sync Measurement Device
        public int UpdateSyncMeasurementDevice(Entity.MeasurementDevice measurementDevice)
        {
            //var measurementDeviceInParameter = new List<Parameters>();
            //measurementDeviceInParameter.Add(new Parameters() { ParameterName = "@CPDongleID", Size = 4, Type = DbType.Int16, Value = measurementDevice.CPDongleId });
            //measurementDeviceInParameter.Add(new Parameters() { ParameterName = "@SerialNumber", Size = 0, Type = DbType.String, Value = measurementDevice.SerialNumber });
            //measurementDeviceInParameter.Add(new Parameters() { ParameterName = "@MeasurementDeviceTypeID", Size = 4, Type = DbType.Int16, Value = measurementDevice.MeasurementDeviceTypeDetail.DeviceTypeId });
            //measurementDeviceInParameter.Add(new Parameters() { ParameterName = "@IsActive", Size = 1, Type = DbType.Boolean, Value = measurementDevice.IsActive });
            //measurementDeviceInParameter.Add(new Parameters() { ParameterName = "@MeasurementDeviceID", Size = 4, Type = DbType.Int16, Value = measurementDevice.MeasurementDeviceId });

            return DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspUpdateSyncMeasurementDevice]", new object[]{
             measurementDevice.MeasurementDeviceId,
                measurementDevice.CPDongleId,
                measurementDevice.SerialNumber,
                measurementDevice.MeasurementDeviceTypeDetail.DeviceTypeId,
                measurementDevice.IsActive
            });

            //return GetSyncMeasurementDeviceByMeasurementDeviceId(measurementDevice.MeasurementDeviceId);//get here
        }
        #endregion

        #region Delete Sync Measurement Device
        public int DeleteSyncMeasurementDevice(int syncMeasurementDeviceId)
        {
            return DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspDeleteSyncMeasurementDevice]", new object[]{
             syncMeasurementDeviceId
            });
        }
        #endregion


        #region Get Sync Measurement Device by MeasurementDeviceId
        public Entity.MeasurementDevice GetSyncMeasurementDeviceByMeasurementDeviceId(int measurementDeviceId)
        {
            //[uspGetSyncMeasurementDeviceByDeviceId]
            var measurementDevice = new Entity.MeasurementDevice();

            var dsMeasurementDevice = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetSyncMeasurementDeviceByDeviceId]", new object[] { measurementDeviceId });

            if (dsMeasurementDevice != null && dsMeasurementDevice.Tables.Count > 0 && dsMeasurementDevice.Tables[0].Rows.Count > 0)
            {
                measurementDevice = dsMeasurementDevice.Tables[0].AsEnumerable().
                    Select(x => new Entity.MeasurementDevice()
                    {
                        CPDongleId = DataReader.ReadInt(x, "CPDongleID"),
                        IsActive = DataReader.ReadBoolean(x, "IsActive"),
                        MeasurementDeviceId = DataReader.ReadInt(x, "CPSyncMeasurementDeviceID"),
                        SerialNumber = DataReader.ReadString(x, "SerialNumber"),
                        MeasurementDeviceTypeDetail = new Entity.MeasurementDeviceType()
                        {
                            DeviceTypeId = DataReader.ReadInt(x, "MeasurementDeviceTypeID"),
                            DeviceTypeName = DataReader.ReadString(x, "MeasurementDeviceTypeName")
                        }
                    }).FirstOrDefault();
            }
            return measurementDevice;
        }
        #endregion


        #region Get Measurement device List
        public List<Entity.MeasurementDevice> GetSyncMeasurementDeviceList()
        {
            List<Entity.MeasurementDevice> measurementDeviceList = new List<Entity.MeasurementDevice>();
            var dsMeasurementDeviceList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetSyncMeasurementDeviceByDeviceId]");

            if (dsMeasurementDeviceList != null && dsMeasurementDeviceList.Tables.Count > 0 && dsMeasurementDeviceList.Tables[0].Rows.Count > 0)
            {
                measurementDeviceList = dsMeasurementDeviceList.Tables[0].AsEnumerable().Select(x => new Entity.MeasurementDevice()
                {
                    CPDongleId = DataReader.ReadInt(x, "CPDongleID"),
                    IsActive = DataReader.ReadBoolean(x, "IsActive"),
                    MeasurementDeviceId = DataReader.ReadInt(x, "CPSyncMeasurementDeviceID"),
                    SerialNumber = DataReader.ReadString(x, "SerialNumber"),
                    MeasurementDeviceTypeDetail = new Entity.MeasurementDeviceType()
                    {
                        DeviceTypeId = DataReader.ReadInt(x, "MeasurementDeviceTypeID"),
                        DeviceTypeName = DataReader.ReadString(x, "MeasurementDeviceTypeName")
                    }
                }).ToList();
            }

            return measurementDeviceList;
        }
        #endregion


        public Dictionary<int, string> GetDongleList(int branchId)
        {
            Dictionary<int, string> dongleList = new Dictionary<int, string>();
            var dsDongleList = DataAccessFactory.Instance.GetDsResult("[CPV].[uspGetDongleListByBranchID]", new object[] { 
                branchId
            });
            if (dsDongleList != null && dsDongleList.Tables.Count > 0 && dsDongleList.Tables[0].Rows.Count > 0)
            {
                dongleList = dsDongleList.Tables[0].AsEnumerable().ToDictionary(x => DataReader.ReadInt(x, "CPDongleID"), x => DataReader.ReadString(x, "DongleDescription"));
            }
            return dongleList;
        }
        public Dictionary<int, string> GetMeasurementDeviceTypeList()
        {
            Dictionary<int, string> measurementDeviceTypeList = new Dictionary<int, string>();
            var dsMeasurementDeviceTypeList = DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetSyncMeasurementDeviceType]");
            if (dsMeasurementDeviceTypeList != null && dsMeasurementDeviceTypeList.Tables.Count > 0 && dsMeasurementDeviceTypeList.Tables[0].Rows.Count > 0)
            {
                measurementDeviceTypeList = dsMeasurementDeviceTypeList.Tables[0].AsEnumerable().ToDictionary(x => DataReader.ReadInt(x, "MeasurementDeviceTypeID"), x => DataReader.ReadString(x, "MeasurementDeviceTypeName"));
            }
            return measurementDeviceTypeList;
        }
    }
}
