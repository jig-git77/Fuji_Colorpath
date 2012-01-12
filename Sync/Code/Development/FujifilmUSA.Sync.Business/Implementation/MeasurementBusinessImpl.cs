using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Common;
using System.Data;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class MeasurementBusinessImpl : IMeasurementBusiness
    {
        #region IMeasurementBusiness Members

        public List<Measurement> GetMeasurement(int syncRunId)
        {
            List<Measurement> lstMeasurement = new List<Measurement>();
            Common.ImplementBusinessLogic(() =>
            {
                if (syncRunId <= 0)
                    lstMeasurement = new List<Measurement>();
                else
                    lstMeasurement = ConvertMeasurement(DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetMeasurementBySyncRunID]", new object[] { syncRunId }));

            }, this, ErrorOccured);
            return lstMeasurement;
        }

        public List<Measurement> GetMeasurement(int profileId, ProfileType typeId)
        {
            List<Measurement> lstMeasurement = new List<Measurement>();
            Common.ImplementBusinessLogic(() =>
            {
                if (profileId <= 0 || typeId == ProfileType.None) 
                    lstMeasurement = new List<Measurement>();
                else
                    lstMeasurement = ConvertMeasurement(DataAccessFactory.Instance.GetDsResult("[CPSync4D].[uspGetMeasurementByProfileID]", new object[] { profileId, typeId }));
            }, this, ErrorOccured);
            return lstMeasurement;
        }

        public int SaveMeasurement(Measurement measurement)
        {
            if (measurement == null || measurement.Patches == null || measurement.Patches.Count == 0) return 0;

            Common.ImplementBusinessLogic(() =>
            {
                DataAccessFactory.
                Instance.
                ExecuteProcedureWithTransaction(
                    new List<Action>()
                        {
                            () =>
                                {
                                    var outParams = DataAccessFactory.Instance.
                                        ExecuteProcedure("CPSync4D.uspInsertMeasurement",
                                                         new List<Parameters>()
                                                             {
                                                                 new Parameters() {ParameterName = "@SyncRunID", Size = 0, Value = measurement.SyncRunId},
                                                                 new Parameters() {ParameterName = "@MeasurementDate", Size = 0, Value = measurement.MeasurementDate},
                                                                 new Parameters() {ParameterName = "@MeasurementName", Size = 0, Value = measurement.MeasurementName},
                                                                 new Parameters() {ParameterName = "@MeasurementTypeID", Size = 0, Value = measurement.MeasurementTypeId},
                                                             },
                                                         new List<Parameters>()
                                                             {
                                                                 new Parameters() {ParameterName = "@MeasurementID", Size = 4},
                                                             });

                                    measurement.MeasurementId = System.Convert.ToInt32(outParams[0].Value);
                                },
                            () =>
                                {
                                    measurement.Patches.ForEach(patch => DataAccessFactory.Instance.
                                                                             ExecuteProcedure("CPSync4D.uspInsertMeasuredPatch",
                                                                                              new object[]
                                                                                                  {
                                                                                                      measurement.MeasurementId,
                                                                                                      null,
                                                                                                      patch.SampleName,
                                                                                                      patch.CmykC,
                                                                                                      patch.CmykM,
                                                                                                      patch.CmykY,
                                                                                                      patch.CmykK,
                                                                                                      patch.Cielab.L,
                                                                                                      patch.Cielab.A,
                                                                                                      patch.Cielab.B,
                                                                                                      patch.Ciexyz.X,
                                                                                                      patch.Ciexyz.Y,
                                                                                                      patch.Ciexyz.Z,
                                                                                                      patch.Density.Visual,
                                                                                                      patch.Density.Red,
                                                                                                      patch.Density.Green,
                                                                                                      patch.Density.Blue
                                                                                                  }));
                                }
                        });
            }, this, ErrorOccured);
            return measurement.MeasurementId;
        }

        public int SaveMeasurement(MeasurementLink measurementLink)
        {
            if (measurementLink == null || measurementLink.Measurement == null || measurementLink.Measurement.Patches == null || measurementLink.Measurement.Patches.Count == 0)
                return 0;
            measurementLink.Measurement.MeasurementId = SaveMeasurement(measurementLink.Measurement);

            Common.ImplementBusinessLogic(() =>
            {
                DataAccessFactory.Instance.
                ExecuteProcedure("[CPSync4D].[uspInsertMeasurementLink]",
                                 new object[]
                                     {
                                         measurementLink.Measurement.MeasurementId,
                                         Convert.ToInt32(measurementLink.TypeId),
                                         measurementLink.ProfileId
                                     });
            }, this, ErrorOccured);

            return measurementLink.Measurement.MeasurementId;
        }

        public int DeleteMeasurement(int measurementId)
        {
            int returnValue = 0;
            Common.ImplementBusinessLogic(() =>
            {
                returnValue = DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspDeleteMeasurement]",
                                                               new object[] { measurementId });
            }, this, ErrorOccured);
            return returnValue;
        }

        #endregion

        #region Private Methods
        internal static List<Measurement> ConvertMeasurement(DataSet dsMeasurements)
        {
            var lstMeasurements = dsMeasurements.Tables[0].AsEnumerable().
                Select(x => new Measurement()
                {
                    MeasurementId = DataReader.ReadInt(x, "MeasurementID"),
                    MeasurementName = DataReader.ReadString(x, "MeasurementName"),
                    MeasurementDate = DataReader.ReadDate(x, "MeasurementDate"),
                    SyncRunId = DataReader.ReadInt(x, "SyncRunID"),
                    IsUse = DataReader.ReadBoolean(x, "IsUse")
                }).ToList();
            return lstMeasurements;
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
