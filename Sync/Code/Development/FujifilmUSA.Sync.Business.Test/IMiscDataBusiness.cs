using FujifilmUSA.Sync.Business.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Implementation;

namespace FujifilmUSA.Sync.Business.Test
{
    
    
    /// <summary>
    ///This is a test class for IMiscDataBusinessTest and is intended
    ///to contain all IMiscDataBusinessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IMiscDataBusinessTest
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


        internal virtual IMiscDataBusiness CreateIMiscDataBusiness()
        {
            // TODO: Instantiate an appropriate concrete class.
            IMiscDataBusiness target = new MiscDataBusinessImpl();
            return target;
        }

        /// <summary>
        ///A test for GetBranchList
        ///</summary>
        [TestMethod()]
        public void GetBranchListTest()
        {
            IMiscDataBusiness target = CreateIMiscDataBusiness(); // TODO: Initialize to an appropriate value
            int companyId = 1; // TODO: Initialize to an appropriate value
            Dictionary<int, string> actual;
            
            actual = target.GetBranchList(companyId);
            Assert.IsTrue(true);

        }

        /// <summary>
        ///A test for GetCompanyList
        ///</summary>
        [TestMethod()]
        public void GetCompanyListTest()
        {
            IMiscDataBusiness target = CreateIMiscDataBusiness(); // TODO: Initialize to an appropriate value
            
            Dictionary<int, string> actual;
            actual = target.GetCompanyList();
            
            Assert.IsTrue(true);


        }

        /// <summary>
        ///A test for GetDestinationProfiles
        ///</summary>
        [TestMethod()]
        public void GetDestinationProfilesTest()
        {
            IMiscDataBusiness target = CreateIMiscDataBusiness(); // TODO: Initialize to an appropriate value
            int syncDeviceId = 6; // TODO: Initialize to an appropriate value

            Dictionary<int, string> actual;
            actual = target.GetDestinationProfiles(syncDeviceId);
            Assert.IsTrue(true);
            

        }

        /// <summary>
        ///A test for GetDongleList
        ///</summary>
        [TestMethod()]
        public void GetDongleListTest()
        {
            IMiscDataBusiness target = CreateIMiscDataBusiness(); // TODO: Initialize to an appropriate value
            int branchId = 1; // TODO: Initialize to an appropriate value
            
            Dictionary<int, string> actual;
            actual = target.GetDongleList(branchId);
            Assert.IsTrue(true);

            
        }

        /// <summary>
        ///A test for GetMeasurementDeviceListBySyncDeviceId
        ///</summary>
        [TestMethod()]
        public void GetMeasurementDeviceListBySyncDeviceIdTest()
        {
            IMiscDataBusiness target = CreateIMiscDataBusiness(); // TODO: Initialize to an appropriate value
            int syncDeviceId = 6; // TODO: Initialize to an appropriate value
            
            Dictionary<string, string> actual;
            actual = target.GetMeasurementDeviceListBySyncDeviceId(syncDeviceId);
            Assert.IsTrue(true);
            

        }

        /// <summary>
        ///A test for GetMeasurementDeviceTypeList
        ///</summary>
        [TestMethod()]
        public void GetMeasurementDeviceTypeListTest()
        {
            IMiscDataBusiness target = CreateIMiscDataBusiness(); // TODO: Initialize to an appropriate value
            
            Dictionary<int, string> actual;
            actual = target.GetMeasurementDeviceTypeList();
            Assert.IsTrue(true);
            

        }

        /// <summary>
        ///A test for GetReferenceChartTypeListByMeasurementDeviceId
        ///</summary>
        [TestMethod()]
        public void GetReferenceChartTypeListByMeasurementDeviceIdTest()
        {
            IMiscDataBusiness target = CreateIMiscDataBusiness(); // TODO: Initialize to an appropriate value
            int measurementDeviceId = 24; // TODO: Initialize to an appropriate value
            
            Dictionary<int, string> actual;
            actual = target.GetReferenceChartTypeListByMeasurementDeviceId(measurementDeviceId);
            Assert.IsTrue(true);
            

        }

        /// <summary>
        ///A test for GetSourceProfileList
        ///</summary>
        [TestMethod()]
        public void GetSourceProfileListTest()
        {
            IMiscDataBusiness target = CreateIMiscDataBusiness(); // TODO: Initialize to an appropriate value
            Dictionary<int, string> actual;
            actual = target.GetSourceProfileList();
            Assert.IsTrue(true);

        }
    }
}
