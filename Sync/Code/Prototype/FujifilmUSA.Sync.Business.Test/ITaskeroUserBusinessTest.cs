using FujifilmUSA.Sync.Business.Interface;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using FujifilmUSA.Sync.Entity;
using FujifilmUSA.Sync.Business.Implementation;

namespace FujifilmUSA.Sync.Business.Test
{
    
    
    /// <summary>
    ///This is a test class for ITaskeroUserBusinessTest and is intended
    ///to contain all ITaskeroUserBusinessTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ITaskeroUserBusinessTest
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


        internal virtual ITaskeroUserBusiness CreateITaskeroUserBusiness()
        {
            // TODO: Instantiate an appropriate concrete class.
            ITaskeroUserBusiness target = new TaskeroUserBusinessImpl(); 
            return target;
        }

        /// <summary>
        ///A test for UserLogin
        ///</summary>
        [TestMethod()]
        public void UserLoginTest()
        {
            ITaskeroUserBusiness target = CreateITaskeroUserBusiness(); // TODO: Initialize to an appropriate value
            string UserName = "rinkprinting"; // TODO: Initialize to an appropriate value
            string Password = "fuji"; // TODO: Initialize to an appropriate value
            string AllowedIP = string.Empty; // TODO: Initialize to an appropriate value            
            TaskeroUser actual;
            actual = target.UserLogin(UserName, Password, AllowedIP);
            Assert.IsTrue(actual != null);
        }
    }
}
