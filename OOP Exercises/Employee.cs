using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_exercises
{

    // TODO: Define a class Employee that holds the following information: name, salary, position, department, email and age
    // TODO: The name, salary, position and department are mandatory while the rest are optional.
    // TODO: Take N lines of employees from the console
    // TODO: Calculate the department with the highest average salary
    // TODO: Print for each employee in that department his name, salary, email and age
    // TODO: Sort employee by salary in descending order
    // TODO: If an employee doesn’t have an email – in place of that field you should print “n/a” instead
    // TODO: if he doesn’t have an age – print “-1” instead
    // TODO: The salary should be printed to two decimal places after the seperator.

    class Employee
    {
        public string Name { get; set; }
        public string Position { get; set; }
        public float Salary { get; set; }
        public string Department { get; set; }
        public string Email { get; set; }
        public int Age { get; set; }

        public Employee(string name, string position, float salary, string departament, string email, int age)
        {
            Name = name;
            Position = position;
            Salary = salary;
            Department = departament;
            Email = email;
            Age = age;
        }

        public Employee(string name, string position, float salary, string departament) : this(name, position, salary, departament, "n/a", -1) { }

        public override string ToString()
        {
            return String.Format("{0} - {1}, department {2}, salary {3:C2}, email {4}, age {5}", Name, Position, Department, Salary, Email, Age);
        }
    }

    class Department
    {
        public string name;
        public float avgSalary;
        private int employeeNumber;
        private float salarySumOfDept;
        private Dictionary<string, Employee> departmentEmployees;

        public Department(string depName)
        {
            this.name = depName;
            this.avgSalary = 0F;
            this.employeeNumber = 0;
            this.salarySumOfDept = 0F;
            // this.departmentEmployeeList = new Dictionary<string, float>();
            this.departmentEmployees = new Dictionary<string, Employee>();
        }

        /*
        // this is method that supports old implementation
        public void AddEmployee(string employeeName, float employeeSalary)
        {
            departmentEmployeeList.Add(employeeName, employeeSalary);
            this.employeeNumber++;
            this.salarySumOfDept += employeeSalary;
        }
        */

        // this method is new implementation
        public void AddEmployee(string employeeName, Employee employee)
        {
            departmentEmployees.Add(employeeName, employee);
            this.employeeNumber++;
        }
        /*
        // old method
        public float GetAverage()
        {
            return this.avgSalary = salarySumOfDept / (float)employeeNumber;
        }
        */

        // new method
        public float GetAverageSalary()
        {
            this.salarySumOfDept = 0F;
            foreach (Employee employee in departmentEmployees.Values)
            {
                this.salarySumOfDept += employee.Salary;
            }
            return this.avgSalary = salarySumOfDept / (float)employeeNumber;
        }

        public Stack<Employee> GetEmployeesSortedBySalary(string departmentName)
        {
            SortedList<float, Employee> employeesSortedBySalaryASC = new SortedList<float, Employee>();
            foreach (Employee emp in departmentEmployees.Values)
            {
                if (emp.Department == departmentName)
                {
                    employeesSortedBySalaryASC.Add(emp.Salary, emp);
                }
            }
            // we now have a list of employees from selected dept sorted by salary, in ascending order
            // to fulfill the requirement, we have to sort it by salary in descending order
            // let's use stack for that
            Stack<Employee> employeesSortedBySalaryDESC = new Stack<Employee>();

            foreach (Employee emp in employeesSortedBySalaryASC.Values)
            {
                employeesSortedBySalaryDESC.Push(emp);
            }
            return employeesSortedBySalaryDESC;
        }

        public override string ToString()
        {
            StringBuilder msg = new StringBuilder("\n" + this.name + ", average salary: " + this.GetAverageSalary() + ", no of employees: " + this.employeeNumber);
            msg.AppendLine("\nHere are the employees:");
            foreach (KeyValuePair<string, Employee> kvp in departmentEmployees)
            {
                msg.AppendFormat("\n{0}, salary: {1:C2}", kvp.Key, kvp.Value.Salary);
            }
            return msg.ToString();
        }
    }
}
