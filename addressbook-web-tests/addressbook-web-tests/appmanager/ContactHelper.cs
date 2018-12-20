using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;
using System.Text.RegularExpressions;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }

        public ContactData GetContactInformationFromTable(int index)
        {
            manager.Navigator.GoToContactsPage();
            IList<IWebElement> cells = driver.FindElements(By.Name("entry"))[index].FindElements(By.TagName("td"));
            string lastName = cells[1].Text;
            string firstName = cells[2].Text;
            string allEmails = cells[4].Text;
            string allPhones = cells[5].Text;

            return new ContactData(lastName, firstName)
            {

                AllEmails = allEmails,
                AllPhones = allPhones

            };
        }

        public void RemoveContactFromGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToContactsPage();
            SelectGroupFromFilter(group.Name);
            SelectContact(contact.Id);
            CommitRemoveContactFromGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void CommitRemoveContactFromGroup()
        {
            driver.FindElement(By.Name("remove")).Click();
        }

        public void SelectGroupFromFilter(string name)
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText(name);
        }

        public void AddContactToGroup(ContactData contact, GroupData group)
        {
            manager.Navigator.GoToContactsPage();
            ClearGroupFilter();
            SelectContact(contact.Id);
            SelectGroupToAdd(group.Name);
            CommitAddingContactToGroup();
            new WebDriverWait(driver, TimeSpan.FromSeconds(10)).Until(d => d.FindElements(By.CssSelector("div.msgbox")).Count > 0);
        }

        public void CommitAddingContactToGroup()
        {
            driver.FindElement(By.Name("add")).Click();
        }

        public void SelectGroupToAdd(string name)
        {
            new SelectElement(driver.FindElement(By.Name("to_group"))).SelectByText(name);
        }

        public void ClearGroupFilter()
        {
            new SelectElement(driver.FindElement(By.Name("group"))).SelectByText("[all]");
        }

        public ContactHelper Modify(ContactData newData)
        {
            manager.Navigator.GoToContactsPage();
            InitContactModification();
            FillContactForm(newData);
            SubmitContactModification();
            return this;
        }

        public ContactHelper Modify(ContactData contact, ContactData newData)
        {
            manager.Navigator.GoToContactsPage();
            InitContactModification(contact.Id);
            FillContactForm(newData);
            SubmitContactModification();
            return this;
        }

        public ContactHelper InitContactModification(String id)
        {
            driver.FindElement(By.XPath($"//a[@href='edit.php?id={id}']")).Click();
            return this;
        }

        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.XPath("//*[@id='maintable']/tbody/tr[2]/td[8]/a/img")).Click();
            return this;
        }


        public ContactHelper Remove()
        {
            manager.Navigator.GoToContactsPage();
            SelectContact();
            RemoveContact();
            CloseAlert();
            manager.Navigator.ReturnToContactsPage();
            return this;
        }

        public ContactHelper Remove(ContactData contact)
        {
            manager.Navigator.GoToContactsPage();
            SelectContact(contact.Id);
            RemoveContact();
            CloseAlert();
            manager.Navigator.ReturnToContactsPage();
            return this;
        }

        public ContactHelper SelectContact()
        {
            driver.FindElement(By.Name("selected[]")).Click();
            return this;
        }

        public ContactHelper SelectContact(String id)
        {
            //driver.FindElement(By.XPath("(//*[@id='maintable']/tbody/tr[2]/td[1]/input[@name='selected[]' and @value='" + id + "'])")).Click();
            //driver.FindElement(By.XPath("(//*[@id='maintable']/tbody/tr[2]/td[1]/input and @value='" + id + "')")).Click();
            driver.FindElement(By.Id(id)).Click();
            return this;
        }

        public string GetContactInformationFromPropertiesPage(int i)
        {

            manager.Navigator.GoToContactsPage();
            OpenContactPropertiesPage(i);

            string contactPropertiesPageText = driver.FindElement(By.CssSelector("#content")).Text;

            contactPropertiesPageText = Regex.Replace(contactPropertiesPageText, "[ \r\n]", "").Trim();

            return contactPropertiesPageText;
        }

        public ContactHelper OpenContactPropertiesPage(int i)
        {

            driver.FindElements(By.Name("entry"))[i].FindElements(By.TagName("td"))[6].FindElement(By.TagName("a")).Click();
            return this;
        }

        public ContactData GetContactInformationFromEditForm(int index)
        {
            manager.Navigator.GoToContactsPage();
            InitContactModification();
            string lastName = driver.FindElement(By.Name("lastname")).GetAttribute("value");
            string firstName = driver.FindElement(By.Name("firstname")).GetAttribute("value");
            string address = driver.FindElement(By.Name("address")).GetAttribute("value");
            string mobilePhone = driver.FindElement(By.Name("mobile")).GetAttribute("value");
            string homePhone = driver.FindElement(By.Name("home")).GetAttribute("value");
            string workPhone = driver.FindElement(By.Name("work")).GetAttribute("value");
            string email = driver.FindElement(By.Name("email")).GetAttribute("value");
            string email2 = driver.FindElement(By.Name("email2")).GetAttribute("value");
            string email3 = driver.FindElement(By.Name("email3")).GetAttribute("value");

            return new ContactData(lastName, firstName)
            {

                Mobilephone = mobilePhone,
                Homephone = homePhone,
                Workphone = workPhone,
                Email = email,
                Email2 = email2,
                Email3 = email3

            };
        }

        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToAddContactsPage();
            FillContactForm(contact);
            SubmitContactCreation();
            return this;
        }

        internal double GetContactCount()
        {
            return driver.FindElements(By.CssSelector("tr[name='entry']")).Count;
        }

        private List<ContactData> contactCache = null;

        public List<ContactData> GetContactList()
        {
            if (contactCache == null)
            {
                contactCache = new List<ContactData>();
                manager.Navigator.GoToContactsPage();

                ICollection<IWebElement> elements = driver.FindElements(By.CssSelector("tr[name='entry']"));
                foreach (IWebElement element in elements)
                {
                    var info = element.FindElements(By.CssSelector("td"));
                    var a = new ContactData(info[1].Text, info[2].Text);
                    //a.Address = info[3].Text;
                    contactCache.Add(a);
                }
            }

            return new List<ContactData>(contactCache);
        }

        public ContactHelper CloseAlert()
        {
            driver.SwitchTo().Alert().Accept();
            //Assert.IsTrue(Regex.IsMatch(CloseAlertAndGetItsText(), "^Delete 1 addresses[\\s\\S]$"));
            System.Threading.Thread.Sleep(500);
            return this;
        }

        public ContactHelper RemoveContact()
        {
            driver.FindElement(By.XPath("//input[@value='Delete']")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("mobile"), contact.Mobilephone);
            Type(By.Name("home"), contact.Homephone);
            Type(By.Name("work"), contact.Workphone);
            Type(By.Name("email"), contact.Email);
            Type(By.Name("email2"), contact.Email2);
            Type(By.Name("email3"), contact.Email3);
            return this;
        }

        public ContactHelper SubmitContactCreation()
        {
            driver.FindElement(By.XPath("//div[@id='content']/form/input[21]")).Click();
            contactCache = null;
            return this;
        }

        public ContactHelper SubmitContactModification()
        {
            driver.FindElement(By.Name("update")).Click();
            contactCache = null;
            return this;
        }

        public void GreateContactIfNotExist()
        {
            manager.Navigator.GoToContactsPage();
            if (!IsElementExist(By.Name("selected[]")))
            {
                ContactData contact = new ContactData("Ivanov", "Ivan")
                {
                    Mobilephone = "8-(77)-1272-12",
                    Homephone = "8-(45)-5267-345",
                    Workphone = "8-(42)-542-6",
                    Email = "dgfyhj@dy.yj",
                    Email2 = "etyj@yteyj.tj",
                    Email3 = "eytj@dghfj.dhgf"
                };

                Create(contact);
            }
        }

        //public int GetNumberOfSearchPage()
        //{
        //    manager.Navigator.GoToContactsPage();
        //    string text = driver.FindElement(By.TagName("label")).Text;
        //    Match m = new Regex(@"\d+").Match(text);
        //    return Int32.Parse(m.Value);
        //}
    }
}
