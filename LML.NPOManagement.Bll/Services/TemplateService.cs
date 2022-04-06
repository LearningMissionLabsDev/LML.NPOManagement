using LML.NPOManagement.Bll.Interfaces;
using LML.NPOManagement.Bll.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LML.NPOManagement.Bll.Services
{
    public class TemplateService : ITemplateService
    {
       
        public string Body = "Registration User";

        public string RegistrationUser(string name, DateTime createDate)
        {
            string reg = $"barev hargarjan {name} shnorhakal enq mer cragrum grancvelu hamar. grancum@ katarvel e {createDate}";
            return reg;
        }
        public string Donor(string name, DateTime dateOfCharity)
        {
            string donationEmail = $"";
            return donationEmail;
        }
        


    }
}
