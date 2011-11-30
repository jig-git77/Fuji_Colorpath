using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.DataAccess;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Common;
using System.Data;
using System.Linq;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class SyncRunBusinessImpl : ISyncRunBusiness
    {
        #region ISyncRunBusiness Members

        public int SaveMeasurement(Measurement measurement)
        {
            if (measurement == null || measurement.Patches == null || measurement.Patches.Count == 0) return 0;

            var outParams = DataAccessFactory.Instance.
                ExecuteProcedure("CPSync4D.uspInsertMeasurement",
                                 new List<Parameters>()
                                     {
                                         new Parameters() {ParameterName = "@SyncRunID", Value = measurement.SyncRunId},
                                         new Parameters() {ParameterName = "@MeasurementDate", Value = measurement.MeasurementDate},
                                         new Parameters() {ParameterName = "@MeasurementName", Value = measurement.MeasurementName},
                                         new Parameters() {ParameterName = "@MeasurementTypeID", Value = measurement.MeasurementTypeId},
                                     },
                                 new List<Parameters>()
                                     {
                                         new Parameters() {ParameterName = "@MeasurementID"},
                                     });

            measurement.MeasurementId = System.Convert.ToInt32(outParams[0].Value);
            measurement.Patches.ForEach(patch => DataAccessFactory.Instance.
                                                     ExecuteProcedure("CPSync4D.uspInsertMeasuredPatch",
                                                                      new object[]
                                                                          {
                                                                              patch.MeasurementId,
                                                                              patch.SampleName,
                                                                              patch.CmykC,
                                                                              patch.CmykM,
                                                                              patch.CmykY,
                                                                              patch.CmykK,
                                                                              patch.CielabL,
                                                                              patch.CielabA,
                                                                              patch.CielabB,
                                                                              patch.CiexyzX,
                                                                              patch.CiexyzY,
                                                                              patch.CiexyzZ,
                                                                              patch.DensityV,
                                                                              patch.DensityC,
                                                                              patch.DensityM,
                                                                              patch.DensityY
                                                                          }));
            return measurement.MeasurementId;
        }

        public List<Measurement> GetMeasurement(int syncRunId)
        {
            if (syncRunId <= 0) return new List<Measurement>();

            var dsMeasurements = DataAccessFactory.Instance.GetResult("[CPSync4D].[uspGetMeasurementByPressRunID]", new object[] {syncRunId});
            var lstMeasurements = dsMeasurements.Tables[0].AsEnumerable().
                Select(x => new Measurement()
                                {
                                    MeasurementId = DsReader.ReadInt(x, "MeasurementID"),
                                    MeasurementName = DsReader.ReadString(x, "MeasurementName"),
                                    MeasurementDate = DsReader.ReadDate(x, "MeasurementDate"),
                                    SyncRunId = DsReader.ReadInt(x, "SyncRunID"),
                                    IsUse = DsReader.ReadBoolean(x, "IsUse")
                                }).ToList();
            return lstMeasurements;
        }

        #endregion
    }
}