using System;
using System.Collections.Generic;
using System.Text;

namespace Koolwired.Imap
{
    using NUnit.Framework;

    [TestFixture]
    public class ImapAddressListTest
    {
        ImapAddressList list;
        ImapAddress _address1 = new ImapAddress("john@doe.com");
        ImapAddress _address2 = new ImapAddress("jane@doe.com", "Jane Doe");
        [SetUp]
        protected void Init()
        {
            list = new ImapAddressList();
            list.Add(_address1);
        }

        [Test]
        public void SingleAddress()
        {
            Assert.AreEqual(_address1.ToString(), list.ToString());
        }

        [Test]
        public void MultiAddress()
        {
            list.Add(_address2);
            Assert.AreEqual(string.Format("{0}, {1}", _address1.ToString(), _address2.ToString()), list.ToString());
        }
    }
}
