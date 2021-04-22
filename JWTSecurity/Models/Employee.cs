using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace JWTSecurity.Models
{
    [Table("Employee")]
    public partial class Employee
    {
        [Key]
        public int EmployeeId { get; set; }
        [Required]
        [StringLength(30)]
        public string Name { get; set; }
        public int Age { get; set; }
        [Required]
        [StringLength(10)]
        public string Gender { get; set; }
    }
}
