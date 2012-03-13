using System;
using System.Data;
using System.Linq;
using FujifilmUSA.Sync.Business.Interface;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.DataAccess;
using System.Collections.Generic;
using FujifilmUSA.Sync.Common;
using FujifilmUSA.Sync.Business.ProfilerService;

namespace FujifilmUSA.Sync.Business.Implementation
{
    public class OptimizeRunBusinessImpl : IOptimizeRunBusiness
    {
        #region IOptimizeRunBusiness Members

        public void CreateOptimizeRun(int projectId, int status, DateTime startDateTime)
        {
            DataAccessFactory.Instance.ExecuteProcedure("CPSync4D.CreateOptimizeRun",
                                            new object[] { projectId, status, startDateTime });
        }

        public void CreateInitialDestinationAndDeviceLinkProfiles(int projectId, int optimizeRunId, List<int> measurementIds)
        {
            var sourceProfile = (byte[])DataAccessFactory.Instance.GetScalar("CPSync4D.GetSourceProfileData", new object[] { projectId });
            var request = new CreateDestinationAndDeviceLinkProfileRequest()
                              {
                                  PcsData = new List<double>(),
                                  DcsData = new List<double>(),
                                  SourceProfile = new IccProfile() { ProfileData = sourceProfile }
                              };

            var measurementDataResponse = BusinessFactory<IMeasurementBusiness>.CreateObject().GetMeasurementData(measurementIds);
            request.PcsData = measurementDataResponse.PcsData;
            request.DcsData = measurementDataResponse.DcsData;

            //Add options and call profile service to generate project dlp
            //var profilerOptionDetail = BusinessFactory<IProfilerOptionDetailBusiness>().CreateObject());
            //var optionDetails = profilerOptionDetail.GetProfilerOptionDetails(profileOptionId, ProfileType.Optimization);


            //Add options and call profile service to generate optimize dlp
            using (var client = new ProfilerClient())
            {
                //request.Options = new List<Parameter>();
                //optionDetails.ForEach(
                //                option => request.Options.Add(new Parameter() { Name = option.Key, Value = option.Value }));
                var response = client.CreateDestinationAndDeviceLinkProfile(request);
                //optimizeDlp.Data.Profile = response.OptimizedProfile.ProfileData;
            }

            //Store optimize dlp to current sync run
            //DataAccessFactory.Instance.ExecuteProcedure("[CPSync4D].[uspUpdateICCProfile]", new object[] { syncRunId, optimizeDlp.Data.Profile });

        }


        public byte[] GetDeviceLinkProfile(int optimizeRunId)
        {
            throw new NotImplementedException();
        }

        public List<Measurement> GetMeasurements(int optimizeRunId)
        {
            var dsMeasurements = DataAccessFactory.Instance.GetDsResult("CPSync4D.GetMeasurementsByOptimizeRunID", new object[] { optimizeRunId });
            var measurements = dsMeasurements.Tables[0].AsEnumerable().
                Select(x => new Measurement()
                                {
                                    MeasurementId = DataReader.ReadInt(x, "MeasurementID"),
                                    MeasurementName = DataReader.ReadString(x, "MeasurementName"),
                                    MeasurementDate = DataReader.ReadDate(x, "MeasurementDate"),
                                    OptimizeRunId = DataReader.ReadInt(x, "OptimizeRunID"),
                                    IsUsed = DataReader.ReadBoolean(x, "IsUsed")
                                }).ToList();
            return measurements;
        }

        #endregion
    }
}
