using System;
using System.ComponentModel.DataAnnotations;

namespace MovieBooking.Entity
{
    public class User
    {
        [Key]
        public int Id { get; private init; }

        [Required]
        [MaxLength(32)]
        public required string Name { get; set; }

        [Required]
        public DateTime RegisteredAt { get; init; } = DateTime.UtcNow;

        public DateTime LastLoginAt { get; set; } = DateTime.MinValue;
    }
}
