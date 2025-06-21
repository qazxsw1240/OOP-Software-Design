using System;
using System.ComponentModel.DataAnnotations;

namespace MovieBooking.Entity
{
    public class User
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        public required string Name { get; set; }

        [Required]
        public DateTime RegisteredAt { get; set; } = DateTime.UtcNow;

        public DateTime LastLoginAt { get; set; } = DateTime.MinValue;
    }
}
