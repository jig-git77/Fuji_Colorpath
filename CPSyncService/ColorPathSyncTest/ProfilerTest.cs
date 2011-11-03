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
                                                   Options = GetOptions()
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
                                                                    Options = GetOptions()
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
                                                                   Options = GetOptions(),
                                                                   C_X = C_X,
                                                                   M_Y = M_Y,
                                                                   Y_Z = Y_Z,
                                                                   K = K
                                                               });
                Assert.IsTrue(response.Severity != MessageSeverity.Error);
            }
        }

        #region Private Methods
        private static Parameter[] GetOptions()
        {
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
