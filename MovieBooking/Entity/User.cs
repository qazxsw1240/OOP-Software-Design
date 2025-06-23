using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MovieBooking.Entity
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; private init; }

        [Required]
        [MaxLength(32)]
        public required string Name { get; set; }

        [Required]
        public DateTime RegisteredAt { get; init; } = DateTime.UtcNow;

        public DateTime LastLoginAt { get; set; } = DateTime.MinValue;
    }
}
