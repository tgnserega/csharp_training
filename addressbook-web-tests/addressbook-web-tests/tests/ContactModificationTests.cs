using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;


namespace WebAddressbookTests
{
    [TestFixture]
    public class ContactModificationTests : ContactTestBase
    {
        [Test]
        public void ContactModificationTest()
        {
            var a = new Random().Next(1, 13000);
            ContactData newData = (new ContactData("Ivanov" + a, "Ivan" + a));
            newData.Mobilephone = null;
            newData.Homephone = null;
            newData.Workphone = null;
            newData.Email = null;
            newData.Email2 = null;
            newData.Email3 = null;

            app.Contacts.GreateContactIfNotExist();

            List<ContactData> oldContacts = ContactData.GetAll();
            ContactData toBeModified = oldContacts[0];

            app.Contacts.Modify(toBeModified, newData);
            app.Navigator.GoToContactsPage();

            Assert.AreEqual(oldContacts.Count, app.Contacts.GetContactCount());

            List<ContactData> newContacts = ContactData.GetAll();
            oldContacts[0].Lastname = newData.Lastname;
            oldContacts[0].Firstname = newData.Firstname;
            oldContacts.Sort();
            newContacts.Sort();
            Assert.AreEqual(oldContacts, newContacts);

            foreach (ContactData contact in newContacts)
            {
                if (contact.Id == toBeModified.Id)
                {

                    Assert.AreEqual(newData.Firstname, toBeModified.Firstname);
                }
            }
        }
    }
}
