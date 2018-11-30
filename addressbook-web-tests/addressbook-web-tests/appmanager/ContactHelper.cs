using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.Support.UI;

namespace WebAddressbookTests
{
    public class ContactHelper : HelperBase
    {
        public ContactHelper(ApplicationManager manager) : base(manager)
        {
        }
        public ContactHelper Create(ContactData contact)
        {
            manager.Navigator.GoToAddContactsPage();
            FillContactForm(contact);
            SubmitContactCreation();
            return this;
        }

        public ContactHelper Modify(ContactData newData)
        {
            manager.Navigator.GoToContactsPage();
            InitContactModification();
            FillContactForm(newData);
            SubmitContactModification();
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

        public ContactHelper Remove()
        {
            manager.Navigator.GoToContactsPage();
            SelectContact();
            RemoveContact();
            CloseAlert();
            manager.Navigator.ReturnToContactsPage();
            return this;
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

        public ContactHelper SelectContact()
        {
            driver.FindElement(By.Name("selected[]")).Click();
            return this;
        }

        public ContactHelper FillContactForm(ContactData contact)
        {
            Type(By.Name("firstname"), contact.Firstname);
            Type(By.Name("lastname"), contact.Lastname);
            Type(By.Name("title"), contact.Title);
            Type(By.Name("company"), contact.Company);
            Type(By.Name("address"), contact.Address);
            Type(By.Name("mobile"), contact.Mobilephone);
            Type(By.Name("email"), contact.Email);
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

        public ContactHelper InitContactModification()
        {
            driver.FindElement(By.XPath("//*[@id='maintable']/tbody/tr[2]/td[8]/a/img")).Click();
            return this;
        }

        public void GreateContactIfNotExist()
        {
            manager.Navigator.GoToContactsPage();
            if (!IsElementExist(By.Name("selected[]")))
            {
                ContactData contact = new ContactData("Ivanov", "Ivan")
                {
                    Title = "Test",
                    Company = "Arl",
                    Address = "Russia",
                    Mobilephone = "888888",
                    Email = "adkl@sdkgfj.com"
                };

                Create(contact);
            }
        }
    }
}
