using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using ColorPathSyncTest.Profiler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace ColorPathSyncTest
{
    [TestClass]
    public class ProfilerTest
    {
        private const string PcsFile = "..\\Out\\XYZValues.txt";
        private const string DcsFile = "..\\Out\\CMYKValues.txt";

        [TestMethod]
        public void CreateSourceProfileTest()
        {
            using (var client = new Profiler.ProfilerClient())
            {
                var response = client.CreateSourceProfile(new CreateSourceProfileRequest()
                                                              {
                                                                  DCSData = ReadFile(DcsFile),
                                                                  PCSData = ReadFile(PcsFile),
                                                                  Options = GetOptions(SetOptions.CreateSourceProfile)
                                                              });
                Assert.IsTrue(response.Severity != MessageSeverity.Error);
            }
        }

        [TestMethod]
        public void ExtractProfileValuesTest()
        {
            using (var client = new Profiler.ProfilerClient())
            {
                byte[] profile = client.CreateSourceProfile(new CreateSourceProfileRequest()
                                                                {
                                                                    DCSData = ReadFile(DcsFile),
                                                                    PCSData = ReadFile(PcsFile),
                                                                    Options = GetOptions(SetOptions.CreateSourceProfile)
                                                                }).Profile.ProfileData;

                var C_X = new double[] { 40 };
                var M_Y = new double[] { 70 };
                var Y_Z = new double[] { 70 };
                var K = new double[] { 10 };
                //var C_X = new double[] { 32.84 };
                //var M_Y = new double[] { 17 };
                //var Y_Z = new double[] { 15.44 };
                //var K = new double[] { 0 };
                var response = client.ExtractProfileValues(new ExtractProfileValuesRequest()
                                                               {
                                                                   ExtractType = ProfileExtractType.Xyz,
                                                                   Profile = new IccProfile()
                                                                                 {
                                                                                     ProfileData = profile
                                                                                 },
                                                                   C_X = C_X,
                                                                   M_Y = M_Y,
                                                                   Y_Z = Y_Z,
                                                                   K = K
                                                               });
                Assert.IsTrue(response.Severity != MessageSeverity.Error);
            }
        }

        [TestMethod]
        public void CreateDeviceLinkProfileTest()
        {
            using (var client = new Profiler.ProfilerClient())
            {
                var srcProfile = client.CreateSourceProfile(new CreateSourceProfileRequest()
                                                                {
                                                                    DCSData = ReadFile(DcsFile),
                                                                    PCSData = ReadFile(PcsFile),
                                                                    Options = GetOptions(SetOptions.CreateSourceProfile)
                                                                }).Profile.ProfileData;
                var destProfile = srcProfile;
                var response = client.CreateDeviceLinkProfile(new CreateDeviceLinkProfileRequest()
                                                                  {
                                                                      SourceProfile = new IccProfile()
                                                                                          {
                                                                                              ProfileData = srcProfile
                                                                                          },
                                                                      DestinationProfile = new IccProfile()
                                                                                               {
                                                                                                   ProfileData = destProfile
                                                                                               },
                                                                      Options = GetOptions(SetOptions.CreateDeviceLinkProfile)
                                                                  });
                Assert.IsTrue(response.Severity != MessageSeverity.Error);
            }
        }

        [TestMethod]
        public void OptimizeDeviceLinkProfileTest()
        {
            using (var client = new Profiler.ProfilerClient())
            {
                var destProfile = client.CreateSourceProfile(new CreateSourceProfileRequest()
                                                                 {
                                                                     DCSData = ReadFile(DcsFile),
                                                                     PCSData = ReadFile(PcsFile),
                                                                     Options = GetOptions(SetOptions.CreateSourceProfile)
                                                                 }).Profile.ProfileData;

                var dlp = client.CreateDeviceLinkProfile(new CreateDeviceLinkProfileRequest()
                                                             {
                                                                 SourceProfile = new IccProfile()
                                                                                     {
                                                                                         ProfileData = destProfile
                                                                                     },
                                                                 DestinationProfile = new IccProfile()
                                                                                          {
                                                                                              ProfileData = destProfile
                                                                                          },
                                                                 Options = GetOptions(SetOptions.CreateDeviceLinkProfile)
                                                             }).DeviceLinkProfile.ProfileData;

                var response = client.OptimizeDeviceLinkProfile(new OptimizeDeviceLinkProfileRequest()
                                                                    {
                                                                        SourceProfile = new IccProfile()
                                                                                            {
                                                                                                ProfileData = destProfile
                                                                                            },
                                                                        DestinationProfile = new IccProfile()
                                                                                                 {
                                                                                                     ProfileData = destProfile
                                                                                                 },
                                                                        DeviceLinkProfile = new IccProfile()
                                                                                                {
                                                                                                    ProfileData = dlp
                                                                                                },
                                                                        PCSData = ReadFile(PcsFile),
                                                                        DCSData = ReadFile(DcsFile),
                                                                        Options = GetOptions(SetOptions.OptimizeDeviceLinkProfile)
                                                                    });
                Assert.IsTrue(response.Severity != MessageSeverity.Error);
            }
        }

        #region Private Methods
        private enum SetOptions
        {
            None = 0,
            CreateSourceProfile = 1,
            CreateDestinationProfile = 2,
            CreateDeviceLinkProfile = 3,
            OptimizeDeviceLinkProfile = 4
        }

        private static Parameter[] GetOptions(SetOptions options)
        {
            switch (options)
            {
                case SetOptions.None:
                    break;
                case SetOptions.CreateSourceProfile:
                    return new Parameter[]
                               {
                                   new Parameter() {Name = "ProfileType", Value = "Printer"},
                                   new Parameter() {Name = "ProfileSize", Value = "2"},
                                   new Parameter() {Name = "ProfileFormat", Value = "4"},
                                   new Parameter() {Name = "SaveProfilerInfo", Value = "1"},
                                   new Parameter() {Name = "GridA2B0", Value = "3"},
                                   new Parameter() {Name = "GridA2B2", Value = "3"},
                                   new Parameter() {Name = "GridB2A0", Value = "3"},
                                   new Parameter() {Name = "GridB2A1", Value = "3"},
                                   new Parameter() {Name = "GridB2A2", Value = "3"},
                                   new Parameter() {Name = "Description", Value = "Test Profiler WFC Service"}
                               };
                    break;
                case SetOptions.CreateDestinationProfile:
                    break;
                case SetOptions.CreateDeviceLinkProfile:
                    return new Parameter[]
                               {
                                   new Parameter() {Name = "ProfileType", Value = "DeviceLink"},
                                   new Parameter() {Name = "ProfileSize", Value = "2"},
                                   new Parameter() {Name = "Rendering", Value = "14"},
                                   new Parameter() {Name = "PerceptualCompression2", Value = "16"},
                                   new Parameter() {Name = "PerceptualCompressionMergeFactor", Value = "75"},
                                   new Parameter() {Name = "DLType", Value = "6"},
                                   new Parameter() {Name = "DLExceptions", Value = "65536"},
                                   new Parameter() {Name = "DLOptimizeExceptions", Value = "524289"},
                                   new Parameter() {Name = "DLExceptionRange", Value = "15"},
                                   new Parameter() {Name = "DLOptimizeInkRange", Value = "0.2"},
                                   new Parameter() {Name = "Description", Value = "7900/Xerox 1000 DLP Setting v1.0"},
                                   new Parameter() {Name = "BlackGeneration", Value = "GCR2Smooth"},
                                   new Parameter() {Name = "InkMax", Value = "79 79 77 95 BalanceCMY"},
                                   new Parameter() {Name = "InkTotal", Value = "330"},
                                   new Parameter() {Name = "BlackStart", Value = "10"},
                                   new Parameter() {Name = "BlackWidth", Value = "100"}
                               };
                    break;
                case SetOptions.OptimizeDeviceLinkProfile:
                    return new Parameter[]
                               {
                                   new Parameter() {Name = "ProfileType", Value = "OptimizeDeviceLink"},
                                   new Parameter() {Name = "ProfileSize", Value = "2"},
                                   new Parameter() {Name = "Rendering", Value = "3"},
                                   new Parameter() {Name = "DLBypassBlack", Value = "100"},
                                   new Parameter() {Name = "DLExceptions", Value = "129"},
                                   new Parameter() {Name = "ProfileFormat", Value = "2"},
                                   new Parameter() {Name = "DLOptimizationLimit", Value = "0"},
                                   new Parameter() {Name = "Flags", Value = "0"},
                                   new Parameter() {Name = "Attributes0", Value = "0"},
                                   new Parameter() {Name = "Attributes1", Value = "0"},
                                   new Parameter() {Name = "SaveProfilerInfo", Value = "1"}
                               };
                    break;
                default:
                    throw new ArgumentOutOfRangeException("options");
            }
            return new Parameter[] {};
        }

        private static double[] ReadFile(string filePath)
        {
            var data = new List<double>();
            string line;
            var file = new System.IO.StreamReader(filePath);
            while ((line = file.ReadLine()) != null)
            {
                data.AddRange(line.Split('\t').Select(Convert.ToDouble).ToList());
            }
            file.Close();
            return data.ToArray();
        }

        #endregion
    }
}