namespace EmployeeHierarchy
{
    public class Employees
    {
        private readonly List<Employee> _employees;
        private Node _ceoNode;
        public Employees(string employeeList)
        {
            // Generate employee list
            _employees = ProcessEmployeeList(employeeList);

            // Add CEO
            var ceo = _employees.Where(x => string.IsNullOrWhiteSpace(x.ManagerID)).First();
            _ceoNode = AddEmployee(ceo); // root node
        }

        public long ManagerBudget(string manager)
        {
            // Find manager node
            var managerNode = SearchEmployee(manager, _ceoNode);
            if (managerNode == null)
                throw new ArgumentException("The manager does not exist");

            // Calculate budget
            long budget = SumSalaries(managerNode, managerNode.Salary);

            return budget;
        }

        private long SumSalaries(Node employee, long initialSum)
        {
            var currentSum = initialSum;
            foreach (var sub in employee.Subordinate)
            {
                currentSum += SumSalaries(sub, currentSum + sub.Salary);
            }

            return currentSum;
        }

        // Search for employee
        private Node? SearchEmployee(string employee, Node currentNode)
        {
            Node? employeeNode = null;
            if (currentNode.EmployeeID != employee)
            {
                foreach (var nextEmployee in currentNode.Subordinate)
                {
                    var searchedEmployee = SearchEmployee(employee, nextEmployee);
                    if(searchedEmployee != null && searchedEmployee.EmployeeID == employee)
                    {
                        employeeNode = searchedEmployee;
                        break;
                    }
                }
                return employeeNode;
            }
            else
            {
                return currentNode;
            }
        }

        // Generate tree recursively
        private Node AddEmployee(Employee employee)
        {
            // Find all employees under current employee
            var subordinates = _employees.Where((x) => x.ManagerID == employee.ID);
            var currentEmployee = new Node
            {
                EmployeeID = employee.ID,
                Salary = employee.Salary,
            };

            // Process each subordinate into tree as well
            foreach (var subordinate in subordinates)
            {
                currentEmployee.Subordinate.Add(AddEmployee(subordinate));
            }

            return currentEmployee;
        }

        // Generate list from provided csv data
        private List<Employee> ProcessEmployeeList(string employeeList)
        {
            var processedList = new List<Employee>();
            foreach (var myString in employeeList.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var tempSplit = myString.Split(',');
                if (!int.TryParse(tempSplit[2], out int salary))
                    throw new ArgumentException("One or more salaries are not valid numbers.");

                processedList.Add(new Employee
                {
                    ID = tempSplit[0],
                    ManagerID = tempSplit[1],
                    Salary = salary
                });
            }

            // Extra validations
            // Only one CEO
            if (processedList.Where(x => string.IsNullOrWhiteSpace(x.ManagerID)).Count() > 1)
                throw new ArgumentException("Multiple employees with no managers were detected.");

            return processedList;
        }


        // Tree for hierarchy
        class Node
        {
            public string EmployeeID;
            public int Salary;
            public List<Node> Subordinate = new();
        }
    }
}