using FujifilmUSA.Sync.Business.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Implementation;

namespace FujifilmUSA.Sync.Business.Test
{
    
    
    /// <summary>
    ///This is a test class for ISyncRunBusinessTest and is intended
    ///to contain all ISyncRunBusinessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ISyncRunBusinessTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        internal virtual ISyncRunBusiness CreateISyncRunBusiness()
        {
            // TODO: Instantiate an appropriate concrete class.
            ISyncRunBusiness target = new SyncRunBusinessImpl();
            return target;
        }

        /// <summary>
        ///A test for GetIccProfileBySyncRunId
        ///</summary>
        [TestMethod()]
        public void GetIccProfileBySyncRunIdTest()
        {
            ISyncRunBusiness target = CreateISyncRunBusiness(); // TODO: Initialize to an appropriate value
            int syncRunId = 29; // TODO: Initialize to an appropriate value

            byte[] actual;
            actual = target.GetIccProfileBySyncRunId(syncRunId);
            Assert.IsTrue(true);

        }

        /// <summary>
        ///A test for GetMasterSyncRunId
        ///</summary>
        [TestMethod()]
        public void GetMasterSyncRunIdTest()
        {
            ISyncRunBusiness target = CreateISyncRunBusiness(); // TODO: Initialize to an appropriate value
            int syncRunId = 29; // TODO: Initialize to an appropriate value

            int actual;
            actual = target.GetMasterSyncRunId(syncRunId);
            Assert.IsTrue(true);



        }

        /// <summary>
        ///A test for GetMeasurement
        ///</summary>
        [TestMethod()]
        public void GetMeasurementTest()
        {
            ISyncRunBusiness target = CreateISyncRunBusiness(); // TODO: Initialize to an appropriate value
            int syncRunId = 6;

            List<Measurement> actual;
            actual = target.GetMeasurement(syncRunId);
            Assert.IsTrue(true);



        }


        /// <summary>
        ///A test for SaveMeasurement
        ///</summary>
        [TestMethod()]
        public void SaveMeasurementTest()
        {
            ISyncRunBusiness target = CreateISyncRunBusiness(); // TODO: Initialize to an appropriate value
            Measurement measurement = new Measurement(); // TODO: Initialize to an appropriate value

            MeasurementPatch patch = new MeasurementPatch();
            patch.Cielab = new CieLab();
            patch.Ciexyz = new CieXyz();
            patch.Density = new Density();

            patch.SampleName = "Test A4";
            patch.CmykC = 0;
            patch.CmykM = 0;
            patch.CmykY = 0;
            patch.CmykK = 0;
            patch.Cielab.L = 0;
            patch.Cielab.A = 0;
            patch.Cielab.B = 0;
            patch.Ciexyz.X = 0;
            patch.Ciexyz.Y = 0;
            patch.Ciexyz.Z = 0;
            patch.Density.Visual = 0;
            patch.Density.Red = 0;
            patch.Density.Green = 0;
            patch.Density.Blue = 0;
            measurement.Patches = new List<MeasurementPatch>() { patch  };


            measurement.SyncRunId = 0;
            measurement.MeasurementDate = DateTime.Today;
            measurement.MeasurementName = "Mea Testing";
            measurement.MeasurementTypeId = 0;

            int actual;
            actual = target.SaveMeasurement(measurement);
            
            Assert.IsTrue(true);
            

        }

        /// <summary>
        ///A test for OptimizeDeviceLinkProfile
        ///</summary>
        [TestMethod()]
        public void OptimizeDeviceLinkProfileTest()
        {
            ISyncRunBusiness target = CreateISyncRunBusiness(); // TODO: Initialize to an appropriate value
            string measurementIds = string.Empty; // TODO: Initialize to an appropriate value
            int syncRunId = 29; // TODO: Initialize to an appropriate value
            int projectId = 26; // TODO: Initialize to an appropriate value

            DeviceLinkProfile actual;
            actual = target.OptimizeDeviceLinkProfile(measurementIds, syncRunId, projectId);
            Assert.IsTrue(true);


        }

        /// <summary>
        ///A test for DeleteMeasurement
        ///</summary>
        [TestMethod()]
        public void DeleteMeasurementTest()
        {
            ISyncRunBusiness target = CreateISyncRunBusiness(); // TODO: Initialize to an appropriate value
            int measurementId = 37; // TODO: Initialize to an appropriate value
            int actual;
            actual = target.DeleteMeasurement(measurementId);
            Assert.IsTrue(true);

        }

     

       
     


    }
}
