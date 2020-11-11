using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EmployeeManager.ServerApp.Models
{
    public class Employee    
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        [Column(TypeName = "decimal(8, 2)")]
        public decimal Salary { get; set; }

        public DateTime HiringDate { get; set; }

        public DateTime? TerminationDate { get; set; }

        public Position Position { get; set; }
    }
}
