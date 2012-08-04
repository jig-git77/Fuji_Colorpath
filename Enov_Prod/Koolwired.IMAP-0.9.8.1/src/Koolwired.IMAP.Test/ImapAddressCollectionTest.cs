using System;
using System.Collections.Generic;
using System.Text;

namespace Koolwired.Imap
{
    using NUnit.Framework;

    [TestFixture]
    public class ImapAddressCollectionTest
    {
        ImapAddressCollection collection;
        ImapAddress _address = new ImapAddress("john@doe.com");
        ImapAddressList _addresslist = new ImapAddressList();

        [SetUp]
        protected void Init()
        {
            collection = new ImapAddressCollection();
            collection.From = _address;
            collection.Sender = _address;
            collection.ReplyTo = _address;
            collection.To.Add(_address);
            collection.CC.Add(_address);
            collection.BCC.Add(_address);
        }
        [Test]
        public void From()
        {
            Assert.AreEqual(collection.From, _address);
        }
        [Test]
        public void Sender()
        {
            Assert.AreEqual(collection.Sender, _address);
        }
        [Test]
        public void ReplyTo()
        {
            Assert.AreEqual(collection.ReplyTo, _address);
        }
        [Test]
        public void To()
        {
            Assert.AreEqual(collection.To[0], _address);
        }
        [Test]
        public void CC()
        {
            Assert.AreEqual(collection.CC[0], _address);
        }
        [Test]
        public void BCC()
        {
            Assert.AreEqual(collection.BCC[0], _address);
        }
    }
}
