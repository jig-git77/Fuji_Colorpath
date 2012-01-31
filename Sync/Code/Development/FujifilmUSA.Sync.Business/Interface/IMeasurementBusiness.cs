using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FujifilmUSA.Sync.Entity;

namespace FujifilmUSA.Sync.Business.Interface
{
    public interface IMeasurementBusiness : ITaskBase
    {
        List<Measurement> GetMeasurement(int syncRunId);
        List<Measurement> GetMeasurement(int profileId, ProfileType typeId);
        int SaveMeasurement(Measurement measurement);
        int SaveMeasurement(MeasurementLink measurementLink);
        int DeleteMeasurement(int measurementId);
        List<MeasurementPatch> GetMeasurementPatch(int measurementID);
    }
}
