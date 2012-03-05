using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Mock
{
    public class MeasurementBusinessMock : IMeasurementBusiness
    {
        #region IMeasurementBusiness Members

        public List<Measurement> GetMeasurement(int syncRunId)
        {
            return new List<Measurement>();
        }

        public List<Measurement> GetMeasurement(int profileId, Entity.ProfileType typeId)
        {
            return new List<Measurement>();
        }

        public int SaveMeasurement(Measurement measurement)
        {
            return 0;
        }

        public int SaveMeasurement(MeasurementLink measurementLink)
        {
            return 0;
        }

        public int DeleteMeasurement(int measurementId)
        {
            return 0;
        }
        public List<MeasurementPatch> GetMeasurementPatch(int measurementID)
        {
            return new List<MeasurementPatch>();
        }
        #endregion

        #region ITaskBase Members

        public event ErrorHandler ErrorOccured;

        #endregion
    }
}
