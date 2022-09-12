namespace EmployeeHierarchy
{
    public class Employees
    {
        private readonly List<Employee> _employees;
        public readonly Node _ceoNode;
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
                currentSum = SumSalaries(sub, currentSum + sub.Salary);
            }

            return currentSum;
        }

        // Search for employee
        private Node? SearchEmployee(string employee, Node currentNode)
        {
            Node? employeeNode = null;
            if (!currentNode.EmployeeID.Equals(employee))
            {
                foreach (var nextEmployee in currentNode.Subordinate)
                {
                    var searchedEmployee = SearchEmployee(employee, nextEmployee);
                    if(searchedEmployee != null && searchedEmployee.EmployeeID.Equals(employee))
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
            var subordinates = _employees.Where((x) => x.ManagerID.Equals(employee.ID));
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
            var managers = new List<string>();
            foreach (var myString in employeeList.Split(new string[] { Environment.NewLine }, StringSplitOptions.RemoveEmptyEntries))
            {
                var tempSplit = myString.Split(',');
                // check for invalid salaries
                if (!int.TryParse(tempSplit[2], out int salary))
                    throw new ArgumentException("One or more salaries are not valid numbers.");

                // check for multiple managers
                if (processedList.Any(x => x.ID.Equals(tempSplit[0]) && !x.ManagerID.Equals(tempSplit[1])))
                    throw new ArgumentException("Employees cannot have more than one manager.");

                // check for circular employee-manager relationship
                if (processedList.Any(x => x.ID.Equals(tempSplit[1]) && x.ManagerID.Equals(tempSplit[0])))
                    throw new ArgumentException("Employees cannot manage employees listed as their manager.");

                processedList.Add(new Employee
                {
                    ID = tempSplit[0],
                    ManagerID = tempSplit[1],
                    Salary = salary
                });
                managers.Add(tempSplit[1]);
            }

            // Extra validations
            // Only one CEO
            if (processedList.Where(x => string.IsNullOrWhiteSpace(x.ManagerID)).Count() > 1)
                throw new ArgumentException("Multiple employees with no managers were detected.");

            // Managers must also be employees
            foreach (var manager in managers.Distinct())
            {
                if (string.IsNullOrWhiteSpace(manager))
                    continue;
                if (!processedList.Any(x => x.ID.Equals(manager)))
                    throw new ArgumentException("A manager was found that is not listed as an employee.");
            }

            return processedList;
        }


        // Tree for hierarchy
        public class Node
        {
            public string EmployeeID;
            public int Salary;
            public List<Node> Subordinate = new();
        }
    }
}