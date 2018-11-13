using System;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using NUnit.Framework;

namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactRemovalTests : TestBase
    {
        [Test]
        public void ContactRemovalTest()
        {
            OpenHomePage();
            Login(new AccountData("admin", "secret"));
            GoToContactsPage();
            SelectContact(1);
            RemoveContact();
            CloseAlert();
            ReturnToContactsPage();
            Logout();
        }
    }
}
