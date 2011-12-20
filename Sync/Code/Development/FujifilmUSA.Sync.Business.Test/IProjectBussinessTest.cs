using FujifilmUSA.Sync.Business.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Implementation;

namespace FujifilmUSA.Sync.Business.Test
{
    
    
    /// <summary>
    ///This is a test class for IProjectBussinessTest and is intended
    ///to contain all IProjectBussinessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IProjectBussinessTest
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


        internal virtual IProjectBussiness CreateIProjectBussiness()
        {
            // TODO: Instantiate an appropriate concrete class.
            IProjectBussiness target = new ProjectBussinessImpl();
            return target;
        }

        /// <summary>
        ///A test for InsertProjectAndMeasurementSetting
        ///</summary>
        [TestMethod()]
        public void InsertProjectAndMeasurementSettingTest()
        {
            IProjectBussiness target = CreateIProjectBussiness(); // TODO: Initialize to an appropriate value
            Project objProject = new Project();
            MeasurementSetting objMeasurementSetting = new MeasurementSetting();

            objProject.ProjectName = "TestByAjay";
            objProject.SyncDeviceId = 1;
            objProject.ΔEType = 1;
            objProject.DeviceLinkProfileId = 4;
            objProject.IsUsed = true;
            objProject.UseDLP = true;
            objProject.OutputTypeId = 1;
            objProject.StartDate = DateTime.Now;
            objProject.ProjectTypeId = 1;
            objProject.ReTargetId = 1;

            objMeasurementSetting.UsePlugin = true;
            objMeasurementSetting.Observer = 1;
            objMeasurementSetting.Illuminant= 1;
            objMeasurementSetting.DensityType = 1;
            objMeasurementSetting.UVIncluded = true;
            objMeasurementSetting.ReferenceChartTypeId = 1;
            objMeasurementSetting.IlluminationMode = 1;
            objMeasurementSetting.InstrumentTypeId = 1;
            objMeasurementSetting.SerialNumber = "Ajay";

            
            Project actual;
            actual = target.InsertProjectAndMeasurementSetting(objProject, objMeasurementSetting);
            Assert.IsTrue(actual != null);
        }

        /// <summary>
        ///A test for GetProjectListBySyncDeviceId
        ///</summary>
        [TestMethod()]
        public void GetProjectListBySyncDeviceIdTest()
        {
            IProjectBussiness target = CreateIProjectBussiness(); // TODO: Initialize to an appropriate value
            int syncDeviceId = 0; // TODO: Initialize to an appropriate value
            List<Project> expected = null; // TODO: Initialize to an appropriate value
            List<Project> actual;
            actual = target.GetProjectListBySyncDeviceId(syncDeviceId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
