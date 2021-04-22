using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

#nullable disable

namespace JWTSecurity.Models
{
    public partial class User
    {
        [Key]
        public int UserId { get; set; }
        [Required]
        [StringLength(30)]
        public string UserName { get; set; }
        [Required]
        [StringLength(30)]
        public string Password { get; set; }
        [Required]
        [StringLength(30)]
        public string Role { get; set; }
    }
}
