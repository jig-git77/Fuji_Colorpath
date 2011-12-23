using FujifilmUSA.Sync.Business.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FujifilmUSA.Sync.Entity;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Implementation;

namespace FujifilmUSA.Sync.Business.Test
{
    
    
    /// <summary>
    ///This is a test class for IMeasurementDeviceBusinessTest and is intended
    ///to contain all IMeasurementDeviceBusinessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IMeasurementDeviceBusinessTest
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


        internal virtual IMeasurementDeviceBusiness CreateIMeasurementDeviceBusiness()
        {
            // TODO: Instantiate an appropriate concrete class.
            IMeasurementDeviceBusiness target = new MeasurementDeviceBusinessImpl();
            return target;
        }

       
        /// <summary>
        ///A test for GetSyncMeasurementDeviceByMeasurementDeviceId
        ///</summary>
        [TestMethod()]
        public void GetSyncMeasurementDeviceByMeasurementDeviceIdTest()
        {
            IMeasurementDeviceBusiness target = CreateIMeasurementDeviceBusiness(); // TODO: Initialize to an appropriate value
            int measurementDeviceId = 24; // TODO: Initialize to an appropriate value
        
            MeasurementDevice actual;
            actual = target.GetSyncMeasurementDevice(measurementDeviceId);
            Assert.IsTrue(true);


        }

        /// <summary>
        ///A test for GetSyncMeasurementDeviceList
        ///</summary>
        [TestMethod()]
        public void GetSyncMeasurementDeviceListTest()
        {
            IMeasurementDeviceBusiness target = CreateIMeasurementDeviceBusiness(); // TODO: Initialize to an appropriate value
        
            List<MeasurementDevice> actual;
            actual = target.GetSyncMeasurementDevices();
            Assert.IsTrue(true);



        }

        /// <summary>
        ///A test for InsertSyncMeasurementDevice
        ///</summary>
        [TestMethod()]
        public void InsertSyncMeasurementDeviceTest()
        {
            IMeasurementDeviceBusiness target = CreateIMeasurementDeviceBusiness(); // TODO: Initialize to an appropriate value
            MeasurementDevice measurementDevice = new MeasurementDevice(); // TODO: Initialize to an appropriate value
            measurementDevice.MeasurementDeviceTypeDetail = new MeasurementDeviceType();
            measurementDevice.CPDongleId = 308;
            measurementDevice.SerialNumber = "";
            measurementDevice.MeasurementDeviceTypeDetail.DeviceTypeId = 1;
            measurementDevice.IsActive = true;

            int actual;
            actual = target.InsertSyncMeasurementDevice(measurementDevice);
            Assert.IsTrue(true);

        }

        /// <summary>
        ///A test for UpdateSyncMeasurementDevice
        ///</summary>
        [TestMethod()]
        public void UpdateSyncMeasurementDeviceTest()
        {
            IMeasurementDeviceBusiness target = CreateIMeasurementDeviceBusiness(); // TODO: Initialize to an appropriate value
            MeasurementDevice measurementDevice = new MeasurementDevice(); // TODO: Initialize to an appropriate value
            measurementDevice.MeasurementDeviceTypeDetail = new MeasurementDeviceType();
            
            measurementDevice.MeasurementDeviceId =27;
            measurementDevice.CPDongleId = 308;
            measurementDevice.SerialNumber= "Update Testing";
            measurementDevice.MeasurementDeviceTypeDetail.DeviceTypeId=1;
            measurementDevice.IsActive = true;

            int actual;
            actual = target.UpdateSyncMeasurementDevice(measurementDevice);
            Assert.IsTrue(true);



        }

    

        /// <summary>
        ///A test for DeleteSyncMeasurementDevice
        ///</summary>
        [TestMethod()]
        public void DeleteSyncMeasurementDeviceTest()
        {
            IMeasurementDeviceBusiness target = CreateIMeasurementDeviceBusiness(); // TODO: Initialize to an appropriate value
            int syncMeasurementDeviceId = 27; // TODO: Initialize to an appropriate value
            bool actual;
            actual = target.DeleteSyncMeasurementDevice(syncMeasurementDeviceId);
            Assert.IsTrue(actual);
        }
    }
}
