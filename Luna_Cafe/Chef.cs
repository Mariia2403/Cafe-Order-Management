using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Luna_Cafe
{
    public class Chef
    {
        public string FirstName { get; }
        public string LastName { get; }

        public Chef(string firstName, string lastName)
        {
            FirstName = firstName;
            LastName = lastName;
        }
    }
}
