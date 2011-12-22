using FujifilmUSA.Sync.Business.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Implementation;

namespace FujifilmUSA.Sync.Business.Test
{
    
    
    /// <summary>
    ///This is a test class for IDeviceLinkProfileBusinessTest and is intended
    ///to contain all IDeviceLinkProfileBusinessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IDeviceLinkProfileBusinessTest
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


        internal virtual IDeviceLinkProfileBusiness CreateIDeviceLinkProfileBusiness()
        {
            // TODO: Instantiate an appropriate concrete class.
            IDeviceLinkProfileBusiness target = new DeviceLinkProfileBusinessImpl();
            return target;
        }

        /// <summary>
        ///A test for GetDeviceLinkProfileList
        ///</summary>
        [TestMethod()]
        public void GetDeviceLinkProfileListTest()
        {
            IDeviceLinkProfileBusiness target = CreateIDeviceLinkProfileBusiness(); // TODO: Initialize to an appropriate value
            int sourceProfileId = 9; // TODO: Initialize to an appropriate value
            int destinationProfileId = 4; // TODO: Initialize to an appropriate value
            List<DeviceLinkProfile> actual;

            actual = target.GetDeviceLinkProfileList(sourceProfileId, destinationProfileId);

            Assert.IsTrue(true);

        }

        /// <summary>
        ///A test for GetDeviceLinkProfileListOnSyncDeviceId
        ///</summary>
        [TestMethod()]
        public void GetDeviceLinkProfileListOnSyncDeviceIdTest()
        {
            IDeviceLinkProfileBusiness target = CreateIDeviceLinkProfileBusiness(); // TODO: Initialize to an appropriate value
            int SyncDeviceId = 6; // TODO: Initialize to an appropriate value

            List<DeviceLinkProfile> actual;
            actual = target.GetDeviceLinkProfileListOnSyncDeviceId(SyncDeviceId);
            
            Assert.IsTrue(true);


        }

        /// <summary>
        ///A test for InsertDeviceLinkProfile
        ///</summary>
        [TestMethod()]
        public void InsertDeviceLinkProfileTest()
        {
            IDeviceLinkProfileBusiness target = CreateIDeviceLinkProfileBusiness(); // TODO: Initialize to an appropriate value
            DeviceLinkProfile objDeviceLinkProfile = new DeviceLinkProfile(); // TODO: Initialize to an appropriate value

            objDeviceLinkProfile.SrcProfile = new SourceProfile();
            objDeviceLinkProfile.DestProfile = new DestinationProfile();

            objDeviceLinkProfile.SrcProfile.SourceProfileId = 9;
            objDeviceLinkProfile.DestProfile.DestinationProfileId = 4;
            objDeviceLinkProfile.DeviceLinkProfileName = "Test DLP";
            objDeviceLinkProfile.IsUse = true;

            int actual;
            actual = target.InsertDeviceLinkProfile(objDeviceLinkProfile);

            Assert.IsTrue(true);

        }

        /// <summary>
        ///A test for UpdateDestinationProfile
        ///</summary>
        [TestMethod()]
        public void UpdateDestinationProfileTest()
        {
            IDeviceLinkProfileBusiness target = CreateIDeviceLinkProfileBusiness(); // TODO: Initialize to an appropriate value
            int deviceLinkProfileId = 5; // TODO: Initialize to an appropriate value
            byte[] profile = null; // TODO: Initialize to an appropriate value
            target.UpdateDestinationProfile(deviceLinkProfileId, profile);
            Assert.IsTrue(true);
        }
    }
}
