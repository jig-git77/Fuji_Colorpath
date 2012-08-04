using System;
using System.Text;
using Koolwired.Imap;

namespace Koolwired.Imap
{
    using NUnit.Framework;

    [TestFixture]
    public class ImapAddressTest
    {
        ImapAddress address1;
        ImapAddress address2;
        ImapAddress address3;
        string _emailaddress = "john@doe.com";
        string _name = "John Doe";
        string _invalidaddress = "johndoe@";
        Encoding _encoding = Encoding.ASCII;

        [SetUp]
        protected void Init()
        {
            address1 = new ImapAddress(_emailaddress);
            address2 = new ImapAddress(_emailaddress, _name);
            address3 = new ImapAddress(_emailaddress, _name, _encoding);
        }
        [Test]
        public void CreateSimpleAddresses()
        {
            Assert.AreEqual(address1.Address, _emailaddress);
        }
        [Test]
        public void CreateNormalAddress()
        {
            Assert.AreEqual(address2.Address, _emailaddress);
            Assert.AreEqual(address2.DisplayName, _name);
        }
        [Test]
        public void CreateComplexAddress()
        {
            Assert.AreEqual(address3.Address, _emailaddress);
            Assert.AreEqual(address3.DisplayName, _name);
        }
        [Test]
        public void ToStringSimple()
        {
            Assert.AreEqual(address1.ToString(), _emailaddress, string.Format("{0} != {1}", address1.ToString(), _emailaddress));
        }
        [Test]
        public void ToStringNormal()
        {
            Assert.AreEqual(address2.ToString(), string.Format("\"{0}\" <{1}>", _name, _emailaddress));
        }
        [Test]
        public void ToStringComplex()
        {
            Assert.AreEqual(address3.ToString(), string.Format("\"{0}\" <{1}>", _name, _emailaddress));
        }
        [Test]
        public void ToMailAddressSimple()
        {
            Assert.AreEqual(address1.ToMailAddress(), new System.Net.Mail.MailAddress(_emailaddress));
        }
        [Test]
        public void ToMailAddressNormal()
        {
            Assert.AreEqual(address2.ToMailAddress(), new System.Net.Mail.MailAddress(_emailaddress, _name));
        }
        [Test]
        public void ToMailAddressComplex()
        {
            Assert.AreEqual(address3.ToMailAddress(), new System.Net.Mail.MailAddress(_emailaddress, _name, _encoding));
        }
        [Test]
        [ExpectedException(typeof(FormatException))]
        public void FormatExceptionAddressSimple()
        {
            ImapAddress address = new ImapAddress(_invalidaddress);
        }
        [Test]
        [ExpectedException(typeof(FormatException))]
        public void FormatExceptionAddressNormal()
        {
            ImapAddress address = new ImapAddress(_invalidaddress, _name);
        }
        [Test]
        [ExpectedException(typeof(FormatException))]
        public void FormatExceptionAddressComplex()
        {
            ImapAddress address = new ImapAddress(_invalidaddress, _name, _encoding);
        }
    }
}
