using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mail_Sender
{
    interface IMailSender
    {       
        void AddPerson(string name, string email);
        string SendEmail();
    }
}
