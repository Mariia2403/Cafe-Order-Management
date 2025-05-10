using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna_Cafe
{
    public class Chef
    {
        private string firstName;
        private string lastName;

        public Chef(string firstName, string lastName)
        {
            this.firstName = firstName;
            this.lastName = lastName;
        }

        public string GetFirstName() => firstName;
        public string GetLastName() => lastName;

        public string FullName() => $"{lastName} {firstName}";
    }
}
