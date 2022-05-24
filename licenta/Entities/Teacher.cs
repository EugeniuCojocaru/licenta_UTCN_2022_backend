﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using static licenta.Entities.Constants;

namespace licenta.Entities
{
    public class Teacher
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        [Required]
        [MaxLength(100)]
        public string Email { get; set; }

        [Required]
        public Role Role { get; set; }

        public Teacher(string name, string email, Role role)
        {
            Name = name;
            Email = email;
            Role = role;
        }

        public Teacher(string name, string email)
        {
            Name = name;
            Email = email;
        }
    }
}
