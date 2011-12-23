using FujifilmUSA.Sync.Business.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Implementation;

namespace FujifilmUSA.Sync.Business.Test
{
    
    
    /// <summary>
    ///This is a test class for IDestinationProfileBusinessTest and is intended
    ///to contain all IDestinationProfileBusinessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IDestinationProfileBusinessTest
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


        internal virtual IDestinationProfileBusiness CreateIDestinationProfileBusiness()
        {
            // TODO: Instantiate an appropriate concrete class.
            IDestinationProfileBusiness target = new DestinationProfileBusinessImpl();
            return target;
        }

        /// <summary>
        ///A test for GetDestinationProfileList
        ///</summary>
        [TestMethod()]
        public void GetDestinationProfileListTest()
        {
            IDestinationProfileBusiness target = CreateIDestinationProfileBusiness(); // TODO: Initialize to an appropriate value
            int syncDeviceId = 6; // TODO: Initialize to an appropriate value

            List<DestinationProfile> actual;
            actual = target.GetDestinationProfiles(syncDeviceId);

            Assert.IsTrue(true);

        }


        /// <summary>
        ///A test for InsertDestinationProfile
        ///</summary>
        [TestMethod()]
        public void InsertDestinationProfileTest()
        {
            IDestinationProfileBusiness target = CreateIDestinationProfileBusiness(); // TODO: Initialize to an appropriate value
            DestinationProfile objDestinationProfile = new DestinationProfile(); // TODO: Initialize to an appropriate value
            int actual;

            objDestinationProfile.DestinationProfileName = "Test Profile";
            objDestinationProfile.SyncDeviceId = 6;
            objDestinationProfile.IsUse = true;

            actual = target.InsertDestinationProfile(objDestinationProfile);
            Assert.IsTrue(true);

        }

        

        /// <summary>
        ///A test for UpdateDestinationProfile
        ///</summary>
        [TestMethod()]
        public void UpdateDestinationProfileTest()
        {
            IDestinationProfileBusiness target = CreateIDestinationProfileBusiness(); // TODO: Initialize to an appropriate value
            
            int destinationProfileId = 7; // TODO: Initialize to an appropriate value
            byte[] profile = null; // TODO: Initialize to an appropriate value
           
            target.UpdateDestinationProfile(destinationProfileId, profile);
           
            Assert.IsTrue(true);


        }

    }
}
