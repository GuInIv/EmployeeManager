using System;
using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.ServerApp.Models.BindingTargets
{
    public class EmployeeData
    {
        public long Id { get; set; }
        
        [Required]
        public string FirstName { get; set; }

        [Required]
        public string LastName { get; set; }

        [Required]
        public decimal Salary{ get; set; }

        [Required]
        public string HiringDate { get; set; }

        public string TerminationDate { get; set; }

        [Required]
        public Position Position { get; set; }

    }
}

