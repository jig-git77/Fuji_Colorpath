using System;
using System.Collections.Generic;
using System.Text;
using Koolwired.Imap.Test;

namespace Koolwired.Imap
{
    using NUnit.Framework;
    [TestFixture]
    public class ImapMailboxTest
    {
        #region Private Variables
        string _host;
        string _user;
        string _pass;
        int _port;
        bool _ssl;
        #endregion

        [TestFixtureSetUp]
        public void Init()
        {
            MailboxGUI settings = new MailboxGUI();
            _host = settings.Host;
            _user = settings.User;
            _pass = settings.Pass;
            _port = settings.Port;
            _ssl = settings.SSL;
        }
        [Test]
        public void EntireInbox()
        {
            try
            {
                ImapConnect connection = new ImapConnect(_host, _port, _ssl);
                ImapCommand command = new ImapCommand(connection);
                ImapAuthenticate authenticate = new ImapAuthenticate(connection, _user, _pass);
                connection.Open();
                authenticate.Login();
                ImapMailbox mailbox = command.Select("INBOX");
                mailbox = command.Fetch(mailbox);
                authenticate.Logout();
                connection.Close();
                Assert.That(true);
            }
            catch (Exception)
            {
                Assert.That(false);
            }
        }
        [Test]
        public void CheckBodyStructure()
        {
            ImapConnect connection = new ImapConnect(_host, _port, _ssl);
            ImapCommand command = new ImapCommand(connection);
            ImapAuthenticate authenticate = new ImapAuthenticate(connection, _user, _pass);
            connection.Open();
            authenticate.Login();
            ImapMailbox mailbox = command.Select("INBOX");
            ImapMailboxMessage message = new ImapMailboxMessage();
            message.ID = 1;
            message = command.FetchBodyStructure(message);
            authenticate.Logout();
            connection.Close();
        }
        [Test]
        public void CheckList()
        {
            ImapConnect connection = new ImapConnect(_host, _port, _ssl);
            ImapCommand command = new ImapCommand(connection);
            ImapAuthenticate authenticate = new ImapAuthenticate(connection, _user, _pass);
            connection.Open();
            authenticate.Login();
            command.List();
            authenticate.Logout();
            connection.Close();
        }
    }
}
