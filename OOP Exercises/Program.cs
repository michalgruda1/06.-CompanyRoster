using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOP_exercises
{
    class Program
    {
        static void Main(string[] args)
        /* test data
        Michał, Młodszy Programista, 3500, IT
        Wojtek, Starszy Programista, 10000,IT
        Sylwia, Starszy UX, 8000,IT
        Mariusz, UX, 6000,IT
        Mariusz, Product Manager, 14000, Marketing
        Paweł, Dyrektor, 18000, Marketing
        Bartek, PO, 12000, Marketing
        */
        {
            // inform user
            Console.WriteLine("Write down employees: Name, Position, Salary, Department, email and age:");

            // initialize storage
            // TODO: Why is this list neccessary?
            List<Employee> employees = new List<Employee>();
            Dictionary<string, Department> departments = new Dictionary<string, Department>();

            // do actual user input reading
            string input = Console.ReadLine();

            // initialize local variables, so compiler does not complain
            string name = "";
            string position = "";
            float salary = 0F;
            string department = "";
            string email = "n/a";
            int age = -1;

            while (input != string.Empty)
            {
                string[] separators = { ", ", ",", " , " };
                string[] line = input.Split(separators, StringSplitOptions.RemoveEmptyEntries);

                // name, position, department and salary are mandatory
                // max 6 fields

                if (line.Length < 4)
                {
                    throw new Exception("Name, position, salary and department are mandatory");
                }
                else if (line.Length == 4)
                {
                    name = line[0];
                    position = line[1];
                    salary = float.Parse(line[2]);
                    department = line[3];
                }
                else if (line.Length == 5 && line[4].Trim() != "")
                {
                    email = line[4];
                }
                else if (line.Length == 6 && line[5].Trim() != "")
                {
                    bool e = Int32.TryParse(line.ElementAtOrDefault(5), out age);
                }
                else
                {
                    throw new Exception("Too many fields, maximum 6 fields in line");
                }

                // add new employee to employee list
                Employee tempEmployeeObj = new Employee(name, position, salary, department, email, age);
                employees.Add(tempEmployeeObj);

                // add department of employee
                try
                {
                    // add department to list of depts
                    departments.Add(department, new Department(department));
                    Console.WriteLine("Added new department {0}", department);
                }
                catch (Exception)
                {
                    Console.WriteLine("Department {0} exists", department);
                }

                // add employee to right dept, and store salary
                if (departments.ContainsKey(department))
                {
                    /* 
                    // old implementation
                    departments[department].AddEmployee(name, salary);
                    */

                    // new implementation
                    departments[department].AddEmployee(name, tempEmployeeObj);
                }
                else
                {
                    throw new Exception("Oops, issue with adding a new department " + department);
                }
                input = Console.ReadLine();
            }

            // initialize variables
            float maxAvgSalary = 0F;
            string highestPaidDept = "";

            /*
            // old implementation
            foreach (KeyValuePair<string,Department> dep in departments)
            {
                if (dep.Value.GetAverage() > maxAvgSalary)
                {
                    maxAvgSalary = dep.Value.GetAverage();
                    highestPaidDept = dep.Key;
                }
            }
            Console.WriteLine("Highest paid dept is {0}, avg salary is {1}",highestPaidDept,maxAvgSalary);
            */

            // new implementation
            foreach (KeyValuePair<string, Department> kvp in departments)
            {
                if (maxAvgSalary < kvp.Value.GetAverageSalary())
                {
                    maxAvgSalary = kvp.Value.GetAverageSalary();
                    highestPaidDept = kvp.Key;
                }
            }
            Console.WriteLine("Highest paid dept is {0}, avg salary is {1:C2}", highestPaidDept, maxAvgSalary);

            Console.WriteLine("Employees of highest paid dept, sorted by salary DESC:");

            foreach (Employee emp in departments[highestPaidDept].GetEmployeesSortedBySalary(highestPaidDept))
            {
                Console.WriteLine(emp.ToString());
            }

            Console.Read();
        }
    }
}