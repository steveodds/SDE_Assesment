using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeHierarchy
{
    // Represents a single employee
    internal class Employee
    {
        public string ID { get; set; }
        public string ManagerID { get; set; }
        public int Salary { get; set; }
    }
}
