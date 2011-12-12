using FujifilmUSA.Sync.Business.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using FujifilmUSA.Sync.Business.Implementation;
using System.Linq;

namespace FujifilmUSA.Sync.Business.Test
{
    /// <summary>
    ///This is a test class for IBranchBusinessTest and is intended
    ///to contain all IBranchBusinessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IBranchBusinessTest
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


        internal virtual IBranchBusiness CreateIBranchBusiness()
        {

            // TODO: Instantiate an appropriate concrete class.
            IBranchBusiness target = new BranchBusinessImpl();
            return target;
        }

        /// <summary>
        ///A test for GetBranchList
        ///</summary>
        [TestMethod()]
        public void GetBranchListTest()
        {
            IBranchBusiness target = CreateIBranchBusiness(); // TODO: Initialize to an appropriate value
            int CompanyId = 1; // TODO: Initialize to an appropriate value            
            Dictionary<int, string> actual;
            actual = target.GetBranchList(CompanyId);
            Assert.IsTrue(actual != null);

        }

        /// <summary>
        ///A test for GetBranchList
        ///</summary>
        [TestMethod()]
        public void GetBranchListTest1()
        {
            IBranchBusiness target = CreateIBranchBusiness(); // TODO: Initialize to an appropriate value
            int CompanyId = 0; // TODO: Initialize to an appropriate value
            Dictionary<int, string> expected = null; // TODO: Initialize to an appropriate value
            Dictionary<int, string> actual;
            actual = target.GetBranchList(CompanyId);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
