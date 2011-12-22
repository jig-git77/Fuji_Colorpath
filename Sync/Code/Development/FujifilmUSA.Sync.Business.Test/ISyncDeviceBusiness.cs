using FujifilmUSA.Sync.Business.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Implementation;

namespace FujifilmUSA.Sync.Business.Test
{
    
    
    /// <summary>
    ///This is a test class for ISyncDeviceBusinessTest and is intended
    ///to contain all ISyncDeviceBusinessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ISyncDeviceBusinessTest
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


        internal virtual ISyncDeviceBusiness CreateISyncDeviceBusiness()
        {
            // TODO: Instantiate an appropriate concrete class.
            ISyncDeviceBusiness target = new SyncDeviceBusinessImpl();
            return target;
        }

        /// <summary>
        ///A test for GetSyncDeviceListByUserId
        ///</summary>
        [TestMethod()]
        public void GetSyncDeviceListByUserIdTest()
        {
            ISyncDeviceBusiness target = CreateISyncDeviceBusiness(); // TODO: Initialize to an appropriate value
            int taskeroUserId = 6; // TODO: Initialize to an appropriate value
            int roleId = 1; // TODO: Initialize to an appropriate value
            int companyId = 1; // TODO: Initialize to an appropriate value
            int branchId = 1; // TODO: Initialize to an appropriate value
            
            List<SyncDevice> actual;
            actual = target.GetSyncDeviceListByUserId(taskeroUserId, roleId, companyId, branchId);
            Assert.IsTrue(true);



        }
    }
}
