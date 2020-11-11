using System;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace EmployeeManager.ServerApp.Models
{
    public class SeedDb
    {
        public static void SeedDatabase(DataContext context)
        {
          //  context.Database.Migrate();

            if (context.Employees.Count() == 0)
            {
                var position1 = new Position
                {
                    Name = "Junior Dev"
                };
                var position2 = new Position
                {
                    Name = "Middle Dev"
                };
                var position3 = new Position
                {
                    Name = "Senior Dev"
                };

                context.Employees.AddRange(
                    new Employee
                    {
                        FirstName = "Ivan",
                        LastName = "Ivanov",
                        Position = position1,
                        Salary = 1000,
                        HiringDate = new DateTime(2020, 1, 1)
                    },
                     new Employee
                     {
                         FirstName = "Petr",
                         LastName = "Petrov",
                         Position = position2,
                         Salary = 2500,
                         HiringDate = new DateTime(2020, 2, 1)
                     },
                     new Employee
                     {
                         FirstName = "Boris",
                         LastName = "Sidorov",
                         Position = position1,
                         Salary = 1300,
                         HiringDate = new DateTime(2020, 3, 1)
                     },
                     new Employee
                     {
                         FirstName = "Angela",
                         LastName = "Ivanova",
                         Position = position3,
                         Salary = 3000,
                         HiringDate = new DateTime(2018, 1, 1)
                     },
                     new Employee
                     {
                         FirstName = "Anna",
                         LastName = "Petrova",
                         Position = position2,
                         Salary = 2700,
                         HiringDate = new DateTime(2017, 1, 1)
                     },
                     new Employee
                     {
                         FirstName = "Helena",
                         LastName = "Sidorova",
                         Position = position3,
                         Salary = 3500,
                         HiringDate = new DateTime(2017, 5, 1)
                     });

                context.SaveChanges();
            }
        }
    }
}
