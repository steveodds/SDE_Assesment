using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace EmployeeHierarchy.Test
{
    [TestClass]
    public class EmployeeUnitTests
    {
        [TestMethod]
        public void GenerateHierarchyHasProperCEO()
        {
            var employees = @"Emplyee4,Employee2,500
Employee3,Employee1,800
Employee1,,1000
Employee5,Employee1,500
Employee2,Employee1,500";

            var orgChart = new Employees(employees);
            Assert.AreEqual(orgChart._ceoNode.EmployeeID, "Employee1");
        }

        [TestMethod]
        public void ThrowsOnInvalidSalary()
        {
            var employees = @"Emplyee4,Employee2,500
Employee3,Employee1,800
Employee1,,1000
Employee5,Employee1,500
Employee2,Employee1,12e3";

            Assert.ThrowsException<ArgumentException>(() => new Employees(employees));
        }

        [TestMethod]
        public void ThrowsOnFindingMultipleEmployeesWithNoManager()
        {
            var employees = @"Emplyee4,Employee2,500
Employee3,Employee1,800
Employee1,,1000
Employee5,,500
Employee2,Employee1,500";

            Assert.ThrowsException<ArgumentException>(() => new Employees(employees));
        }

        [TestMethod]
        public void ThrowsOnEmployeeWithMultipleManagers()
        {
            var employees = @"Emplyee4,Employee2,500
Employee3,Employee1,800
Employee1,,1000
Employee2,Employee1,500
Employee2,Employee1,500";

            Assert.ThrowsException<ArgumentException>(() => new Employees(employees));
        }

        [TestMethod]
        public void ThrowsOnEmployeesReportingToEachOtherCircular()
        {
            var employees = @"Emplyee4,Employee2,500
Employee3,Employee1,800
Employee1,,1000
Employee5,Employee2,500
Employee6,Employee7,200
Employee7,Employee6,250
Employee2,Employee1,500";

            Assert.ThrowsException<ArgumentException>(() => new Employees(employees));
        }

        [TestMethod]
        public void ThrowsOnManagerThatIsNotInEmployeeColumn()
        {
            var employees = @"Emplyee4,Employee2,500
Employee3,Employee1,800
Employee1,,1000
Employee5,Employee6,500
Employee2,Employee1,500";

            Assert.ThrowsException<ArgumentException>(() => new Employees(employees));
        }

        [TestMethod]
        public void CalculatesValidTotalManagerBudget()
        {
            var employees = @"Emplyee4,Employee2,500
Employee3,Employee1,800
Employee1,,1000
Employee5,Employee1,500
Employee2,Employee1,500";

            var orgChart = new Employees(employees);
            var budget = orgChart.ManagerBudget("Employee1");
            Assert.AreEqual(3300L, budget);
        }
    }
}