using System.ComponentModel.DataAnnotations;

namespace EmployeeManager.ServerApp.Models.BindingTargets
{
    public class PositionData
    {
        public long Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
