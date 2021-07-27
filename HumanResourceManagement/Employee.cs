using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Homework
{
    class Employee
    {
        public string fullname { get; set; }
        public string favlang { get; set; }
        public List<List<String>> certificate { get; set; }

        public Employee(String fullname, String favlang)
        {
            this.fullname = fullname;
            this.favlang = favlang;
            this.certificate = new List<List<String>>();
        }
    }
}
