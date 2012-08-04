
#region Copyright (c) Koolwired Solutions, LLC.
/*--------------------------------------------------------------------------
 * Copyright (c) 2006, Koolwired Solutions, LLC.
 * All rights reserved.
 *
 * Redistribution and use in source and binary forms, with or without modification,
 * are permitted provided that the following conditions are met:
 *
 * Redistributions of source code must retain the above copyright notice,
 * this list of conditions and the following disclaimer. 
 * Redistributions in binary form must reproduce the above copyright
 * notice, this list of conditions and the following disclaimer in the
 * documentation and/or other materials provided with the distribution. 
 * Neither the name of Koolwired Solutions, LLC. nor the names of its
 * contributors may be used to endorse or promote products derived from
 * this software without specific prior written permission. 
 *
 * THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS
 * AND CONTRIBUTORS "AS IS" AND ANY EXPRESS OR IMPLIED
 * WARRANTIES, INCLUDING, BUT NOT LIMITED TO, THE IMPLIED
 * WARRANTIES OF MERCHANTABILITY AND FITNESS FOR A 
 * PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL
 * THE COPYRIGHT OWNER OR CONTRIBUTORS BE LIABLE FOR
 * ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL, EXEMPLARY,
 * OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED
 * TO, PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS
 * OF USE, DATA, OR PROFITS; OR BUSINESS INTERRUPTION)
 * HOWEVER CAUSED AND ON ANY THEORY OF LIABILITY,
 * WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT
 * (INCLUDING NEGLIGENCE OR OTHERWISE) ARISING IN ANY
 * WAY OUT OF THE USE OF THIS SOFTWARE, EVEN IF ADVISED
 * OF THE POSSIBILITY OF SUCH DAMAGE.
 *--------------------------------------------------------------------------*/
#endregion

#region History
/*--------------------------------------------------------------------------
 * Modification History: 
 * Date       Programmer              Description
 * 07/24/07   Scott A. Braithwaite    New test case
 *--------------------------------------------------------------------------*/
#endregion

using NUnit.Framework;
using Koolwired.Imap.Test;
using System;

namespace Koolwired.Imap
{
    [TestFixture]
    public class ImapFullConnectionTest
    {
        ImapConnect _connection;
        ImapAuthenticate _authentication;
        ImapCommand _command;
        ImapMailbox _mailbox;
        string _stdhost = "localhost";
        string _stduser = "email@address.com";
        string _stdpass = "password";
        string _sslhost = "imap.gmail.com";
        string _ssluser = "email@gmail.com";
        string _sslpass = "password";

        [TestFixtureSetUp]
        public void Init()
        {
            ConnectionGUI settings = new ConnectionGUI();
            if (settings.Status)
            {
                _stdhost = settings.StandardHost;
                _stduser = settings.StandardUser;
                _stdpass = settings.StandardPass;
                _sslhost = settings.SslHost;
                _ssluser = settings.SslUser;
                _sslpass = settings.SslPass;
            }
        }

        [Test]
        public void HmacMd5Test()
        {
            //The test case from the http://www.ietf.org/rfc/rfc2195.txt spec
            string server = "+ PDE4OTYuNjk3MTcwOTUyQHBvc3RvZmZpY2UucmVzdG9uLm1jaS5uZXQ+";
            string username = "tim";
            string password = "tanstaaftanstaaf";
            string response = ImapAuthenticate.HmacMd5(username, password, server);
            Assert.IsTrue(String.Compare(response, "dGltIGI5MTNhNjAyYzdlZGE3YTQ5NWI0ZTZlNzMzNGQzODkw") == 0);
        }

        [Test]
        public void ImapFullPlainTest()
        {
            _connection = new ImapConnect(_stdhost);
            _authentication = new ImapAuthenticate(_connection, _stduser, _stdpass);
            _command = new ImapCommand(_connection);

            Assert.IsTrue(_connection.Open());
            Assert.IsTrue(_connection.State == ConnectionState.Connected);
            _connection.LoginType = LoginType.PLAIN;
            _authentication.Login();
            Assert.IsTrue(_connection.State == ConnectionState.Open);
            _mailbox = _command.Examine("Inbox");
            Assert.IsTrue(_mailbox.Exist != 0);
            _authentication.Logout();
            Assert.IsTrue(_connection.State == ConnectionState.Closed);
            _connection.Close();
            _connection.Dispose();
        }

        [Test]
        public void ImapFullCramMd5Test()
        {
            _connection = new ImapConnect(_stdhost);
            _authentication = new ImapAuthenticate(_connection, _stduser, _stdpass);
            _command = new ImapCommand(_connection);
            _connection.LoginType = LoginType.CRAM_MD5;
            Assert.IsTrue(_connection.Open());
            Assert.IsTrue(_connection.State == ConnectionState.Connected);
            _authentication.Login();
            Assert.IsTrue(_connection.State == ConnectionState.Open);
            _mailbox = _command.Examine("Inbox");
            Assert.IsTrue(_mailbox.Exist != 0);
            _authentication.Logout();
            Assert.IsTrue(_connection.State == ConnectionState.Closed);
            _connection.Close();
            _connection.Dispose();
        }
        [Test]
        public void ImapFullSslPlainTest()
        {
            _connection = new ImapConnect(_sslhost, 993, true);
            _authentication = new ImapAuthenticate(_connection, _ssluser, _sslpass);
            _command = new ImapCommand(_connection);
            Assert.IsTrue(_connection.Open());
            Assert.IsTrue(_connection.State == ConnectionState.Connected);
            _connection.LoginType = LoginType.PLAIN;
            _authentication.Login();
            Assert.IsTrue(_connection.State == ConnectionState.Open);
            _mailbox = _command.Examine("Inbox");
            Assert.IsTrue(_mailbox.Exist != 0);
            _authentication.Logout();
            Assert.IsTrue(_connection.State == ConnectionState.Closed);
            _connection.Close();
            _connection.Dispose();
        }
    }
}
