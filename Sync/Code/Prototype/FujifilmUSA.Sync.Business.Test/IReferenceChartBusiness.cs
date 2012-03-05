using FujifilmUSA.Sync.Business.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Implementation;

namespace FujifilmUSA.Sync.Business.Test
{
    
    
    /// <summary>
    ///This is a test class for IReferenceChartBusinessTest and is intended
    ///to contain all IReferenceChartBusinessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IReferenceChartBusinessTest
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


        internal virtual IReferenceChartBusiness CreateIReferenceChartBusiness()
        {
            // TODO: Instantiate an appropriate concrete class.
            IReferenceChartBusiness target = new ReferenceChartBusinessImpl();
            return target;
        }

        /// <summary>
        ///A test for GetReferenceChartData
        ///</summary>
        [TestMethod()]
        public void GetReferenceChartDataTest()
        {
            IReferenceChartBusiness target = CreateIReferenceChartBusiness(); // TODO: Initialize to an appropriate value
            int projectId = 26; // TODO: Initialize to an appropriate value
            
            ReferenceChartData actual;
            actual = target.GetReferenceChartData(projectId);
            Assert.IsTrue(true);
            
        }

        /// <summary>
        ///A test for GetReferenceChartDataByTypeId
        ///</summary>
        [TestMethod()]
        public void GetReferenceChartDataByTypeIdTest()
        {
            IReferenceChartBusiness target = CreateIReferenceChartBusiness(); // TODO: Initialize to an appropriate value
            int referenceChartTypeId = 5; // TODO: Initialize to an appropriate value
            string serialNo = string.Empty; // TODO: Initialize to an appropriate value
            
            ReferenceChartData actual;
            actual = target.GetReferenceChartDataByTypeId(referenceChartTypeId, serialNo);
            Assert.IsTrue(true);

        }
    }
}
