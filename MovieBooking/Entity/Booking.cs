using System;
using System.ComponentModel.DataAnnotations;

using Microsoft.EntityFrameworkCore;

namespace MovieBooking.Entity
{
    [Index(nameof(MovieId), nameof(UserId), nameof(ShowTime), IsUnique = true)]
    public class Booking
    {
        [Key]
        public int Id { get; private set; }

        [Required]
        private int MovieId { get; set; }

        public required Movie Movie { get; init; }

        [Required]
        private int UserId { get; set; }

        public required User User { get; init; }

        [Required]
        public TimeOnly ShowTime { get; set; }
    }
}
