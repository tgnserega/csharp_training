using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace addressbook_tests_autoit
{
    public class ContactHelper : HelperBase
    {
        public static string CONTACTWINTITLE = "Contact Editor";

        public ContactHelper(ApplicationManager manager) : base(manager) { }

        public List<ContactData> GetContactList()
        {
            List<ContactData> list = new List<ContactData>();
            string count = aux.ControlTreeView(WINTITLE, "", "WindowsForms10.Window.8.app.0.2c908d510", "GetItemCount", "#0", "");

            for (int i = 0; i < int.Parse(count); i++)

            {

                string FirstName = aux.ControlTreeView(WINTITLE, "", "WindowsForms10.Window.8.app.0.2c908d510", "GetText", "#0|#", "");
                string LastName = aux.ControlTreeView(WINTITLE, "", "WindowsForms10.Window.8.app.0.2c908d510", "GetText", "#0|#", "");

                list.Add(new ContactData()
                {
                    Firstname = FirstName,
                    Lastname = LastName
                });
            }
            return list;
        }

        public void Remove()
        {
            //OpenContactsDialogue();
            //aux.ControlTreeView(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d5", "Select", "#0|#", "");
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d5");
            aux.WinWait("Question");
            ConfirmContactRemove();
            CloseContactsDialogue();
        }

        public void ConfirmContactRemove()
        {
            aux.ControlClick("Question", "", "WindowsForms10.BUTTON.app.0.2c908d5");
        }

        public void Add(ContactData newContact)

        {

            OpenContactsDialogue();
            aux.ControlSend(CONTACTWINTITLE, "", "WindowsForms10.EDIT.app.0.2c908d516", newContact.Firstname);
            aux.ControlSend(CONTACTWINTITLE, "", "WindowsForms10.EDIT.app.0.2c908d513", newContact.Lastname);
            ConfirmContactCreation();
            CloseContactsDialogue();
        }

        public void OpenContactsDialogue()

        {

            aux.WinWait(WINTITLE);
            aux.ControlClick(WINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d58");
            aux.WinWait(CONTACTWINTITLE);
        }

        public void ConfirmContactCreation()

        {
            aux.ControlClick(CONTACTWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d57");
        }

        public void CloseContactsDialogue()
        {
            aux.ControlClick(CONTACTWINTITLE, "", "WindowsForms10.BUTTON.app.0.2c908d59");
        }
    }
}
